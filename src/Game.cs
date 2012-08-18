using System;
using System.Drawing;
using System.Net.Sockets;
using TicTacToe.Properties;

namespace TicTacToe
{
    public class Game
    {
        private readonly Model _model;
        private readonly ITTTProtocol _protocol;
        private readonly IView _view;

        public Game()
        {
            Factory.Instance.CreateExtensions().AppInit();

            _model = new Model();

            _view = Factory.Instance.CreateView();
            _protocol = Factory.Instance.CreateTTTProtocol();
        }

        public void Init()
        {
            _protocol.NewGame += ProtocolOnNewGame;
            _protocol.ConfirmGame += ProtocolOnConfirmGame;
            _protocol.NextTurn += ProtocolOnNextTurn;
            _protocol.ErrorMessage += ProtocolOnErrorMessage;
            try
            {
                _protocol.Init();
            }
            catch (SocketException)
            {
                _view.Say(Resources.Game_Init_Port_Is_No_Available);
                Factory.Instance.CreateExtensions().AppExit();
            }

            _view.CallBackNewGame += ViewOnCallBackNewGame;
            _view.CallBackExit += ViewOnCallBackExit;
            _view.CallBackSetCellState += ViewOnCallBackSetCellState;

            _view.Init(_model);
        }

        private void ProtocolOnErrorMessage(object o, MessageEventArgs messageEventArgs)
        {
            _protocol.CloseGame();
            _model.EmptyGame();
            _view.Repaint();
            _view.Say(Resources.Game_ProtocolOnErrorMessage_ClientAborted);
        }

        /// <summary>
        /// Реакция на попытку пользователя сделать ход
        /// </summary>
        private void ViewOnCallBackSetCellState(object o, TurnEventArgs e)
        {
            var coord2D = new Point(e.X, e.Y);
            if (_model.ThisGamer.ThisTurn)
                MakeTurn(coord2D);
        }

        /// <summary>
        /// Реакция на попытку выйти из приложения
        /// </summary>
        private void ViewOnCallBackExit(object o, EventArgs eventArgs)
        {
            _protocol.Exit();
        }

        /// <summary>
        /// Здесь вызывается меню для ввода пользователем ип адреса, после чего 
        /// введенному адресу посылается предложение игры
        /// </summary>
        private void ViewOnCallBackNewGame(object o, EventArgs eventArgs)
        {
            string ip = _view.GetOtherIpOnUser();
            if (ip != null)
            {
                EndGame(Model.CheckStateResult.Process);

                _protocol.PostGoGame(ip);
            }
        }


        /// <summary>
        /// Поставить крестик или нолик в указанную точку и выполнить все изменения состояния игры
        /// </summary>
        /// <param name="coord2D">координаты относительно ячеек</param>
        private void MakeTurn(Point coord2D)
        {
            int id = _model.GetIdBy2D(coord2D);
            if (id < 9 && id >= 0)
            {
                Cell currentCell = _model.Data[id];
                //получили номер ячейки и вытащили по нему сам объект ячейки
                if (currentCell.State != null)
                    return;

                Gamer tGamer = _model.ThisGamer;

                if (tGamer.ThisTurn)
                {
                    tGamer.ThisTurn = false;

                    currentCell.State = tGamer.PlayState;
                    _view.Repaint();
                    
                    //отправили сообщение оппоненту
                    _protocol.PostMyTurn(coord2D.X, coord2D.Y);
                }
                else
                {
                    currentCell.State = !tGamer.PlayState;
                    _view.Repaint();
                    tGamer.ThisTurn = true;
                }

                //проверить не завершилась ли игра и если да, то как
                Model.CheckStateResult cellsState = _model.CheckCellsState();
                if (cellsState != Model.CheckStateResult.Process)
                    EndGame(cellsState);
            }
        }

        private void EndGame(Model.CheckStateResult cellsState)
        {
            if (cellsState == Model.CheckStateResult.ThisGamerWin)
            {
                _protocol.PostYouLoser();
                _view.Say(Resources.UIGameTTT_You_Win);
            }
            if (cellsState == Model.CheckStateResult.OtherGamerWin)
            {
                _view.Say(Resources.UIGameTTT_You_Miss);
            }
            if (cellsState == Model.CheckStateResult.Draw)
            {
                _view.Say(Resources.UIGameTTT_Drawn);
            }
            _protocol.CloseGame();
            _model.EmptyGame();
            _view.Repaint();
        }

        /// <summary>
        /// Предложение игры от другого пользователя
        /// </summary>
        private void ProtocolOnNewGame(object sender, MessageEventArgs e)
        {
            bool? res = _view.NewGameProposal(e.Message);

            if (res != null)
            {
                //на случай если с тем же игроком уже играли.
                //завершить игру нельзя т.к. потеряется ip предложившего
                _model.EmptyGame();
                _view.Repaint();

                var selectDagger = (bool) res;
                string strType = (selectDagger) ? "2" : "1";
                _protocol.PostContinue(strType);

                StartGame(selectDagger);
            }
        }

        /// <summary>
        /// Реакция на приход сообщения ok_ip_{1|2}
        /// </summary>
        private void ProtocolOnConfirmGame(object sender, MessageEventArgs e)
        {
            //текущий клиент ходит крестиком, если другой клиент выбрал нолик т.е. "1"
            bool thisDagger = e.Message == "1";
            StartGame(thisDagger);

            //res == true оппонент играет крестиками, значит текущий игрок ноликами (state == false)
            //res == true оппонент ходит первым, текущий игрок вторым
        }

        private void StartGame(bool thisDagger)
        {
            _model.ThisGamer.PlayState = thisDagger;
            _model.ThisGamer.ThisTurn = thisDagger;

            if (thisDagger)
            {
                _view.Say(Resources.TTTForm_YouGameDagger);
                _view.Say(Resources.TTTForm_YouTurn);
            }
            else
            {
                _view.Say(Resources.TTTForm_YouGameZero);
            }
        }

        private void ProtocolOnNextTurn(object sender, TurnEventArgs e)
        {
            var coord2D = new Point(e.X, e.Y);
            if (!_model.ThisGamer.ThisTurn)
                MakeTurn(coord2D);
            //_view.Repaint();
        }
    }
}