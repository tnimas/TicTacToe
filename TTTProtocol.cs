using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using TicTacToe.Properties;

namespace TicTacToe
{

    public class TTTProtocol :ITTTProtocol
    {

        /// <summary>
        /// Вспомогательный класс для определения идет ли игра
        /// </summary>
        private class StateController
        {
            public class ControlState
            {
                public string OtherIP { get; set; }
                public bool Check { get; set; }
            }
            public enum State
            {
                OnThis, OnOther, Game
            }

            public StateController()
            {
                Data = new Dictionary<State, ControlState>(3)
                           {
                               {State.OnThis, new ControlState()},
                               {State.OnOther, new ControlState()},
                               {State.Game, new ControlState()}
                           };
            }

            private Dictionary<State, ControlState> Data { get; set; } 


            /// <summary>
            /// hello запрос на игру текущим игроком или противником
            /// </summary>
            /// <param name="state">какой игрок запросил</param>
            /// <param name="ip">ip противника</param>
            public void FirstClick(State state, string ip)
            {
                if (state == State.Game)
                    throw new ArgumentException();
                Data[state].Check = true;
                Data[state].OtherIP = ip;
            }

            /// <summary>
            /// Положительный ответ на запрос на игру
            /// </summary>
            /// <param name="state">какой игрок ответил</param>
            /// <param name="ip">ip противника, если метод вызван пришедшим извне сообщения или null если нет</param>
            public void SecondClick(State state,string ip=null)
            {
                if (state == State.Game)
                    throw new ArgumentException();
                //убедимся что игру подтвердил нужный игрок
                if (Data[state].Check && (ip == null || ip == Data[state].OtherIP))
                {
                    Data[State.Game].Check = true;
                    Data[State.Game].OtherIP = Data[state].OtherIP;

                }
            }

            /// <summary>
            /// Остановка текущей игры
            /// </summary>
            public void StopGame()
            {
                foreach (var controlState in Data)
                {
                    controlState.Value.Check = false;
                    controlState.Value.OtherIP = null;
                }
            }

            public bool ListenMessage(string ip)
            {
                return GameState.OtherIP == null || ip == GameState.OtherIP;
            }

            public string GetIp(State state)
            {
                return Data[state].OtherIP;
            }

            /// <summary>
            /// Состояние игры и ip противника
            /// </summary>
            public ControlState GameState { get { return Data[State.Game]; } }

        }

        private readonly StateController _state = new StateController();

        public static int Port = int.Parse(Resources.Port);

        private IWorker _worker;

        private void Send(string message,StateController.State state = StateController.State.Game)
        {
            string sendedIp = _state.GetIp(state);
                if (sendedIp == null)
                throw new NullReferenceException();

            _worker.SendMessage(message,sendedIp);
        }

        public void Init()
        {
            _worker = Factory.Instance.CreateWorker();
            _worker.NewMessage += GetMessage;

            _worker.Init(Port);

            _worker.ErrorMessage += WorkerError;

        }

        public void CloseGame()
        {
            _state.StopGame();
        }

        private void WorkerError(object sender, MessageEventArgs e)
        {
            if (_state.GameState.OtherIP != null && _state.GameState.OtherIP == e.IpAdress)
            {
                if (ErrorMessage != null)
                    ErrorMessage.Invoke(sender, e);
            }
            
        }

        public event Action<object, MessageEventArgs> NewGame;
        public event Action<object, MessageEventArgs> ConfirmGame;
        public event Action<object,TurnEventArgs> NextTurn;
        public event Action<object, MessageEventArgs> ErrorMessage;

        protected void OnNextTurn(TurnEventArgs e)
        {
            Action<object, TurnEventArgs> handler = NextTurn;
            if (handler != null) handler(this, e);
        }

        protected void OnConfirmGame(MessageEventArgs e)
        {
            Action<object, MessageEventArgs> handler = ConfirmGame;
            if (handler != null) handler(this, e);
        }

        protected void OnNewGame(MessageEventArgs e)
        {
            Action<object, MessageEventArgs> handler = NewGame;
            if (handler != null) handler(this, e);
        }

        public void PostGoGame(string clientIp)
        {
            _state.FirstClick(StateController.State.OnThis,clientIp);

            string thisIP = GetThisIP();

            Send("hello_" + thisIP,StateController.State.OnThis);
        }
        
        public void PostYouLoser()
        {
            Send("you_loser");
        }

        public void PostMyTurn(int x, int y)
        {
            Send(y + "_" + x);
        }

        /// <param name="type">"1" - текущий клиент ходит ноликом, "2" - крестиком</param>
        public void PostContinue(string type)
        {
            _state.SecondClick(StateController.State.OnOther);
            Send(String.Format("ok_{0}_{1}", GetThisIP(), type),StateController.State.OnOther);
        }

        protected string GetThisIP()
        {
            String host = Dns.GetHostName();
            return
                Dns.GetHostEntry(host).AddressList.First(item => item.AddressFamily == AddressFamily.InterNetwork).ToString();
        }

        public void Exit()
        {
            _worker.StopWork();
        }

        protected void GetMessage(object sender, MessageEventArgs e)
        {
            if (!_state.ListenMessage(e.IpAdress))
                return;
            
            string[] msg = e.Message.Split('_');

            switch (msg[0])
            {
                case "hello":
                    {
                       _state.FirstClick(StateController.State.OnOther, e.IpAdress);
                        OnNewGame(new MessageEventArgs(msg[1]));
                        break;
                    }
                case "ok":
                    {
                        if (msg.Length == 3)
                        {
                            _state.SecondClick(StateController.State.OnThis,e.IpAdress);
                            if (_state.GameState.Check)
                                OnConfirmGame(new MessageEventArgs(msg[2]));
                            
                        }
                    }
                    break;
                case "you":
                    Send("ok");
                    break;
                default:
                    int y, x;
                    bool parseY = int.TryParse(msg[0], out y);
                    bool parseX = int.TryParse(msg[1], out x);
                    if (parseX && parseY && msg.Length == 2)
                    {
                        OnNextTurn(new TurnEventArgs(x, y,false));
                    }

                    break;
            }
        }


    }
}