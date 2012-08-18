using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using TicTacToe.Properties;

namespace TicTacToe
{
    public partial class UINewGame : Form
    {
        public bool OkGame { get; set; }
        public string EnemyIP { get; set; }

        public UINewGame()
        {
            InitializeComponent();
            
            OkGame = false;
            EnemyIP = ipBox.Text;
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            EnemyIP = null;
            Fin(false);
        }

        private void ConnectButtonClick(object sender, EventArgs e)
        {
            EnemyIP = null;
            try
            {
                IPAddress.Parse(ipBox.Text);
                
                EnemyIP = ipBox.Text;
                Fin(true);
            }
            catch(FormatException)
            {
                MessageBox.Show(Resources.UINewGame_ConnectButtonClick_IncorrectIPFormat);
            }
            
            
        }

        private void Fin(bool result)
        {
            OkGame = result;
            Close();
        }

        private void IPBoxTextChanged(object sender, EventArgs e)
        {
            //EnemyIP = ((TextBox)sender).Text;
        }
    }
}
