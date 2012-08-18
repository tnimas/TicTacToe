using System;
using System.Drawing;

namespace TicTacToe
{
    /// <summary>
    /// Методы представления, предполагающие независимость реализации интерфейса для заданного порядка действий. 
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Предложение пользователю ввести IP противника, пользователь может отказаться
        /// </summary>
        /// <returns>IP противника или  null в случае отмены</returns>
        string GetOtherIpOnUser();

        /// <summary>
        /// Предложение пользователю выбрать чем он будет играть
        /// </summary>
        /// <param name="otherIp">IP игрока, предлагающего игру</param>
        /// <returns>false - нолик, true - крестик, null - отмена предложения</returns>
        bool? NewGameProposal(string otherIp);

        /// <summary>
        /// Инициализация представления
        /// </summary>
        /// <param name="model">данные</param>
        void Init(Model model);

        /// <summary>
        /// Попытка пользователя сделать ход. 
        /// Координаты нажатоой ячейки содержатся в TurnEventArgs в 2d cells формате
        /// </summary>
        event Action<object, TurnEventArgs> CallBackSetCellState;

        /// <summary>
        /// Попытка текущего пользователя начать новую игру
        /// </summary>
        event Action<object, EventArgs> CallBackNewGame;

        /// <summary>
        /// Попытка текущего пользователя выйти из программы
        /// </summary>
        event Action<object, EventArgs> CallBackExit;

        /// <summary>
        /// Отправка типового сообщения пользователю
        /// </summary>
        /// <param name="message">сообщение</param>
        void Say(string message);

        void Repaint();
    }
}