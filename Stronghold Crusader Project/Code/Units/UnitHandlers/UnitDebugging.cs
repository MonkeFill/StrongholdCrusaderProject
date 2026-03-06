namespace Stronghold_Crusader_Project.Code.Units.UnitHandlers;

/// <summary>
/// This class will handle any debugging needed for units
/// </summary>

public class UnitDebugging
{
    //Class Variables
    private Texture2D Pixel;
    private  Point DebugMouseGrid;
    private Point OldDebugMouseGrid = Point.Zero;
    private Point UnitPosition;
    private List<Point> DebugPath;
    private List<Point> DebugNeighbours;
    private int RectangleSize = TileSize.Y / 2;
    private int RectangleOffset = TileSize.Y / 4;
    
    //Class Methods
    public UnitDebugging(GraphicsDevice Graphics)
    {
        Pixel = new Texture2D(Graphics, 1, 1);
        Pixel.SetData(new[] {Color.White});
    }
    
    #region Public Facing Methods
    //Methods that can be accessed pubically

    public void UpdateDebug(Point StartPoint, List<Point> ActivePath, List<Point> Neighbours, Vector2 MousePosition) //A class to update the debugging
    {
        if (DebugUnitsPathing)
        {
            UnitPosition = StartPoint;
            DebugPath = ActivePath;
            DebugMouseGrid = GridHelper.WorldToGrid(MousePosition);
            try
            {
                if (OldDebugMouseGrid != DebugMouseGrid)
                {
                    OldDebugMouseGrid = DebugMouseGrid;
                    DebugNeighbours = Neighbours;
                }
            }
            catch
            {
            }
        }
    }

    public void DrawDebug(SpriteBatch ActiveSpriteBatch) //A class to draw the debugging
    {
        if (!DebugUnitsPathing)
        {
            return;
        }
        Vector2 MouseWorld = GridHelper.GridToWorld(DebugMouseGrid);
        Vector2 UnitWorld = GridHelper.GridToWorld(UnitPosition);
        if (DebugPath != null)
        {
            foreach (Point ActivePoint in DebugPath)
            {
                Vector2 Position = GridHelper.GridToWorld(ActivePoint);
                ActiveSpriteBatch.Draw(Pixel, new Rectangle((int)Position.X - RectangleOffset, (int)Position.Y - RectangleOffset, RectangleSize, RectangleSize), Color.Green);
            }
        }

        if (DebugNeighbours != null)
        {
            foreach (Point ActivePoint in DebugNeighbours)
            {
                Vector2 Position = GridHelper.GridToWorld(ActivePoint);
                ActiveSpriteBatch.Draw(Pixel, new Rectangle((int)Position.X - RectangleOffset, (int)Position.Y - RectangleOffset, RectangleSize, RectangleSize), Color.Yellow);
            }
        }
        
        ActiveSpriteBatch.Draw(Pixel, new Rectangle((int)MouseWorld.X - RectangleOffset, (int)MouseWorld.Y - RectangleOffset, RectangleSize, RectangleSize), Color.Blue);
        ActiveSpriteBatch.Draw(Pixel, new Rectangle((int)UnitWorld.X - RectangleOffset, (int)UnitWorld.Y - RectangleOffset, RectangleSize, RectangleSize), Color.Orange);
    }
    
    #endregion
    
    #region Helper Methods
    //Methods to help the class
    
    #endregion
}

