namespace Stronghold_Crusader_Project.Code.Mapping;

/// <summary>
/// A class that holds data about the map and 
/// </summary>

public class GameWorld
{
    //Class Variables
    public Tile[,] Tiles = new Tile[(int)MapDimensions.X, (int)MapDimensions.Y];
    private Borders BorderHandler;
    private TileLibary TileManager;
    private MapFileManager FileManager;
    private bool IsLoaded = false;
    
    //Class Methods
    public GameWorld(TileLibary InputTileManager, Borders InputBorderHandler)
    {
        BorderHandler = InputBorderHandler;
        TileManager = InputTileManager;
        FileManager = new MapFileManager();
    }
    
    #region MapHandling
    //Functions that handle the map logic

    public void SetupNewMap() //A method to create a new blank map
    {
        LoopThroughTiles((PositionX, PositionY) =>
        {
            TileType RandomType = TileManager.GetRandomTileType();
            Tiles[PositionX, PositionY] = new Tile(new Point(PositionX, PositionY), RandomType);
        });
        IsLoaded = true;
    }

    public void Draw(SpriteBatch ActiveSpriteBatch) //A method to draw the map
    {
        LoopThroughTiles((PositionX, PositionY) =>
        {
            Tile ActiveTile = Tiles[PositionX, PositionY];
            if (ActiveTile != null) //If its not null
            {
                ActiveSpriteBatch.Draw(ActiveTile.Type.Texture, ActiveTile.WorldPosition - new Vector2(TileSize.X / 2, TileSize.Y / 2), Color.White);
            }
        });
        BorderHandler.Draw(ActiveSpriteBatch);
    }

    public void DrawMinimap(SpriteBatch ActiveSpriteBatch, Rectangle Bounds, Texture2D Pixel) //Draws the mini map or the map
    {
        if (Tiles[0, 0] != null)
        {
            int PixelWidth = Bounds.Width / MapDimensions.X;
            int PixelHeight = Bounds.Height / MapDimensions.Y;
            int OffsetX = (Bounds.Width - (PixelWidth * MapDimensions.X)) / 2;
            int OffsetY = (Bounds.Height - (PixelHeight * MapDimensions.Y)) / 2;
            LoopThroughTiles((PositionX, PositionY) =>
            {
                Color ActiveColor = Tiles[PositionX, PositionY].Type.MinimapColor;
                int PixelX = Bounds.X + OffsetX + (PixelWidth * PositionX);
                int PixelY = Bounds.Y + OffsetY + (PixelHeight * PositionY);

                ActiveSpriteBatch.Draw(Pixel, new Rectangle(PixelX, PixelY, PixelWidth, PixelHeight), ActiveColor);

            });
        }
    }

    public void SaveWorld(string MapName)
    {
        FileManager.SaveMap(this.Tiles, MapName);
    }

    public void LoadWorld(string MapName)
    {
        Tile[,] LoadedTiles = FileManager.LoadMap(this.TileManager, MapName);

        if (LoadedTiles != null)
        {
            this.Tiles = LoadedTiles;
            IsLoaded = true;
            LogEvent($"{MapName} has been loaded successfully", LogType.Info);
        }
    }
    
    #endregion
    
    #region Helper Methods
    //Functions to help the class
    
    private void LoopThroughTiles(Action<int, int> ActionToDo) //A method to loop through all the tiles in a map and perform actions on them
    {
        for (int PositionY = 0; PositionY < MapDimensions.Y; PositionY++)
        {
            for (int PositionX = 0; PositionX < MapDimensions.X; PositionX++) //Loop through all the tiles
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

