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
        /// ����� ������
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// false - �����, true - �������, null - ������ ������
        /// </summary>
        public bool? State { get; set; }
    }
}