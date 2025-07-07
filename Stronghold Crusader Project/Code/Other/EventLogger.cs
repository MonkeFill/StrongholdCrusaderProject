namespace Stronghold_Crusader_Project.Code.Other;

public static class EventLogger //Event logger that will log to both console and a textfile
{
    static string LogFilePath = "Log.txt"; //Default path to where log.txt will be
    public enum LogType //Log types will be outputted in a different colour
    {
        Error,
        Warning,
        Debug,
        Info,
    }

    public static void LogEvent(string Log, LogType TypeOfLog) //Method to create an event for it to log
    {
        ConsoleColor LogColour =  ConsoleColor.White;
        switch (TypeOfLog)
        {
            case LogType.Error:
                LogColour = ConsoleColor.Red;
                break;
            case LogType.Warning:
                LogColour = ConsoleColor.Yellow;
                break;
            case LogType.Debug:
                LogColour = ConsoleColor.Green;
                break;
            case LogType.Info:
                LogColour = ConsoleColor.Cyan;
                break;
        }
        Console.ForegroundColor = LogColour;
        string FullLog = $"[{DateTime.Now}] {TypeOfLog.ToString().ToUpper()}: {Log}";
        Console.WriteLine(FullLog);
        Console.ResetColor();
        StreamWriter FileWrite = new StreamWriter(LogFilePath, true);
        FileWrite.WriteLine(FullLog);
        FileWrite.Close();
    }

    public static void StartEventLog() //A method to start the log and delete the old log
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Event Log Started");
        File.Delete(LogFilePath);
    }
}
