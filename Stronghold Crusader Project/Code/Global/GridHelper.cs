namespace Stronghold_Crusader_Project.Code.Global;

/// <summary>
/// A class that holds all the grid based methods
/// it is global so that every class can access it
/// </summary>

public static class GridHelper
{
    public static Point WorldToGrid(Vector2 WorldPosition) //Convert World to grid positions
    {
        int PositionX = (int)WorldPosition.X / TileSize.X;
        int PositionY = (int)WorldPosition.Y / TileSize.Y;
        return new Point(PositionX, PositionY);
    }
    
    public static Vector2 GridToWorld(Point GridPosition) //Converts grid to world position
    {
        int PositionX = (GridPosition.X * TileSize.X) + (TileSize.X / 2);
        int PositionY = (GridPosition.Y * TileSize.Y) + (TileSize.Y / 2);
        return new Vector2(PositionX, PositionY);
    }

    public static bool WithinMapBounds(Vector2 MousePosition) //Checks if the mouse position is within the bounds of the map
    {
        if (MousePosition.X < 0 || MousePosition.X > MapSize.X)
        {
            return false;
        }
        
        if (MousePosition.Y < 0 || MousePosition.Y > MapSize.Y)
        {
            return false;
        }

        return true;
    }
}

