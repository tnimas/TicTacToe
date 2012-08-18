using System;

namespace TicTacToe
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message)
        {
            Message = message;
        }
        public MessageEventArgs(string message,string ipAdress)
        {
            Message = message;
            IpAdress = ipAdress;
        }

        public string Message { get; set; }
        public string IpAdress { get; set; }
    }
}