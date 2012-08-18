

namespace TicTacToe
{
    /// <summary>
    /// Реализация singleton фабрики классов для приложения
    /// </summary>
    public class Factory
    {

        public static Factory Instance { get; private set; }

        static Factory()
        {
            Instance = new Factory();
        }

        public IWorker CreateWorker()
        {
             return new TcpWorker();
        }

        public IView CreateView()
        {
            return new View();
        }

        public ITTTProtocol CreateTTTProtocol()
        {
            return new TTTProtocol();
        }

        public IExtensions CreateExtensions()
        {
            return new Extensions();
        }

    }
}
