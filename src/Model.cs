using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TicTacToe
{
    /// <summary>
    /// Данные и доступ к ним
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Результат проверки состояния поля
        /// </summary>
        public enum CheckStateResult
        {
            ThisGamerWin, OtherGamerWin, Draw, Process
        }

        //public State GameState { get; set; }

        /// <summary>
        /// толщина поля
        /// </summary>
        public const int WidthCells = 3;

        /// <summary>
        /// высота поля
        /// </summary>
        public const int HeightCells = 3;

        public const int CellsCount = WidthCells*HeightCells;

        public Model()
        {
            EmptyGame();
        }

        public List<Cell> Data { get; private set; }
        public Gamer ThisGamer { get; private set; }

        public CheckStateResult CheckCellsState()
        {
            Func<bool?, CheckStateResult> calcReturn = cell =>
                                         {
                                                 CheckStateResult toReturn = ThisGamer.PlayState == cell
                                                             ? CheckStateResult.ThisGamerWin
                                                             : CheckStateResult.OtherGamerWin;
                                                 return toReturn;
                                         };

            bool?[] f = Data.Select(d => d.State).ToArray();
            if (f[0] == f[1] && f[1] == f[2] && f[0] != null && f[1] != null && f[2] != null) return calcReturn(f[0]);
            if (f[0] == f[3] && f[3]== f[6] && f[0] != null && f[3] != null && f[6] != null) return calcReturn(f[0]);
            if (f[0] == f[4] && f[4]== f[8] && f[0] != null && f[4] != null && f[8] != null) return calcReturn(f[0]);
            if (f[1] == f[4] && f[4]== f[7] && f[1] != null && f[4] != null && f[7] != null) return calcReturn(f[4]);
            if (f[3] == f[4] && f[4]== f[5] && f[3] != null && f[4] != null && f[5] != null) return calcReturn(f[4]);
            if (f[6] == f[4] && f[4]== f[2] && f[6] != null && f[4] != null && f[2] != null) return calcReturn(f[4]);
            if (f[6] == f[7] && f[7]== f[8] && f[6] != null && f[7] != null && f[8] != null) return calcReturn(f[8]);
            if (f[2] == f[5] && f[5]== f[8] && f[2] != null && f[5] != null && f[8] != null) return calcReturn(f[8]);
                
            return  CheckDrawnGame() ? CheckStateResult.Draw : CheckStateResult.Process;
        }

        private bool CheckDrawnGame()
        {
            return Data.All(cell => cell.State != null);
        }

        /// <summary>
        /// Обнулить игру (задать новую)
        /// </summary>
        public void EmptyGame()
        {
            Data = new List<Cell>(CellsCount);

            for (int i = 0; i < CellsCount; i++)
                Data.Add(new Cell(i, null));

            ThisGamer = new Gamer();
            //GameState = State.GameOff;
        }

        public Point Get2DById(int id)
        {
            int x = id % WidthCells;
            int y = id / HeightCells;
            return new Point(x, y);
        }

        public int GetIdBy2D(Point coord)
        {
            return coord.X + coord.Y * WidthCells;
        }

    }
}