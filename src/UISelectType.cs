using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class UISelectType : Form
    {
        public UISelectType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// false - нолик, true - крестик, null - отказ
        /// </summary>
        public bool? ResultState { get; set; }

        private void DaggerGameButtonClick(object sender, EventArgs e)
        {
            Fin(true);
        }

        public void SetCaption(string caption)
        {
            InfLabel.Text = caption;
        }

                private void CancelButtonClick(object sender, EventArgs e)
        {
            Fin(null);
        }

        private void ZeroGameButtonClick(object sender, EventArgs e)
        {
            Fin(false);
        }

        public void Fin(bool? state)
        {
            ResultState = state;
            Close();
        }


    }
}
