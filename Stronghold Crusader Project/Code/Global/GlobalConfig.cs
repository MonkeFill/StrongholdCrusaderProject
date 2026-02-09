namespace Stronghold_Crusader_Project.Code.Global;

/// <summary>
/// Global Config is a static class so that it can be accessed by every other class throughout the code
/// it functions as an easy way to access variables that are used across multiple classes and change them
/// files are const if they don't change at runtime but if they do they use readonly instead to prevent them being overwritten
/// </summary>

public static class GlobalConfig{
    #region System Paths
    //Any path that is based off where the application is being run in
    private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory; //Where the application itself is in (debug folder)
    private static readonly string GameDataFolder = Path.Combine(BaseDirectory, "GameData"); //Where data like settings, maps, saves etc are saved
    public static readonly string ContentFolder = Path.Combine(BaseDirectory, "Content"); //Where all the assets sit 
    
    #endregion
    
    #region Asset Paths
    //Any paths that are for loading assets in 
    private const string AssetsFolder = "Assets";
    private static readonly string UserInterfaceFolder = Path.Combine(AssetsFolder, "UI");
    private static readonly string MappingFolder = Path.Combine(AssetsFolder, "Mapping");
    public static readonly string TilesFolder = Path.Combine(MappingFolder, "Tiles");
    public static readonly string BorderFolder = Path.Combine(MappingFolder, "Borders");
    public static readonly string MenusFolder = Path.Combine(UserInterfaceFolder, "Menus");
    public static readonly string GlobalMenuFolder = Path.Combine(MenusFolder, "Global");
    public static readonly string MenuBoxFolder = Path.Combine(GlobalMenuFolder, "Box");
    public static readonly string GlobalButtonFolder = Path.Combine(GlobalMenuFolder, "Buttons");
    public static readonly string UnitsFolder = Path.Combine(AssetsFolder, "Units");
    
    #endregion
    
    #region Game Data Paths
    //Any paths that only rely on the game data folder
    public static readonly string MapsFolder = Path.Combine(GameDataFolder, "Maps"); //where maps are stored
    public static readonly string SavesFolder = Path.Combine(GameDataFolder, "Saves"); //Where game saves are stored
    public static readonly string LogsFolder = Path.Combine(GameDataFolder, "Logs"); //Where to store logs
    public static readonly string KeybindsFile = Path.Combine(GameDataFolder, "Keybinds.json");
    
    #endregion
    
    #region Map Settings
    //Variables that are required for mapping
    public static readonly Point MapDimensions = new Point(50, 50);
    public static readonly Point TileSize = new Point(32, 32);
    public static readonly Point MapSize = new Point(MapDimensions.X * TileSize.X, MapDimensions.Y * TileSize.Y);
    public static readonly Point BorderSize = new Point((int)(TileSize.X * 2.5),(int)5 * TileSize.Y);
    public const string DefaultBorderName = "DefaultBorder";
    public const string CornerBorderName = "BorderCorner";
    public const string ShortBorderName = "BorderTopSmall";
    public const string NarrowBorderName = "BorderSideSmall";
    
    #endregion
    
    #region CameraSettings
    //Variables that are required for the camera
    public const float ZoomSensitivity = 0.1f; //How fast you can zoom into the map
    public const float MovementAmount = 250f; //How much the camera moves by
    public const float MovementSpeed = 100f; //How fast the camera moves
    public const float ZoomDelta = 120f;
    public const float RotationAmount = MathHelper.PiOver2;
    
    #endregion
    
    #region File Addons
    //Any addon to files like extensions or what they end with
    public const string HoverAddon = "_Hover";
    public const string ActiveAddon = "_Active";
    public const string LockedAddon = "_Locked";
    public const string AvailableAddon = "_Available";
    public const string UnavailableAddon = "_Unavailable";
    public const string MapFileExtension = ".json";
    public const string GameFileExtension = ".json";
    public const string TileNotWalkable = "_Block";
    public const string MonoGameAddon = ".xnb";
    
    #endregion
    
    #region User Interface
    //Variables that are required for the user interface
    public const int VirtualScreenWidth = 1024; //What screen size the UI is made for
    public const int VirtualScreenHeight = 768;
    public const int FontSize = 12;
    public const int BoxBigSize = 24;
    public const int BoxSmallSize = 8;
    public const bool ScaleUI = false;

    #endregion

    #region Other
    //Paths that don't fit in other categories

    public const string UnitAnimationName = "Frame";
    public const double AnimationFrameSpeed = 0.1;
    public const int PFStraightCost = 10;
    public const int PFDiagonalCost = 14;
    public const bool DebugPathfinding = false;

    #endregion
}