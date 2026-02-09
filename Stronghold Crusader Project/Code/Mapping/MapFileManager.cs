namespace Stronghold_Crusader_Project.Code.Mapping;

/// <summary>
/// 
/// </summary>

public class MapFileManager
{
    //Class Methods
    public MapFileManager()
    {
        
    }
    
    #region File Handling
    //Classes that are handling saving and loading files directly

    public void SaveMap(Tile[,] Tiles, string MapName) //A method to save the map to a file
    {
        String[,] TileNames = new string[MapDimensions.X, MapDimensions.Y];
        LoopThroughTiles((PositionX, PositionY) =>
        {
            TileNames[PositionX, PositionY] = Tiles[PositionX, PositionY].Type.Name; //Retrieving all the tile names
        });
        
        string Json = JsonConvert.SerializeObject(TileNames, Formatting.Indented);
        File.WriteAllText(Path.Combine(MapsFolder, MapName + MapFileExtension), Json);
    }

    public Tile[,] LoadMap(TileLibary TileManager, string MapName) //A method to load the map from a file
    {
        string FullMapPath = Path.Combine(SavesFolder, MapName);
        if (!File.Exists(FullMapPath)) //Checking if the map exists
        {
            LogEvent($"{FullMapPath} map not found", LogType.Warning);
            return null;
        }
        try
        {
            string Json = File.ReadAllText(FullMapPath);
            string[,] TileNames = JsonConvert.DeserializeObject<string[,]>(Json);

            if (TileNames == null) //if the file is empty
            {
                LogEvent($"{FullMapPath} map is empty", LogType.Warning);
                return null;
            }

            if (TileNames.GetLength(0) != MapDimensions.X || TileNames.GetLength(1) != MapDimensions.Y) //If the map is not the same size as it should be
            {
                LogEvent($"{FullMapPath} is the wrong legth, size is {TileNames.GetLength(0)}x{TileNames.GetLength(1)}", LogType.Warning);
                return null;
            }
            
            //Now that we have loaded the map in through strings we have to convert them to the tile class
            Tile[,] LoadedMap = new Tile[MapDimensions.X, MapDimensions.Y];
            TileType FallbackTile = TileManager.GetRandomTileType();
            LoopThroughTiles((PositionX, PositionY) =>
            {
                string ActiveTile = TileNames[PositionX, PositionY];
                TileType ActiveTileType = TileManager.GetTileType(ActiveTile);
                if (ActiveTileType == null) //If the tile type isn't found
                {
                    LogEvent($"{ActiveTile} Not found in tiles libary", LogType.Warning);
                    ActiveTileType = FallbackTile;
                }
                
                LoadedMap[PositionX, PositionY] = new Tile(new Point(PositionX, PositionY), ActiveTileType);
            });

            return LoadedMap;
        }
        catch (Exception Error)
        {
            LogEvent($"{MapName} has had an error, {Error.Message}", LogType.Warning);
        }
        return null;
    }
    
    #endregion
    
    #region Helper Methods
    //Functions to help the class
    
    private static void LoopThroughTiles(Action<int, int> ActionToDo) //A method to loop through all the tiles in a map and perform actions on them
    {
        for (int PositionY = 0; PositionY < MapDimensions.X; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapDimensions.Y; PositionX++) //Loop through all the tiles
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
    
    #endregion
}

