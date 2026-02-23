namespace AGM.Logging
{
    public interface ILogger
    {
        void Info(string message);
        void Warning(string message);
        void Error(string message);
        void Debug(string message);
    }
}