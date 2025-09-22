using Spectre.Console;

namespace AGM.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var modes = new[] { "Server", "Client" };
            var mode = AnsiConsole.Prompt(
                new TextPrompt<string>("Game Mode?")
                    .AddChoices(modes)
                    .DefaultValue("Client"));
            
            System.Console.WriteLine("Game Mode: " + mode);
            
            var networkManager = new NetworkManager();
            
            if (mode == "Server")
            {
                networkManager.StartServer();
                
                System.Console.WriteLine("Server started. Type chat messages below, or press Enter to quit.");
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
                System.Console.WriteLine("Connected to server.");
                
                System.Console.WriteLine("Type chat messages below, or press Enter to quit.");
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