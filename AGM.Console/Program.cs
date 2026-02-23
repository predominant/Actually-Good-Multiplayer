using AGM;
using Spectre.Console;

namespace AGM.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Initialize global logger
            Logger.Initialize(new Logging.ConsoleLogger());

            var modes = new[] { "Server", "Client" };
            var mode = AnsiConsole.Prompt(
                new TextPrompt<string>("Game Mode?")
                    .AddChoices(modes)
                    .DefaultValue("Client"));

            Logger.Info("Game Mode: " + mode);

            var networkManager = new NetworkManager(Logger.Instance);

            if (mode == "Server")
            {
                networkManager.StartServer();

                Logger.Info("Server started. Type chat messages below, or press Enter to quit.");
                string input;
                while ((input = System.Console.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        break;
                    }
                    networkManager.SendChatMessage(input);
                }

                networkManager.StopServer();
            }
            else if (mode == "Client")
            {
                var ip = AnsiConsole.Prompt(
                    new TextPrompt<string>("Server IP?")
                        .DefaultValue("127.0.0.1"));
                var port = AnsiConsole.Prompt(
                    new TextPrompt<int>("Server Port?")
                        .DefaultValue(6969));
                networkManager.ConnectToServer(ip, port);
                Logger.Info("Connected to server.");

                Logger.Info("Type chat messages below, or press Enter to quit.");
                string input;
                while ((input = System.Console.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        break;
                    }
                    networkManager.SendChatMessage(input);
                }

                networkManager.DisconnectClient();
            }
        }
    }
}