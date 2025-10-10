namespace Stronghold_Crusader_Project.Code.Global
{
    public static class GlobalConfigManager //Config manager for global config to make sure all its data is there
    {
        //Class Variables
        static readonly string GameDataFolder = GlobalConfig.GameDataFolder;
        static readonly string MapsFolder = GlobalConfig.MapsFolder;
        static readonly string SavesFolder = GlobalConfig.SavesFolder;
        
        //Methods
        public static void InitializeGlobalConfig()
        {
            InitializeDefaultKeybinds();
        }
        private static void CheckAllConfigs()
        {
            CheckGameDataFolder();
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
