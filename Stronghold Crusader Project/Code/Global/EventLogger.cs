namespace Stronghold_Crusader_Project.Code.Global;

/// <summary>
/// This is a static class so it can be accessed by all other classes
/// this will log events that happened throughout out the code to a text file
/// the point of this is so you can track what is happening in the code
/// </summary>
/// 
public static class EventLogger 
{
    //Class Variables
    private static StreamWriter FileWriter;
    private static Object WriterLock = new object();

    public enum LogType
    {
        Error,
        Warning,
        Debug,
        Info,
        Status,
    }
    
    //Class Methods
    public static void LogEvent(string Log, LogType TypeOfLog) //Method that logs the event
    {
        if (FileWriter != null) //If the log has been started
        {
            lock (WriterLock) //If another thread is writing
            {
                ConsoleColor LogColour = ConsoleColor.White;
                switch (TypeOfLog) //Changes console colour depending on what log type it is
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
                string TimeStamp = DateTime.Now.ToLongTimeString();
                string FullLog = $"[{TimeStamp}] {TypeOfLog.ToString().ToUpper()}: {Log}";
                Console.WriteLine(FullLog);
                Console.ResetColor();
                FileWriter.WriteLine(FullLog);
            }
        }
    }

    #region Log Status
    //when the logging changes like starting and stopping
    public static void StartEventLog() //A method to start the log and delete the old log
    {
        foreach (string ActiveFile in Directory.GetFiles(LogsFolder))
        {
            DateTime TimeCreated = File.GetCreationTime(ActiveFile);
            if (TimeCreated < DateTime.Now.AddDays(-7)) //If the file is older than 7 days
            {
                try //To make sure that any errors deleting files don't cause a crash
                {
                    File.Delete(ActiveFile);
                }
                catch{}
            }
        }
        FileWriter = new StreamWriter(Path.Combine(LogsFolder, DateTime.Now.ToString("dd-MM")) + ".txt", true);
        FileWriter.AutoFlush = true; //Will write instantly to the file
        LogEvent(" Event Log Started \n \n", LogType.Status);
    }

    public static void EndEventLog() //Finish off the log
    {
        LogEvent("Event Log Ended \n \n", LogType.Status);
        FileWriter.Close();
    }
    
    #endregion

}