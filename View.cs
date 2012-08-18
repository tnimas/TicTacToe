using System;
using System.Windows.Forms;
using TicTacToe.Properties;

namespace TicTacToe
{
    public class View : IView
    {

        private UIGameTTT _generalField;
        private UINewGame _newCameForm;

        public string GetOtherIpOnUser()
        {
            _newCameForm.OkGame = false;
            _newCameForm.EnemyIP = null;

            _newCameForm.ShowDialog();
            if (_newCameForm.OkGame)
            {
                string otherIp = _newCameForm.EnemyIP;
                return otherIp;
            }
            return null;
        }

        public bool? NewGameProposal(string otherIp)
        {
            string msg = Resources.UIGameTTT_OnGoNewGame_Client_with_IP + otherIp;
            var selectTypeForm = new UISelectType();
            selectTypeForm.SetCaption(msg);
            selectTypeForm.ShowDialog();
            bool? res = selectTypeForm.ResultState;
            return res;
        }

        public void Init(Model model)
        {
            _generalField = new UIGameTTT(model)
                                {
                                    FormNewGame = CallBackNewGame,
                                    FormExit = CallBackExit,
                                    FormSetCellState = CallBackSetCellState
                                };

            _newCameForm = new UINewGame();
            Application.Run(_generalField);
        }

        public event Action<object, TurnEventArgs> CallBackSetCellState;
        public event Action<object, EventArgs> CallBackNewGame;
        public event Action<object, EventArgs> CallBackExit;
        
        public void Say(string message)
        {
            MessageBox.Show(message);
        }

        public void Repaint()
        {
            _generalField.Repaint();
        }
    }
}
