namespace Stronghold_Crusader_Project.Code.Global;

public static class GlobalConfig //Method to store global variables and paths that will be used across multiple classes
{
    //Folder Paths
    private static string CurrentDirectory = Environment.CurrentDirectory; //Debug folder 
    private static string DefaultFolder = Directory.GetParent(CurrentDirectory).Parent.Parent.FullName; //Directory of where the .csproj is 
    public static string GameDataFolder = Path.Combine(DefaultFolder, "GameData"); 
    public static string MapsFolder = Path.Combine(GameDataFolder, "Maps");
    public static string SavesFolder = Path.Combine(GameDataFolder, "Saves");
    private static string ContentFolder = Path.Combine(DefaultFolder, "Content");
    private static string MappingFolder = Path.Combine("Assets/Mapping");
    public static string TilesFolderPathFromContent = Path.Combine(MappingFolder, "Tiles");
    public static string TilesFolderFullPath = Path.Combine(ContentFolder, "bin","DesktopGL", "Content", TilesFolderPathFromContent);
    public static string EventLoggerPath = Path.Combine(DefaultFolder, "Logs");
    private static string UIElementFolder = Path.Combine(ContentFolder, "UI");
    public static string MainMenuFolder = Path.Combine(UIElementFolder, "MainMenu");
    public static string ButtonsFolder = Path.Combine(UIElementFolder, "Buttons");
    
    //Map Variables
    public static int MapWidth = 100;
    public static int MapHeight = 100;
    public static int TileHeight = 16;
    public static int TileWidth = 32;
    public static int TileReferencePrefixLength = 1;
    public static int RealMapHeight = TileHeight + ((MapHeight - 1) * (TileHeight / 2));
    public static int RealMapWidth = MapWidth * TileWidth + (TileWidth / 2); 
    public static int MapHeightSize => GetMaxMapHeight();
    public static int MapWidthSize => GetMaxMapWidth();
    public static int TotalMapHeight => MapHeightSize + (BorderHeight * 2) - (TileHeight / 2);
    public static int TotalMapWidth => MapWidthSize + (BorderWidth * 2) - (TileWidth / 2);
    
    //Border Variables
    public static int BorderHeight = 10 * TileHeight;
    public static int BorderWidth = Convert.ToInt32(TileWidth * 2.5);
    public static string BorderPath =  Path.Combine(MappingFolder, "Borders");
    public static string DefaultBorderTexture = "DefaultBorder";
    public static string TopSmallBorderTexture = "BorderTopSmall";
    public static string SideSmallBorderTexture = "BorderSideSmall";
    public static string CornerBorderTexture = "BorderCorner";
    
    //Camera Variables
    public static float MaxZoom = 3.5f;
    public static float ZoomSensitivity = 0.1f;
    public static float MovementAmount = 200f;
    public static float MovementSpeed = 75;
    public static float RotationAmount = MathHelper.PiOver2;
    public static bool MapVertical => MapIsVertical();
    
    //Methods
    private static int GetMaxMapHeight()
    {
        if (MapVertical)
        {
            return RealMapWidth;
        }
        return RealMapHeight;
    }
    private static int GetMaxMapWidth()
    {
        if (MapVertical)
        {
            return RealMapHeight;
        }
        return RealMapWidth;
    }

    public static bool MapIsVertical()
    {
        int CameraDegrees = GetCameraRotationDegrees();
        if (CameraDegrees == 90 || CameraDegrees == 270)
        {
            return true;
        }
        return false;
    }
}
