namespace Stronghold_Crusader_Project.Code.Mapping;

/// <summary>
/// This is a class that will manage the tiles that the map will use
/// It will load and create tiles
/// </summary>

public class TileLibary
{
    //Class Variables
    private Dictionary<string, TileType> TileTypes = new Dictionary<string, TileType>();
    private Dictionary<string, List<TileType>> TileTypesCategories = new Dictionary<string, List<TileType>>();
    private Random RNG = new Random();
    
    //Class Methods
    public TileLibary(ContentManager Content) 
    {
        LoadTileTypes(Content);
    }
    
    #region Retrieving
    //Public functions that are for retrieving information about the tiles
    
    public TileType GetTileType(string TileName) //Function that will return a single tile through its name
    {
        if (TileTypes.ContainsKey(TileName))
        {
            return TileTypes[TileName];
        }
        LogEvent($"{TileName} Not found", LogType.Warning);
        return null;
    }

    public List<TileType> GetTileTypeCategory(string Category) //Function that will return a list of tiles by its category name
    {
        if (TileTypesCategories.ContainsKey(Category))
        {
            return TileTypesCategories[Category];
        }
        LogEvent($"{Category} Not found", LogType.Warning);
        return null;
    }

    public TileType GetRandomTileType() //A function that will return a random tile
    {
        return TileTypes.ElementAt(RNG.Next(0, TileTypes.Count)).Value;
    }
    
    #endregion
    
    #region Helper Functions
    //Methods that will help the class

    private void LoadTileTypes(ContentManager Content) //A method that will load all the tiles in
    {
        LogEvent("Loading tile types", LogType.Info);
        string FullTilesFolder = Path.Combine(ContentFolder, TilesFolder);
        if (!Directory.Exists(FullTilesFolder)) //If the tiles folder doesn't exist
        {
            LogEvent($"{FullTilesFolder} folder not found", LogType.Error);
            return;
        }
        string[] Categories = Directory.GetDirectories(FullTilesFolder); //Getting all the categories of tiles
        foreach (string ActiveCategory in Categories)
        {
            string ActiveCategoryName = Path.GetFileName(ActiveCategory);
            if (!TileTypesCategories.ContainsKey(ActiveCategoryName)) //if the category doesn't already exist create a new blank list
            {
                TileTypesCategories[ActiveCategoryName] = new List<TileType>();
            }
            string[] Tiles = Directory.GetFiles(ActiveCategory, "*.xnb"); //Getting all the tiles inside that end with .xnb
            foreach (string ActiveTile in Tiles)
            {
                string ActiveTileName = Path.GetFileNameWithoutExtension(ActiveTile);
                bool Walkable = true;
                if (ActiveTileName.Contains(TileNotWalkable)) //Checking if it is a tile that shouldn't be walked on
                {
                    ActiveTileName = ActiveTileName.Replace(TileNotWalkable, "");
                    Walkable = false;
                    
                }
                if (TileTypes.ContainsKey(ActiveTileName)) //Checking if that tile already exists
                {
                    LogEvent($"{ActiveTile} tile conflict with another tile", LogType.Warning);
                    continue;
                }
                try //Incase of any errors when loading textures or trying to add it
                {
                    Texture2D Texture = Content.Load<Texture2D>(Path.Combine(TilesFolder, ActiveCategoryName, ActiveTileName));
                    TileType NewTile = new TileType(ActiveCategoryName, ActiveTileName, Texture, Walkable);
                    TileTypes.Add(ActiveTileName, NewTile);
                    TileTypesCategories[ActiveCategoryName].Add(NewTile);
                }
                catch (Exception error)
                {
                    LogEvent($"Failed to load tile {ActiveTile}, {error.Message}", LogType.Error);
                }
            }
        }
        LogEvent("Loaded tile types successfully", LogType.Info);
    }

    #endregion
}

