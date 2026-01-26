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
        int DirectionAmount = Enum.GetNames(typeof(UnitDirection)).Length; //Getting how many directions are possible
        float RotationPer = 360f / DirectionAmount;
        int Index = (int)((Rotation / RotationPer) % DirectionAmount);
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

        if (DistanceToTarget > 0) //Making sure the distance is bigger then 0
        {
            DirectionVector.Normalize(); //Making sure it always move the same speed
            Position += DirectionVector * Unit.MovementSpeed * (float)TimeOfGame.ElapsedGameTime.TotalSeconds; //Moving the unit
            Rotation = MathF.Atan2(DirectionVector.Y, DirectionVector.X) * (180f / (float)Math.PI); //Facing the unit the right direction

            if (Rotation < 0) //Making sure the angle isn't negative
            {
                Rotation += 360f;
            }
        }

        if (DistanceToTarget < 2.0f) //Checking if we are not close enough to the target
        {
            CurrentPathIndex++;
        }
    }
    
    #endregion
    
    #region Class Helpers
    //Methods that help the class

    private Vector2 GridToWorld(Point GridPosition) //Class that will convert grid points to world vector2s
    {
        float OffSetX = 0;
        if (GridPosition.Y % 2 != 0) //If the tile is an odd row shift to the right
        {
            OffSetX = TileWidth / 2f;
        }

        float PositionX = (GridPosition.X * TileWidth) + OffSetX;
        float PositionY = (GridPosition.Y * (TileHeight / 2f));
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

