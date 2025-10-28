namespace Stronghold_Crusader_Project.Code.Global.Other;

public static class EventLogger //Event logger that will log to both console and a text file
{
    //Class Variables
    static StreamWriter FileWrite;
    
    //Enumerated Variables
    public enum LogType //Log types will be outputted in a different colour
    {
        Error,
        Warning,
        Debug,
        Info,
        Status,
    }

    //Methods
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
            case LogType.Status:
                LogColour = ConsoleColor.Magenta;
                break;
        }
        Console.ForegroundColor = LogColour;
        string FullLog = $"[{DateTime.Now}] {TypeOfLog.ToString().ToUpper()}: {Log}";
        Console.WriteLine(FullLog);
        Console.ResetColor();
        //StreamWriter FileWrite = new StreamWriter(LogFilePath, true);
        FileWrite.WriteLine(FullLog);
    }

    public static void StartEventLog() //A method to start the log and delete the old log
    {
        foreach (string ActiveFile in Directory.GetFiles(EventLoggerPath))
        {
            DateTime TimeCreated = File.GetCreationTime(ActiveFile);
            if (TimeCreated < DateTime.Now.AddDays(-7)) //If the file is older than 7 days
            {
                File.Delete(ActiveFile);
            }
        }
        FileWrite = new StreamWriter(Path.Combine(EventLoggerPath, DateTime.Now.ToString("dd-MM")) + ".txt", true);
        LogEvent("Event Log Started", LogType.Status);
    }

    public static void EndEventLog() //Finish off the log
    {
        LogEvent("Event Log Ended", LogType.Status);
        FileWrite.Close();
    }
}
