using AGM;

namespace AGM.Server
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Initialize global logger and pass it to NetworkManager
            Logger.Initialize(new Logging.ConsoleLogger());
            var networkManager = new NetworkManager(Logger.Instance);

            networkManager.StartServer();

            Logger.Info("Server started. Type chat messages below, or press Enter to quit.");
            string input;
            while ((input = System.Console.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(input)) break;
                networkManager.SendChatMessage(input);
            }

            networkManager.StopServer();
        }
    }
}