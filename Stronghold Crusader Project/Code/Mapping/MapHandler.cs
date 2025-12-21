namespace Stronghold_Crusader_Project.Code.Mapping;

public static class MapHandler //Class to handle any map functions
{

    //Class Variables
    public static MapTile[,] Map = new MapTile[MapHeight, MapWidth];
    public static List<MapTextureTile> TextureMap = new List<MapTextureTile>();
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

    public static List<MapTextureTile> GetMapVariantType(string VariantType) //Returns the different variant types
    {
        List<MapTextureTile> Variants = new List<MapTextureTile>();
        foreach (MapTextureTile ActiveTextureTile in TextureMap)
        {
            if (ActiveTextureTile.VariantName == VariantType)
            {
                Variants.Add(ActiveTextureTile);
            }
        }
        return Variants;
    }
    
    public static MapTextureTile GetMapVariant(string Variant) //returns a specific variant
    {
        foreach (MapTextureTile ActiveTextureTile in TextureMap)
        {
            if (ActiveTextureTile.VariantKey == Variant)
            {
                return (ActiveTextureTile);
            }
        }
        return null;
    }

    public static bool TextureMapContains(string Variant) //Checks if a variant already exists
    {
        foreach (MapTextureTile ActiveTextureTile in TextureMap)
        {
            if (ActiveTextureTile.VariantKey == Variant)
            {
                return true;
            }
        }
        return false;
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
    
    public static void DrawMiniMap(SpriteBatch ActiveSpriteBatch, Rectangle MiniMap, Texture2D Pixel) //Draws a small version of the map in a compact and basic form
    {
        if (Map[0,0] != null)
        {
            int TileWidth = MiniMap.Width / MapWidth;
            int TileHeight = MiniMap.Height / MapHeight;
            int OffSetX = (MiniMap.Width - (TileWidth * MapWidth)) / 2;
            int OffSetY = (MiniMap.Height - (TileHeight * MapHeight)) / 2;
            Vector2 StartPosition = new Vector2(MiniMap.X + OffSetX, MiniMap.Y + OffSetY);
            for (int PositionY = 0; PositionY < MapHeight; PositionY++)
            {
                for (int PositionX = 0; PositionX < MapWidth; PositionX++) //Loop through all the tiles
                {
                    string Temp = Map[PositionY, PositionX].TileKey;
                    string ActiveTile = Map[PositionY, PositionX].TileKey;
                    Color ActiveColour = GetMapVariant(ActiveTile).BasicColour;
                    Rectangle Position = new Rectangle((int)((TileWidth * PositionX) + StartPosition.X), (int)((TileHeight * PositionY) + StartPosition.Y), TileWidth, TileHeight);
                    ActiveSpriteBatch.Draw(Pixel, Position, ActiveColour);
                }
            }
        }
    }

    public static Point GetArrayPositionMouse() //Returns the position in the array of the tile the mouse is over
    {
        Vector2 MousePosition = GetCameraMousePosition();
        int TileX = (int)Math.Floor(MousePosition.X / TileWidth);
        int TileY = (int)Math.Floor(MousePosition.Y / TileHeight);
        if (TileX < 0 || TileX >= MapWidth || TileY < 0 || TileY >= MapHeight) //if it is out of bounds
        {
            return new Point(TileX, TileY);
        }
        return new Point(-1, -1);
    }

    public static Vector2 GetTileMousePosition() //returns the position of the tile the mouse is over
    {
        Point ArrayPosition = GetArrayPositionMouse();
        return new Vector2(ArrayPosition.X * TileWidth, ArrayPosition.Y * TileHeight);
    }
    
    private static void SetupNewMap() //Method to create a new blank map
    {
        Random RanInt = new Random();
        String[,] BlankMap = new string[MapHeight, MapWidth];
        for (int PositionY = 0; PositionY < MapHeight; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapWidth; PositionX++) //Loop through all the tiles
            {
                BlankMap[PositionY, PositionX] = TextureMap.ElementAt(RanInt.Next(TextureMap.Count)).VariantKey; //Set all the tiles to be grass
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
                if (TextureMapContains(FileName)) //if the dictionary already has it
                {
                    continue;
                }
                string ActiveTileFromContent = Path.Combine(TilesFolderPathFromContent, FolderName, FileName); //Rebuilding the file path from content to load it in
                TextureMap.Add(new MapTextureTile(FolderName, FileName, Content.Load<Texture2D>(ActiveTileFromContent))); //Adding the texture
                
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

