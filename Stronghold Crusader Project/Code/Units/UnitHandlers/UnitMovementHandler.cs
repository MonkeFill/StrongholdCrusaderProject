namespace Stronghold_Crusader_Project.Code.Units.UnitHandlers;

/// <summary>
/// A class that is used by the unit template to handle movement
/// </summary>

public class UnitMovementHandler
{
    //Class Variables
    private int CurrentPathIndex;
    private float Rotation = 0f;
    public Vector2 Position;
    private List<Point> CurrentPath;
    private Tile[,] Map = null;
    UnitTemplate Unit;
    
    //Class Methods
    public UnitMovementHandler(Vector2 InputPosition, UnitTemplate InputUnit)
    {
        Position = InputPosition;
        Unit = InputUnit;
    }
    
    #region Public Facing
    //Methods that will be used by unit template
    
    public UnitDirection GetDirection() //A class to get which direction the unit is pointing
    {
        float AdjustedRotation = Rotation + 90;
        if (AdjustedRotation >= 360)
        {
            AdjustedRotation -= 360;
        }
        int DirectionAmount = 8;
        float RotationPerAmount = 360f / DirectionAmount;
        int Index = (int)Math.Round(AdjustedRotation / RotationPerAmount);
        if (Index >= DirectionAmount)
        {
            Index = 0;
        }
        return (UnitDirection)Index;

    }
    
    public void MoveTo(List<Point> NewPath) //Gets the new path for the unit
    {
        CurrentPath = NewPath;
        CurrentPathIndex = 0;
    }

    public void Update(GameTime TimeOfGame, Tile[,] UpdateMap) //A class that will handle all the movement of the Unit
    {
        Map = UpdateMap;
        if (CurrentPath == null) //If it has no active path
        {
            UnitFinishedPathing();
            return;
        }

        if (CurrentPathIndex >= CurrentPath.Count) //If it has finished its path finding
        {
            UnitFinishedPathing();
            return;
        }

        Point TargetGridPosition = CurrentPath[CurrentPathIndex]; 
        if (!Map[TargetGridPosition.X, TargetGridPosition.Y].IsWalkable) //Checks if the tile its going to walk on is walkable
        {
            UnitFinishedPathing();
            return;
        }
        
        Vector2 TargetPosition = GridToWorld(TargetGridPosition); 
        Vector2 DirectionVector = TargetPosition - Position; //Checks the vector it needs to do to get to the target
        float DistanceToTarget = DirectionVector.Length(); //Getting how far from the target the unit is
        float MoveAmount = Unit.MovementSpeed * (float)TimeOfGame.ElapsedGameTime.TotalSeconds;

        if (DistanceToTarget <= MoveAmount)
        {
            Position = TargetPosition;
            CurrentPathIndex++;
        }
        else
        {
            DirectionVector.Normalize();
            Position += DirectionVector * MoveAmount;
            Rotation = MathF.Atan2(DirectionVector.Y, DirectionVector.X) * (180f / (float)Math.PI);
            if (Rotation < 0)
            {
                Rotation += 360;
            }
        }
    }
    
    #endregion
    
    #region Class Helpers
    //Methods that help the class

    private Vector2 GridToWorld(Point GridPosition)
    {
        int PositionX = (GridPosition.X * TileSize.X) + (TileSize.X / 2);
        int PositionY = (GridPosition.Y * TileSize.Y) + (TileSize.Y / 2);
        return new Vector2(PositionX, PositionY);
    }
    private void UnitFinishedPathing() //A class for when the unit finished its path finding
    {
        CurrentPath = null;
        CurrentPathIndex = 0;
        Unit.ActiveState = UnitState.Idle;
    }
    
    #endregion
}

