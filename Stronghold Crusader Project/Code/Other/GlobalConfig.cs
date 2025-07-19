namespace Stronghold_Crusader_Project.Code.Other;

public static class GlobalConfig
{
    //Folder Paths
    private static string CurrentDirectory = Environment.CurrentDirectory; //Debug folder 
    private static string DefaultFolder = Directory.GetParent(CurrentDirectory).Parent.Parent.FullName; //Directory of where the .csproj is 
    public static string GameDataFolder = Path.Combine(DefaultFolder, "GameData"); 
    public static string MapsFolder = Path.Combine(GameDataFolder, "Maps");
    public static string SavesFolder = Path.Combine(GameDataFolder, "Saves");
    private static string ContentFolder = Path.Combine(DefaultFolder, "Content");
    public static string TilesFolderPathFromContent = "Assets/Tiles";
    public static string TilesFolderFullPath = Path.Combine(ContentFolder, "bin","DesktopGL", TilesFolderPathFromContent);
    
    //Map Variables
    public static int MapHeight = 100;
    public static int MapWidth = 100;
    public static int TileHeight = 32;
    public static int TileWidth = 64;
    public static int TileReferencePrefixLength = 1;
    public static int BorderHeight = 200;
    public static int BorderWidth = 200;
    public static int MaxMapHeight = ((MapHeight * TileHeight) /2) +(BorderHeight * 2);
    public static int MaxMapWidth = (MapWidth * TileWidth) + (BorderWidth * 2);
    
    //Camera Variables
    public static float MaxZoom = 2f;
    public static float ZoomSensitivity = 0.01f;
    public static float MovementAmount = 200f;
    public static float MovementSpeed = 150f;
    public static float RotationAmount = MathHelper.ToRadians(90);
    
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
