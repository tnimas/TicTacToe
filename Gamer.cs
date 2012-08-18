namespace TicTacToe
{
    public class Gamer
    {
        /// <summary>
        /// false - игрок играет ноликами, true - крестиками, null - еще не определено
        /// </summary>
        public bool? PlayState;

        /// <summary>
        /// —ейчас ходит данный игрок (true), оппонент - false
        /// </summary>
        public bool ThisTurn;
    }
}