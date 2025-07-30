namespace Stronghold_Crusader_Project.Code.Other
{
    public static class GlobalConfigManager
    {

        static readonly string GameDataFolder = GlobalConfig.GameDataFolder;
        static readonly string MapsFolder = GlobalConfig.MapsFolder;
        static readonly string SavesFolder = GlobalConfig.SavesFolder;

        public static void InitializeGlobalConfig()
        {
            InitializeKeybinds();
        }
        private static void CheckAllConfigs()
        {
            CheckGameDataFolder();
        }
        private static void InitializeKeybinds()
        {
            AddNewKeybind("MoveUp", Keys.W); 
            AddNewKeybind("MoveDown", Keys.S);
            AddNewKeybind("MoveLeft", Keys.A);
            AddNewKeybind("MoveRight", Keys.D);
            AddNewKeybind("RotateCameraLeft", Keys.Q);
            AddNewKeybind("RotateCameraRight", Keys.E);
            AddNewKeybind("PreviousMenu", Keys.Escape);
        }

        public static void CheckGameDataFolder()
        {
            if (Directory.Exists(GameDataFolder)) //Does exist which it should do
            {
                EventLogger.LogEvent("Found GameData folder", EventLogger.LogType.Info);
                if (Directory.Exists(MapsFolder))
                {
                    EventLogger.LogEvent("Found Maps folder", EventLogger.LogType.Info);
                }
                else
                {
                    CreateMapsFolder();
                }
                if (Directory.Exists(SavesFolder))
                {
                    EventLogger.LogEvent("Found Saves folder", EventLogger.LogType.Info);
                }
                else
                {
                    CreateSavesFolder();
                }
            }
            else
            {
                EventLogger.LogEvent("GameData Folder not found", EventLogger.LogType.Error);
                Directory.CreateDirectory(GameDataFolder);
                EventLogger.LogEvent($"GameData Folder created at {GameDataFolder}", EventLogger.LogType.Info);
                CreateMapsFolder();
                CreateSavesFolder();
            }
        }

        private static void CreateMapsFolder()
        {
            EventLogger.LogEvent("MapsFolder folder not found", EventLogger.LogType.Error);
            Directory.CreateDirectory(MapsFolder);
            EventLogger.LogEvent($"MapsFolder created at {MapsFolder}", EventLogger.LogType.Info);
        }
        private static void CreateSavesFolder()
        {
            EventLogger.LogEvent("SavesFolder folder not found", EventLogger.LogType.Error);
            Directory.CreateDirectory(SavesFolder);
            EventLogger.LogEvent($"SavesFolder created at {SavesFolder}", EventLogger.LogType.Info);
        }
    }
}
