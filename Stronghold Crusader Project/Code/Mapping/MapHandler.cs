namespace Stronghold_Crusader_Project.Code.Mapping;

public class MapHandler //Class to handle any map functions
{

    //Class Variables
    public MapTile[,] Map = new MapTile[MapHeight, MapWidth];
    public Dictionary<string, Texture2D> TextureMap = new Dictionary<string, Texture2D>();
    public string MapPath => Path.Combine(MapsFolder, (ActiveMapName + ".json"));
    
    public string ActiveMapName;
    private ContentManager Content;
    private MapFileManager FileManager;
    
    //Methods
    public MapHandler(ContentManager InputContent)  //Initializer
    {
        FileManager = new MapFileManager(this);
        Content = InputContent;
        LoadTextureMap();
    }

    public void MapExportHandler() //Handler for exporting maps
    {
        string[,] SavedMap = FileManager.SaveMap();
        FileManager.ExportMap(SavedMap);
    }

    public void MapImportHandler(string MapName) //Handler for importing maps
    {
        ActiveMapName = MapName;
        string[,] ImportedMap = FileManager.ImportMap();
        if (ImportedMap != null)
        {
            FileManager.LoadMap(ImportedMap);
        }
    }
    private void LoadTextureMap() //Method to load all textures from the textures folder
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

    public void LoopThroughTiles(Action<int, int> ActionToDo) //A method to loop through all the tiles in a map and perform actions on them
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

    public void DrawMap(SpriteBatch ActiveSpriteBatch) //Method to draw all the tiles for the map
    {
        LoopThroughTiles((PositionX, PositionY) =>
        {
            Map[PositionY, PositionX].Draw(ActiveSpriteBatch);
        });
    }
    public void SetupNewMap() //Method to create a new blank map
    {
        String[,] BlankMap = new string[MapHeight, MapWidth];
        for (int PositionY = 0; PositionY < MapHeight; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapWidth; PositionX++) //Loop through all the tiles
            {
                BlankMap[PositionY, PositionX] = TextureMap.Last().Key; //Set all the tiles to be grass
            }
        }
        FileManager.LoadMap(BlankMap);
    }
}

