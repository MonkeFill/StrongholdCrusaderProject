namespace Stronghold_Crusader_Project.Code.Global
{
    public class StartupManager //Startup manager which will check if certain folders exist, load in preferences and initalise everything needed
    {
        //Class Variables
        private string LogBack;
        private LogType LogBackType;
        //Methods

        public void StartGame(ContentManager Content) //Starting everything
        {
            CheckLogFolder(); //First running a check for log folder so then we can start event
            StartEventLog(); //Running event log before everything else so it can log everything being checked
            LogEvent(LogBack, LogBackType); //Logging what happened with log folder as it requires the folder to be found and then eventlog to be started before anything
            CheckAllConfigs(); //Checking all configs and folders
            //Initialising everything that will be needed
            
            //InitializeDefaultKeybinds();
            
        }
        
        public void EndGame() //Ending everything
        {
            EndEventLog();
        }
        private void CheckAllConfigs()
        {
            CheckGameDataFolder();
        }

        public void CheckGameDataFolder() //Checks if the game data folder exists and its sub folders
        {
            if (Directory.Exists(GameDataFolder)) //Does exist which it should do
            {
                EventLogger.LogEvent("Found GameData folder", LogType.Info);
                if (Directory.Exists(MapsFolder))
                {
                    LogEvent("Found Maps folder", LogType.Info);
                }
                else
                {
                    CreateMapsFolder();
                }
                if (Directory.Exists(SavesFolder))
                {
                    LogEvent("Found Saves folder", LogType.Info);
                }
                else
                {
                    CreateSavesFolder();
                }
            }
            else
            {
                LogEvent("GameData Folder not found", LogType.Warning);
                Directory.CreateDirectory(GameDataFolder);
                LogEvent($"GameData Folder created at {GameDataFolder}", LogType.Warning);
                CreateMapsFolder();
                CreateSavesFolder();
            }
        }
        private void CreateMapsFolder() //Creates a maps folder
        {
            LogEvent("MapsFolder folder not found", LogType.Warning);
            Directory.CreateDirectory(MapsFolder);
            LogEvent($"MapsFolder created at {MapsFolder}", LogType.Warning);
        }
        private void CreateSavesFolder() //Creates a saves folder
        {
            LogEvent("SavesFolder folder not found", LogType.Error);
            Directory.CreateDirectory(SavesFolder);
            LogEvent($"SavesFolder created at {SavesFolder}", LogType.Info);
        }

        private void CheckLogFolder() //Checks if a log folder exists
        {
            if (Directory.Exists(EventLoggerPath))
            {
                 LogBack = $"Found Logs folder at {EventLoggerPath}";
                 LogBackType = LogType.Info;
            }
            else
            {
                Directory.CreateDirectory(EventLoggerPath);
                LogBack = $"Logs folder not found at {EventLoggerPath}, created new Logs folder";
                LogBackType = LogType.Warning;
            }
            
        }
    }
}
