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

    public void MapExportHandler()
    {
        string[,] SavedMap = SaveMap();
        ExportMap(SavedMap);
    }

    public void MapImportHandler(string MapName)
    {
        ActiveMapName = MapName;
        string[,] ImportedMap = ImportMap();
        if (ImportedMap != null)
        {
            LoadMap(ImportedMap);
        }
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
    private string[,] ImportMap() //Will import a map 
    {
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
                    LogEvent($"Map {ActiveMapName} has been imported", LogType.Info);
                    return LoadedMap;
                }
            }
            catch (JsonSerializationException) //If it cannot deserialize it because it is not in the correct format
            {
                LogEvent($"Map {ActiveMapName} is not in the correct format and hasn't been loaded", LogType.Error);
            }
            catch (Exception Error)
            {
                LogEvent($"Map {ActiveMapName} could not be loaded, {Error.Message}", LogType.Error);
            }
        }
        else
        {
            EventLogger.LogEvent($"Map {ActiveMapName} not found", LogType.Error);
        }
        return null;
    }
    
    public void ExportMap(string[,] ExportMap) //Export the basic key tile grid to a file
    {
        if (File.Exists(MapPath))
        {
            EventLogger.LogEvent($"{MapPath} already exists!", EventLogger.LogType.Warning);
        }
        string Json = JsonConvert.SerializeObject(ExportMap, Formatting.Indented);
        File.WriteAllText(MapPath, Json);
        EventLogger.LogEvent($"Map {Json} saved to {MapPath}", EventLogger.LogType.Info);
    }
    
    private bool ValidMap(string[,] LoadedMap) //Checking through all the tiles to make sure they aren't null
    {
        bool Valid = true;
        LoopThroughTiles((PositionX, PositionY) =>
        {
            if (string.IsNullOrWhiteSpace(LoadedMap[PositionX, PositionY]) )
            {
                EventLogger.LogEvent($"tile at ({PositionX},{PositionY}) is invalid", LogType.Error);
                Valid = false;
            }
        });
        EventLogger.LogEvent($"{ActiveMapName} is a valid map", LogType.Info);
        return Valid;
    }

    private void LoadMap(string[,] LoadedMap) //Load Map will turn the basic grid of tile keys into actual tiles 
    {
        LoopThroughTiles((PositionX, PositionY) =>
        {
            string ActiveTileKey = LoadedMap[PositionX, PositionY];
            Texture2D ActiveTexture = GetTileTexture(ActiveTileKey);
            Vector2 ActivePosition = new Vector2(PositionX, PositionY);
            Map[PositionX, PositionY] = new MapTile(ActiveTileKey, ActiveTexture, ActivePosition);
        });
        LogEvent($"Map {ActiveMapName} has been loaded, here is the map \n {MapAsText()}", LogType.Info);
        Console.WriteLine("");
    }
    
    private string[,] SaveMap() //Save the map meaning it will turn the Map tiles into a basic grid of tile keys
    {
        
        string[,] BasicMap = new string[MapHeight, MapWidth];
        LoopThroughTiles((PositionX, PositionY) =>
        {
            BasicMap[PositionX, PositionY] = Map[PositionX, PositionY].TileKey;
        });
        return BasicMap;
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

    private string MapAsText()
    {
        StringBuilder MapText = new StringBuilder();
        LoopThroughTiles((PositionX, PositionY) =>
        {
           MapText.Append(Map[PositionX, PositionY].TileKey);
           if (PositionX == MapWidth-1)
           {
               MapText.AppendLine();
           }
           else
           {
               MapText.Append(",");
           }
        });
        return MapText.ToString();
    }

    private void LoopThroughTiles(Action<int, int> ActionToDo) //A method to loop through all the tiles in a map and perform actions on them
    {
        for (int PositionY = 0; PositionY < MapHeight; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapWidth; PositionX++) //Loop through all the tiles
            {
                ActionToDo(PositionX, PositionY); //Execute the action for that specific tile
            }
        }
    }
    
    /*Template Lambda expression to use the method, must use lambda line to create a method inside a method but don't want to actually separate it
    using the lambda it will run pieces of code after it for each x and y
    LoopThroughTiles((PositionX, PositionY) =>
    {
    Any code you want to run here
    });
    end*/

    public void DrawMap(SpriteBatch ActiveSpriteBatch)
    {
        LoopThroughTiles((PositionX, PositionY) =>
        {
            Map[PositionX, PositionY].Draw(ActiveSpriteBatch);
        });
    }
}

