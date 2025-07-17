using System.Linq;

namespace Stronghold_Crusader_Project.Code.Mapping;

public class MapHandler
{
    //Static readonly variables that will be used across multiple methods but don't want it to update constantly or keep storing it in every instance
    static readonly int MapHeight = GlobalConfig.MapHeight;
    static readonly int MapWidth = GlobalConfig.MapWidth;
    static readonly string TilesFolderPathFromContent = GlobalConfig.TilesFolderPathFromContent;
    static readonly string TilesFolderFullPath = GlobalConfig.TilesFolderFullPath;
    static readonly int TileReferencePrefixLength = GlobalConfig.TileReferencePrefixLength;
    static readonly string MapFolder = GlobalConfig.MapsFolder;
    static readonly int TileHeight = GlobalConfig.TileHeight;
    static readonly int TileWidth = GlobalConfig.TileWidth;
    
    MapTile[,] Map = new MapTile[MapWidth, MapHeight];
    Dictionary<string, Texture2D> TextureMap = new Dictionary<string, Texture2D>();
    string ActiveMapName;
    string MapPath => Path.Combine(MapFolder, (ActiveMapName + ".json"));
    ContentManager Content;
    public MapHandler(ContentManager InputContent)
    {
        Content = InputContent;
        LoadTextureMap();
    }
    
    

    private void LoadTextureMap()
    {
        LogEvent("Loading texture map started", LogType.Info);
        string[] TileFolders =  Directory.GetDirectories(TilesFolderFullPath); //Getting any of the tile folders
        if (TileFolders.Length == 0) //If it can't find any tile folders
        {
            LogEvent("No tiles folder found", LogType.Error);
            return;
        }
        foreach (string ActiveTileFolder in TileFolders) //Getting any of the tiles within the folder
        {
            string[] TileVariants = Directory.GetFiles(ActiveTileFolder);
            if (TileVariants.Length == 0)
            {
                LogEvent($"No tiles found for {ActiveTileFolder}", LogType.Error);
                continue;
            }
            string FolderName = Path.GetFileName(ActiveTileFolder);
            int TempCount = 0;
            foreach (string ActiveTileVariant in TileVariants)
            {
                if (!ActiveTileVariant.EndsWith(".xnb")) //If the file is not a monogame asset file
                {
                    continue;
                }
                TempCount++;
                string FileName = Path.GetFileNameWithoutExtension(ActiveTileVariant); //getting the name of the file without its extension
                string TextureKeyNumber = FileName.Replace(FolderName, ""); //Getting just the numbers of the file
                string TextureKeyName = FolderName.Substring(0,TileReferencePrefixLength); //Using the folder name
                string TextureKey = TextureKeyName + TextureKeyNumber; //Combining to create a key
                LogEvent($"Accessing {ActiveTileVariant} tile",  LogType.Info);
                if (TextureMap.ContainsKey(TextureKey)) //if the dictionary already has it
                {
                    continue;
                }
                string ActiveTileFromContent = Path.Combine(TilesFolderPathFromContent, FolderName, FileName); //Rebuilding the file path from content to load it in
                TextureMap.Add(TextureKey, Content.Load<Texture2D>(ActiveTileFromContent)); //Adding the texture and key
            }
            if (TempCount == 0) //Check if there are any .xnb files
            {
                LogEvent($"No tiles found for {ActiveTileFolder}", LogType.Error);
                continue;
            }
        }
        LogEvent("Loaded texture map finished" , LogType.Info);
    }
    

    private void SaveMap()
    {
        string[,] BasicMap = new string[MapHeight, MapWidth];
        for (int PositionY = 0; PositionY < MapHeight; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapWidth; PositionX++)
            {
                string ActiveTileKey = Map[PositionY, PositionX].ExportTileKey();
                BasicMap[PositionY, PositionX] = ActiveTileKey;
            }
        }
    }
    private bool ImportMap(string MapName)
    {
        ActiveMapName = MapName;
        if (File.Exists(MapPath)) //Check if the map exists 
        {
            string Json = File.ReadAllText(MapPath);
            string[,] LoadedMap = new string[MapWidth, MapHeight];
            EventLogger.LogEvent($"{MapPath} found and is being loaded", LogType.Info);
            try
            {
                LoadedMap = JsonConvert.DeserializeObject<string[,]>(Json);
                if (ValidMap(LoadedMap)) //If none of the map is null
                {
                    LogEvent($"Map {MapName} has been imported", LogType.Info);
                    return true;
                }
            }
            catch (JsonSerializationException) //If it cannot deserialize it because it is not in the correct format
            {
                LogEvent($"Map {MapName} is not in the correct format and hasn't been loaded", LogType.Error);
            }
            catch (Exception Error)
            {
                LogEvent($"Map {MapName} could not be loaded, {Error.Message}", LogType.Error);
            }
        }
        else
        {
            EventLogger.LogEvent($"Map {MapName} not found", LogType.Error);
        }
        return false;
    }
    
    private bool ValidMap(string[,] LoadedMap) //Checking through all the tiles to make sure they aren't null
    {
        for (int PositionY = 0; PositionY < MapHeight; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapWidth; PositionX++)
            {
                if (LoadedMap[PositionX, PositionY] == null)
                {
                    EventLogger.LogEvent($"tile at ({PositionX},{PositionY}) is invalid", LogType.Error);
                    return false;
                }
            }
        }
        EventLogger.LogEvent($"{ActiveMapName} is a valid map", LogType.Info);
        return true;
    }

    private void LoadMap(string[,] LoadedMap)
    {
        for (int PositionY = 0; PositionY < MapHeight; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapWidth; PositionX++)
            {
                string ActiveTileKey = LoadedMap[PositionX, PositionY];
                Texture2D ActiveTexture = GetTileTexture(ActiveTileKey);
                Vector2 ActivePosition = new Vector2((TileWidth * PositionX), (TileHeight * PositionY));
                Map[PositionX, PositionY].ImportMapTile(ActiveTileKey, ActiveTexture, ActivePosition);
            }
        }
    }

    private Texture2D GetTileTexture(string TileKey)
    {
        if (TextureMap.ContainsKey(TileKey)) //If the dictionary has the tile
        {
            return TextureMap[TileKey];
        }
        LogEvent($"Tile Key {TileKey} could not be found in texture map" , LogType.Error);
        return TextureMap.First().Value;
    }
}

