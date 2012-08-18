using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TicTacToe
{
    public class TcpWorker : IWorker
    {
        private Thread _listenThread;
        private volatile bool _needExit;
        private int _port;

        private TcpListener _tcpListener;

        private TcpClient _writeClient;

        #region IWorker Members

        public event Action<object, MessageEventArgs> NewMessage;
        public event Action<object, MessageEventArgs> ErrorMessage;

        public void Init(int port)
        {
            _port = port;
            _tcpListener = new TcpListener(IPAddress.Any, port);

            _listenThread = new Thread(ListenForClients);
            _listenThread.Start();
        }

        public void StopWork()
        {
            RemoveWriter();
            _needExit = true;
        }

        public void SendMessage(string message, string sendedIp)
        {
            try
            {
                TcpClient tcpClient = GetWriter(sendedIp);
                NetworkStream clientStream = tcpClient.GetStream();

                var encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(message);
                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
            }
            catch (Exception e)
            {
                OnErrorMessage(new MessageEventArgs(e.Message, sendedIp));
            }
        }

        #endregion

        protected void OnNewMessage(MessageEventArgs e)
        {
            if (NewMessage != null)
            {
                NewMessage(this, e);
            }
        }

        protected void OnErrorMessage(MessageEventArgs e)
        {
            if (ErrorMessage != null)
            {
                ErrorMessage(this, e);
            }
        }


        /// <summary>
        /// Ожидание клиентов и генерация потоков для обработки их сообщений.
        /// </summary>
        private void ListenForClients()
        {
            _tcpListener.Start();
            while (!_needExit)
            {
                if (!_tcpListener.Pending())
                {
                    Thread.Sleep(500);
                    continue;
                }
                TcpClient client = _tcpListener.AcceptTcpClient();

                var readThread = new Thread(HandleClientComm);
                readThread.Start(client);
            }
        }


        /// <summary>
        /// Метод выполняемый для каждого подключившегося клиента в отдельном потоке.
        /// Чтение сообщений, кодирование в символы и отправка посредством события вверх.
        /// </summary>
        /// <param name="client">подключившийся TcpClient</param>
        private void HandleClientComm(object client)
        {
            var tcpClient = (TcpClient) client;
            string ip = ((IPEndPoint) tcpClient.Client.RemoteEndPoint).Address.ToString();
            NetworkStream clientStream = tcpClient.GetStream();
            var message = new byte[4096];

            try
            {
                while (!_needExit)
                {
                    //blocks until a client sends a message
                    if (!clientStream.DataAvailable)
                    {
                        Thread.Sleep(200);
                        continue;
                    }
                    int bytesRead = clientStream.Read(message, 0, 4096);

                    if (bytesRead == 0)
                    {
                        //the client has disconnected from the server
                        break;
                    }

                    //message has successfully been received
                    var encoder = new ASCIIEncoding();
                    string msg = encoder.GetString(message, 0, bytesRead);

                    OnNewMessage(new MessageEventArgs(msg, ip));
                    //System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));
                }
            }
            catch (Exception e)
            {
                OnErrorMessage(new MessageEventArgs(e.Message, ip));
            }
            finally
            {
                tcpClient.Close();
            }
        }


        private void RemoveWriter()
        {
            if (_writeClient != null && _writeClient.Connected)
                _writeClient.Close();
            _writeClient = null;
        }

        /// <summary>
        /// Получить клиент для отправки сообщений на указанный ip
        /// </summary>
        private TcpClient GetWriter(string ip)
        {
            if (_writeClient == null)
            {
                _writeClient = new TcpClient(ip, _port);
            }
            else
            {
                string thisIp = ((IPEndPoint) _writeClient.Client.RemoteEndPoint).Address.ToString();
                if (thisIp != ip || !_writeClient.Connected)
                {
                    RemoveWriter();
                    _writeClient = new TcpClient(ip, _port);
                }
            }

            return _writeClient;
        }
    }
}