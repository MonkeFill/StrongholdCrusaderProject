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
    public static int BorderHeight = 7 * TileHeight;
    public static int BorderWidth = 5 * TileWidth;
    public static int MapTotalHeight = (MapHeight - 1) * (TileHeight / 2);
    public static int MapTotalWidth = MapWidth * TileWidth; 
    public static int MaxMapHeight => GetMaxMapHeight();
    public static int MaxMapWidth => GetMaxMapWidth();
    
    //Camera Variables
    public static float MaxZoom = 2f;
    public static float ZoomSensitivity = 0.01f;
    public static float MovementAmount = 200f;
    public static float MovementSpeed = 150f;
    private static float Degree90InPi = MathHelper.PiOver2;
    private static float Degree270InPi = 3 * MathHelper.PiOver2;
    private static float Degree90Difference => Math.Abs(Rotation - Degree90InPi);
    private static float Degree270Difference => Math.Abs(Rotation - Degree270InPi);
    static float DegreeDifferenceTolerance = 0.1f;
    public static float RotationAmount = Degree90InPi;
    public static bool MapVertical => MapIsVertical();
    
    //Methods
    private static int GetMaxMapHeight()
    {
        if (MapVertical)
        {
            return MapTotalWidth + (BorderHeight * 2);
        }
        return MapTotalHeight + (BorderHeight * 2);
    }
    private static int GetMaxMapWidth()
    {
        if (MapVertical)
        {
            return MapTotalHeight + (BorderWidth * 2);
        }
        return MapTotalWidth + (BorderWidth * 2);
    }

    public static bool MapIsVertical()
    {
        if (Degree90Difference < DegreeDifferenceTolerance || Degree270Difference < DegreeDifferenceTolerance)
        {
            return true;
        }
        return false;
    }
}
