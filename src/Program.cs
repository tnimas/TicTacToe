using System;
using System.Windows.Forms;

namespace TicTacToe
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

//           try
//           {
                var game = new Game();
                game.Init();
//            
//            }
//            catch (Exception e)
//            {
//                throw;
//                MessageBox.Show(e.Message);
//            }
        }
    }
}
