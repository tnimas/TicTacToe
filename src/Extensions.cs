using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe
{
    public class Extensions : IExtensions
    {

        public void AppInit()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public void AppExit()
        {
            Application.Exit();
        } 
    }

    public interface IExtensions
    {
        void AppInit();
        void AppExit();
    }
}
