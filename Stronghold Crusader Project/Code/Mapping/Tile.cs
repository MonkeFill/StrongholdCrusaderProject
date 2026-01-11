using Stronghold_Crusader_Project.Code.Buildings;

namespace Stronghold_Crusader_Project.Code.Mapping;

/// <summary>
/// The tile class is the class that controls all information about each individual tile that makes up the map
/// </summary>

public class Tile //Class for each individual tile on the map
{
    //Class Variables
    public Point GridPosition;
    public Vector2 WorldPosition;
    public TileType Type;
    public BuildingTemplate Building;
    public bool IsWalkable => CheckWalkable();

    //Class Methods
    public Tile(Point InputGridPosition, TileType InputType)
    {
        GridPosition = InputGridPosition;
        Type = InputType;
        WorldPosition = GridToWorld();
        Building = null;
    }
    
    #region Helper Methods
    //functions that help the class

    private bool CheckWalkable() //Checking if the tile should be able to be walked on
    {
        if (!Type.IsWalkable) //if the tile type should be walked on like a rock or water
        {
            return false;
        }
        if (Building != null) //if a building is on this tile it should not be able to be walked on
        {
            return false;
        }
        return true;
    }

    private Vector2 GridToWorld() //Method that converts grid positions to world positions which is a staggered position
    {
        float OffSetX = 0;
        if (GridPosition.Y % 2 != 0) //If it is the second row then it will be shifted right by half a tile
        {
            OffSetX = TileWidth / 2f;
        }
        float NewPositionX = (GridPosition.X * TileWidth) + OffSetX;
        float NewPositionY = (GridPosition.Y * (TileHeight / 2f));
        return new Vector2(NewPositionX, NewPositionY);
    }
    
    #endregion
}

