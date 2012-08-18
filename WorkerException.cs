using System;

namespace TicTacToe
{
    public class WorkerException : Exception {
        public MessageEventArgs Content {get; set;}
        public WorkerException(MessageEventArgs msg):base(msg.Message) {
            Content = msg;
        }
    }
}