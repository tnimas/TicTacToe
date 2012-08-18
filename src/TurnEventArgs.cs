using System;

namespace TicTacToe
{
    public class TurnEventArgs :EventArgs
    {
        public TurnEventArgs(int x, int y, bool thisGamer) 
        {
            X = x;
            Y = y;
            ThisGamer = thisGamer;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public bool ThisGamer { get; set; }
    }
}