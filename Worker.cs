using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TicTacToe
{

    

    /// <summary>
    /// Непосредственно UDP клиент - сервер
    /// </summary>
//    public class Worker :IWorker
//    {
//        /// <summary>
//        /// порт, по которому идет передача и прием всей информации 
//        /// </summary>
//        private readonly int _port;
//
//        /// <summary>
//        /// Сокет, отправляющий сообщения
//        /// </summary>
//        private readonly Socket _sendSocket;
//
//        /// <summary>
//        /// Сокет, принимающий сообщения
//        /// </summary>
//        private Socket _listenSocket;
//
//        private Thread _listenThread;
//
//        private string _sendedIP;
//
//        /// <summary>
//        /// Адрес другого клиента
//        /// </summary>
//        private EndPoint _sendedPoint;
//
//        public Worker(int port, string sendedIP = null)
//        {
//            //сокет на UDP протокол по IPv4
//            _sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//
//            _port = port;
//            SetListen(sendedIP);
//        }
//
//        public bool DoStop { get; set; }
//
//        /// <summary>
//        /// Подписавшиеся на данный делегат методы получат сообщения от другого клиента
//        /// </summary>
//        public event MessageEventHandler NewMessage;
//
//
//        public void OnNewMessage(MessageEventArgs e)
//        {
//            MessageEventHandler handler = NewMessage;
//            if (handler != null) handler(this, e);
//        }
//
//
//        public void SendMessage(string message)
//        {
//            if (_sendedIP == null)
//                throw new NullReferenceException(String.Format("Не указан клиент для отправки сообщения {0}", message));
//            byte[] data = Encoding.ASCII.GetBytes(message);
//            _sendSocket.SendTo(data, data.Length, SocketFlags.None, GetHost(_sendedIP));
//        }
//
//        private void ListenCycle()
//        {
//            while (true)
//            {
//                if (DoStop)
//                    break;
//                if (_sendedPoint == null)
//                    throw new NullReferenceException(String.Format("Не указан клиент для прослушивания"));
//                var data = new byte[1024];
//                int length = _listenSocket.ReceiveFrom(data, ref _sendedPoint);
//                string message = Encoding.ASCII.GetString(data).Substring(0, length);
//                OnNewMessage(new MessageEventArgs(message));
//            }
//        }
//
//        /// <summary>
//        /// Установка связи с sendedIP
//        /// </summary>
//        public void SetListen(string sendedIP)
//        {
//            _sendedIP = sendedIP;
//            _sendedPoint = GetPoint();
//
//            DieThread();
//            DoStop = false;
//
//            _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//            _listenSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
//
//            _listenThread = new Thread(ListenCycle);
//            _listenThread.Start();
//        }
//
//        public void DieThread()
//        {
//            if (_listenSocket != null)
//            {
//                try
//                {
//                    _listenSocket.Close();
//                } catch(Exception e)
//                {
//                    Console.WriteLine(e);
//                }
//            }
//            if (_listenThread != null && _listenThread.IsAlive)
//                _listenThread.Abort();
//        }
//
//
//        private EndPoint GetHost(string host)
//        {
//            IPAddress hostIPAddress = IPAddress.Parse(host);
//            EndPoint hostIPEndPoint = new IPEndPoint(hostIPAddress, _port);
//            return hostIPEndPoint;
//        }
//
//        private EndPoint GetPoint()
//        {
//            return _sendedIP != null ? GetHost(_sendedIP) : new IPEndPoint(IPAddress.Any, _port);
//        }
//    }
}