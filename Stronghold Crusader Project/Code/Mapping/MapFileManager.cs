namespace Stronghold_Crusader_Project.Code.Mapping;

public class MapFileManager //Class that will handle any map file operations
{
    //Class Variables
    private MapHandler MapHandling;
    private string MapPath => MapHandling.MapPath;
    private MapTile[,] Map => MapHandling.Map;
    private Dictionary<string, Texture2D> TextureMap => MapHandling.TextureMap;
    private string MapName => MapHandling.ActiveMapName;
    
    //Methods
    public MapFileManager(MapHandler Handler) //Initializer
    {
        MapHandling = Handler;
    }
    
    public string[,] ImportMap() //Will import a map 
    {
        if (File.Exists(MapPath)) //Check if the map exists 
        {
            string Json = File.ReadAllText(MapPath);
            string[,] LoadedMap;
            LogEvent($"{MapPath} found and is being loaded", LogType.Info);
            try
            {
                LoadedMap = JsonConvert.DeserializeObject<string[,]>(Json);
                if (ValidMap(LoadedMap)) //If none of the map is null
                {
                    LogEvent($"Map {MapName} has been imported", LogType.Info);
                    return LoadedMap;
                }
            }
            catch (JsonSerializationException) //If it cannot deserialize it because it is not in the correct format
            {
                LogEvent($"Map {MapName} is not in the correct format and hasn't been loaded", LogType.Error);
            }
            catch (Exception Error) //Any other error that may happen
            {
                LogEvent($"Map {MapName} could not be loaded, {Error.Message}", LogType.Error);
            }
        }
        else //Map doesn't exist
        {
            LogEvent($"Map {MapName} not found", LogType.Error);
        }
        return null;
    }
    
    public void ExportMap(string[,] ExportMap) //Export the basic key tile grid to a file
    {
        if (File.Exists(MapPath))
        {
            LogEvent($"{MapPath} already exists!", LogType.Warning);
        }
        string Json = JsonConvert.SerializeObject(ExportMap, Formatting.Indented);
        File.WriteAllText(MapPath, Json);
        LogEvent($"Map {MapName} saved to {MapPath}", LogType.Info);
    }
    
    public string[,] SaveMap() //Save the map meaning it will turn the Map tiles into a basic grid of tile keys
    {
        
        string[,] BasicMap = new string[MapHeight, MapWidth];
        MapHandling.LoopThroughTiles((PositionX, PositionY) =>
        {
            BasicMap[PositionY, PositionX] = Map[PositionY, PositionX].TileKey;
        });
        return BasicMap;
    }
    
    public void LoadMap(String[,] LoadedMap) //Load Map will turn the basic grid of tile keys into actual tiles 
    {
        MapHandling.LoopThroughTiles((PositionX, PositionY) =>
        {
            string ActiveTileKey = LoadedMap[PositionY, PositionX];
            Texture2D ActiveTexture = GetTileTexture(ActiveTileKey);
            Vector2 ActivePosition = new Vector2(PositionX, PositionY);
            Map[PositionY, PositionX] = new MapTile(ActiveTileKey, ActiveTexture, ActivePosition);
        });
        LogEvent($"Map {MapName} has been loaded, here is the map \n {MapAsText(LoadedMap)}", LogType.Info);
    }
    
    private bool ValidMap(string[,] LoadedMap) //Checking through all the tiles to make sure they aren't null
    {
        bool Valid = true;
        MapHandling.LoopThroughTiles((PositionX, PositionY) =>
        {
            if (string.IsNullOrWhiteSpace(LoadedMap[PositionY, PositionX]) )
            {
                EventLogger.LogEvent($"tile at ({PositionX},{PositionY}) is invalid", LogType.Error);
                Valid = false;
            }
        });
        EventLogger.LogEvent($"{MapName} is a valid map", LogType.Info);
        return Valid;
    }
    
    private string MapAsText(string[,] Map) //Convert to a string so you are able to read the map easily and for debugging
    {
        StringBuilder MapText = new StringBuilder();
        MapHandling.LoopThroughTiles((PositionX, PositionY) =>
        {
            MapText.Append(Map[PositionY, PositionX]);
            if (PositionX == MapWidth-1)
            {
                MapText.AppendLine();
                MapText.AppendLine();
            }
            else
            {
                MapText.Append(",");
            }
        });
        return MapText.ToString();
    }
    
    private Texture2D GetTileTexture(string TileKey) //Method to get the texture of a tile based on its key
    {
        if (TextureMap.ContainsKey(TileKey)) //If the dictionary has the tile
        {
            return TextureMap[TileKey];
        }
        LogEvent($"Tile Key {TileKey} could not be found in texture map" , LogType.Error);
        return TextureMap.First().Value;
    }
}

