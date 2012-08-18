using System;

namespace TicTacToe
{
    /// <summary>
    /// ������� �������������� �� ������ ����������������� ���������
    /// ��������:
    /// ������������� ���� ������: 
    /// hello_hostIp
    /// ���������� ������:
    /// ok_clientIp_{1|2} 1-����� 2-�������, ������ ����� ������� 
    /// Y_X - 0..2_0..2 ������_�������
    /// Y_X 
    /// 
    /// � ������ ������ ������ �� ��������, �� ����������
    /// you_loser
    /// �����
    /// ok
    /// </summary>
    public interface ITTTProtocol
    {
        /// <summary>
        /// ����� ������ ���������, ������� ����� ������������� �������� �����
        /// </summary>
        void Init();

        /// <summary>
        /// ���������� ������ ���������
        /// </summary>
        void Exit();

        void CloseGame();

        /// <summary>
        /// ������ hello_hostIp
        /// </summary>
        event Action<object, MessageEventArgs> NewGame;

        /// <summary>
        /// ������ ok_clientIp_{1|2} 1-����� 2-�������, ������ ����� ������� 
        /// </summary>
        event Action<object, MessageEventArgs> ConfirmGame;

        /// <summary>
        /// ������ Y_X - 0..2_0..2 ������_�������
        /// </summary>
        event Action<object, TurnEventArgs> NextTurn;

        /// <summary>
        /// ������ � ������
        /// </summary>
        event Action<object, MessageEventArgs> ErrorMessage;

        /// <summary>
        /// �������� hello_hostIp
        /// </summary>
        /// <param name="clientIp">����� �������� �����</param>
        void PostGoGame(string clientIp);

        /// <summary>
        /// �������� you_loser
        /// </summary>
        void PostYouLoser();

        /// <summary>
        /// �������� Y_X - 0..2_0..2 ������_�������
        /// </summary>
        void PostMyTurn(int x, int y);

        ///<summary>
        /// �������� ok_clientIp_{1|2} 1-����� 2-�������, ������ ����� ������� 
        /// </summary>
        /// <param name="type">"1" - ������� ������ ����� �������, "2" - ���������</param>
        void PostContinue(string type);


    }
}