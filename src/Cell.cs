namespace TicTacToe
{
    public class Cell
    {
        public Cell(int id, bool? state)
        {
            Id = id;
            State = state;
        }

        /// <summary>
        /// Номер ячейки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// false - нолик, true - крестик, null - пустая клетка
        /// </summary>
        public bool? State { get; set; }
    }
}