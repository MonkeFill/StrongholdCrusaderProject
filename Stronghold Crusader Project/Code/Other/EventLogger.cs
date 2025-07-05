using System;
using System.IO;

namespace Stronghold_Crusader_Project.Other;


public static class EventLogger
{
    static string LogFilePath = "Log.txt";
    public enum LogType
    {
        Error,
        Warning,
        Debug,
        Info,
    }

    public static void LogEvent(string Log, LogType TypeOfLog)
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

    public static void StartEventLog()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Event Log Started");
        File.Delete(LogFilePath);
    }
}
