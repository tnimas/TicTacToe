using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class UIGameTTT : Form
    {
        /// <summary>
        /// Размер ячейки
        /// </summary>
        public static readonly Size SizeCell = new Size(100, 100);

        public Model EModel { get; set; }


        public Action<object, TurnEventArgs> FormSetCellState;
        private void OnCallBackSetCellState(TurnEventArgs arg2)
        {
            Action<object, TurnEventArgs> handler = FormSetCellState;
            if (handler != null) handler(this, arg2);
        }

        public Action<object, EventArgs> FormNewGame;
        private void OnCallBackNewGame(EventArgs arg2)
        {
            Action<object, EventArgs> handler = FormNewGame;
            if (handler != null) handler(this, arg2);
        }

        public Action<object, EventArgs> FormExit;
        private void OnCallBackExit(EventArgs arg2)
        {
            Action<object, EventArgs> handler = FormExit;
            if (handler != null) 
                handler(this, arg2);
        }


        #region Методы формы
        public UIGameTTT(Model model)
        {
            EModel = model;
            InitializeComponent();
            
        }

        private void TTTFormLoad(object sender, EventArgs e)
        {
            Size = new Size(SizeCell.Width*Model.WidthCells + SizeCell.Width/2,
                            SizeCell.Height*Model.HeightCells + SizeCell.Height);
            MaximumSize = Size;
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void NewGameToolStripMenuItemClick(object sender, EventArgs e)
        {
            OnCallBackNewGame(new EventArgs());
        }

        private void UIGameTTTFormClosing(object sender, FormClosingEventArgs e)
        {
            OnCallBackExit(new EventArgs());
        }

        /// <summary>
        /// Перерисовка поля
        /// </summary>
        private void BackgroundPaint(object sender, PaintEventArgs e)
        {
            int sizeWidth = SizeCell.Width;
            int sizeHeight = SizeCell.Height;

            for (int i = 0; i < Model.CellsCount; i++)
            {
                Graphics g = e.Graphics;
                Cell cell = EModel.Data[i];

                //берем отчетную точку координаты каждой ячейки, от нее будем рисовать рабочую область и крестики-нолики
                Point coord = EModel.Get2DById(cell.Id);
                coord.X *= SizeCell.Width;
                coord.Y *= SizeCell.Height;

                //рисуем квадрат рабочей области
                var rect = new Rectangle(coord, SizeCell);
                g.DrawRectangle(Pens.Black, rect);

                if (cell.State != null)
                {
                    //рисуем крестик
                    if ((bool) cell.State)
                    {
                        coord = new Point(coord.X + sizeWidth/2, coord.Y + sizeHeight/2);

                        var tl = new Point(coord.X - sizeWidth/2, coord.Y - sizeHeight/2);
                        var tr = new Point(coord.X - sizeWidth/2, coord.Y + sizeHeight/2);
                        var bl = new Point(coord.X + sizeWidth/2, coord.Y - sizeHeight/2);
                        var br = new Point(coord.X + sizeWidth/2, coord.Y + sizeHeight/2);

                        g.DrawLine(new Pen(Color.DodgerBlue, 2), tl, br);
                        g.DrawLine(new Pen(Color.DodgerBlue, 2), tr, bl);
                    }
                    else
                    {
                        //рисуем нолик
                        g.DrawEllipse(new Pen(Color.BlueViolet, 2), rect);
                    }
                }
            }
        }

        /// <summary>
        /// Нажатие на кнопку мыши
        /// </summary>
        private void BackgroundMouseClick(object sender, MouseEventArgs e)
        {

            //получили координаты относительно ячеек
            Point coord2D = Get2DByGround(new Point(e.X, e.Y));
            OnCallBackSetCellState(new TurnEventArgs(coord2D.X,coord2D.Y,true));
            Background.Invalidate();
//            if (Enginer.ThisGamer.ThisTurn)
//                MakeTurn(coord2D);
        }

        protected Point Get2DByGround(Point backgroundPoint)
        {
            int x = backgroundPoint.X / SizeCell.Width;
            int y = backgroundPoint.Y / SizeCell.Height;
            return new Point(x, y);
        }

        public void Repaint()
        {
            Background.Invalidate();
        }

#endregion

    }
}