namespace Stronghold_Crusader_Project.Code.Mapping;

public static class MapHandler //Class to handle any map functions
{

    //Class Variables
    public static MapTile[,] Map = new MapTile[MapHeight, MapWidth];
    public static Dictionary<string, Texture2D> TextureMap = new Dictionary<string, Texture2D>();
    public static Dictionary<string, Color> BasicTextureMap = new Dictionary<string, Color>();
    public static string MapPath => Path.Combine(MapsFolder, ActiveMapName);
    private static Borders BorderHandler;
    public static string ActiveMapName;
    private static ContentManager Content;
    private static MapFileManager FileManager;
    private static bool MapLoaded = false;

    //Methods
    public static void MapHandlerInitializer(ContentManager InputContent)  //Initializer
    {
        FileManager = new MapFileManager();
        Content = InputContent;
        LoadTextureMap();
        BorderHandler = new Borders(InputContent);
    }

    public static void MapExportHandler() //Handler for exporting maps
    {
        string[,] SavedMap = FileManager.SaveMap();
        FileManager.ExportMap(SavedMap);
    }

    public static void MapImportHandler(string MapName) //Handler for importing maps
    {
        ActiveMapName = MapName;
        string[,] ImportedMap = FileManager.ImportMap();
        if (ImportedMap != null) //If the imported map has been loaded correctly
        {
            MapLoaded = true;
            FileManager.LoadMap(ImportedMap);
        }
        else
        {
            Map = new MapTile[MapHeight, MapWidth];
            LogEvent($"{MapName} is not valid", LogType.Warning);
        }
    }

    public static void LoopThroughTiles(Action<int, int> ActionToDo) //A method to loop through all the tiles in a map and perform actions on them
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

    public static void DrawMap(SpriteBatch ActiveSpriteBatch) //Method to draw all the tiles for the map
    {
        if (MapLoaded) //Making sure a valid map has been loaded to be drawn
        {
            LoopThroughTiles((PositionX, PositionY) =>
            {
                Map[PositionY, PositionX].Draw(ActiveSpriteBatch);
            });
            BorderHandler.Draw(ActiveSpriteBatch);
        }
        else
        {
            LogEvent("Tried drawing map with no map loaded", LogType.Error);
        }
    }
    public static void SetupNewMap() //Method to create a new blank map
    {
        Random RanInt = new Random();
        String[,] BlankMap = new string[MapHeight, MapWidth];
        for (int PositionY = 0; PositionY < MapHeight; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapWidth; PositionX++) //Loop through all the tiles
            {
                BlankMap[PositionY, PositionX] = TextureMap.ElementAt(RanInt.Next(TextureMap.Count)).Key; //Set all the tiles to be grass
            }
        }
        FileManager.LoadMap(BlankMap);
    }

    private static void LoadTextureMap() //Method to load all textures from the textures folder
    {
        LogEvent("Loading texture map started", LogType.Info);
        string[] TileFolders = Directory.GetDirectories(TilesFolderFullPath); //Getting any of the tile folders
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
                LogEvent($"Accessing {ActiveTileVariant} tile", LogType.Info);
                if (TextureMap.ContainsKey(FileName)) //if the dictionary already has it
                {
                    continue;
                }
                string ActiveTileFromContent = Path.Combine(TilesFolderPathFromContent, FolderName, FileName); //Rebuilding the file path from content to load it in
                TextureMap.Add(FileName, Content.Load<Texture2D>(ActiveTileFromContent)); //Adding the texture and key

                //Now adding the basic map texture
                if (!BasicTextureMap.ContainsKey(FolderName)) //If the BasicTextureMap doesn't already contain the basic texture for that specific tile set
                {
                    Texture2D ActiveTexture = TextureMap[FileName];
                    Rectangle TexturePixel = new Rectangle(ActiveTexture.Width / 2, ActiveTexture.Height / 2, 1, 1);
                    Color[] TextureColour = new Color[1];
                    ActiveTexture.GetData(0, TexturePixel, TextureColour, 0, 1);
                    BasicTextureMap.Add(FolderName, TextureColour[0]);
                }
            }
            if (TempCount == 0) //Check if there are any .xnb files
            {
                LogEvent($"No tiles found for {ActiveTileFolder}", LogType.Error);
                continue;
            }
        }
        LogEvent("Loaded texture map finished", LogType.Info);
    }
}

