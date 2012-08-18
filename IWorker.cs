using System;

namespace TicTacToe
{
    /// <summary>
    /// Реализация протокола отправки - приема
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        /// сообщение от подключенного клиента пришло сообщение
        /// </summary>
        event Action<object, MessageEventArgs> NewMessage;

        event Action<object, MessageEventArgs> ErrorMessage;
        
        /// <summary>
        /// отправка сообщения указанному ip
        /// </summary>
        /// <param name="message">текст сообщения</param>
        /// <param name="ip">ip, на которое будет отправлено сообщение </param>
        void SendMessage(string message,string ip);
        
        /// <summary>
        /// Установка прослушивания на указанный порт
        /// </summary>
        /// <param name="port">порт, по которому пойдет соединение</param>
        void Init(int port);

        /// <summary>
        /// очищение сетевых соединений
        /// </summary>
        void StopWork();


    }
}
