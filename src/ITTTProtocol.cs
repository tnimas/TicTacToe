using System;

namespace TicTacToe
{
    /// <summary>
    /// Сетевые взаимодействия на уровне пользовательского протокола
    /// протокол:
    /// Запрашивающий игру клиент: 
    /// hello_hostIp
    /// Отвечающий клиент:
    /// ok_clientIp_{1|2} 1-нолик 2-крестик, первый ходит крестик 
    /// Y_X - 0..2_0..2 строка_столбец
    /// Y_X 
    /// 
    /// В случае победы одного из клиентов, он отправляет
    /// you_loser
    /// Ответ
    /// ok
    /// </summary>
    public interface ITTTProtocol
    {
        /// <summary>
        /// старт работы протокола, включая старт прослушивания игрового порта
        /// </summary>
        void Init();

        /// <summary>
        /// Завершение работы протокола
        /// </summary>
        void Exit();

        void CloseGame();

        /// <summary>
        /// пришло hello_hostIp
        /// </summary>
        event Action<object, MessageEventArgs> NewGame;

        /// <summary>
        /// пришло ok_clientIp_{1|2} 1-нолик 2-крестик, первый ходит крестик 
        /// </summary>
        event Action<object, MessageEventArgs> ConfirmGame;

        /// <summary>
        /// пришло Y_X - 0..2_0..2 строка_столбец
        /// </summary>
        event Action<object, TurnEventArgs> NextTurn;

        /// <summary>
        /// Ошибка в работе
        /// </summary>
        event Action<object, MessageEventArgs> ErrorMessage;

        /// <summary>
        /// отправка hello_hostIp
        /// </summary>
        /// <param name="clientIp">адрес текущего компа</param>
        void PostGoGame(string clientIp);

        /// <summary>
        /// отправка you_loser
        /// </summary>
        void PostYouLoser();

        /// <summary>
        /// отправка Y_X - 0..2_0..2 строка_столбец
        /// </summary>
        void PostMyTurn(int x, int y);

        ///<summary>
        /// отправка ok_clientIp_{1|2} 1-нолик 2-крестик, первый ходит крестик 
        /// </summary>
        /// <param name="type">"1" - текущий клиент ходит ноликом, "2" - крестиком</param>
        void PostContinue(string type);


    }
}