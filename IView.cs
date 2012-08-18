using System;
using System.Drawing;

namespace TicTacToe
{
    /// <summary>
    /// ������ �������������, �������������� ������������� ���������� ���������� ��� ��������� ������� ��������. 
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// ����������� ������������ ������ IP ����������, ������������ ����� ����������
        /// </summary>
        /// <returns>IP ���������� ���  null � ������ ������</returns>
        string GetOtherIpOnUser();

        /// <summary>
        /// ����������� ������������ ������� ��� �� ����� ������
        /// </summary>
        /// <param name="otherIp">IP ������, ������������� ����</param>
        /// <returns>false - �����, true - �������, null - ������ �����������</returns>
        bool? NewGameProposal(string otherIp);

        /// <summary>
        /// ������������� �������������
        /// </summary>
        /// <param name="model">������</param>
        void Init(Model model);

        /// <summary>
        /// ������� ������������ ������� ���. 
        /// ���������� �������� ������ ���������� � TurnEventArgs � 2d cells �������
        /// </summary>
        event Action<object, TurnEventArgs> CallBackSetCellState;

        /// <summary>
        /// ������� �������� ������������ ������ ����� ����
        /// </summary>
        event Action<object, EventArgs> CallBackNewGame;

        /// <summary>
        /// ������� �������� ������������ ����� �� ���������
        /// </summary>
        event Action<object, EventArgs> CallBackExit;

        /// <summary>
        /// �������� �������� ��������� ������������
        /// </summary>
        /// <param name="message">���������</param>
        void Say(string message);

        void Repaint();
    }
}