namespace Stronghold_Crusader_Project.Code.Units;

/// <summary>
/// This class is a template class for all troops in the game
/// </summary>

public abstract class UnitTemplate
{
    //Class Variables

    //Variables that depend on the unit type
    protected string UnitName;
    protected float MaxHealth;
    public float MovementSpeed;
    protected float AttackPower;
    protected float AttackSpeed;
    
    //Variables that don't depend on the unit type
    private float CurrentHealth;
    private UnitAnimationHandler AnimationManager;
    private UnitMovementHandler MovementManager;
    public UnitState ActiveState;
    


    //Class Methods
    public UnitTemplate(string InputName, float InputMaxHealth, float InputMovementSpeed, float InputAttackPower, float InputAttackSpeed, UnitAnimationLibrary AnimationLibrary, Vector2 InputPosition)
    {
        UnitName = InputName;
        MaxHealth = InputMaxHealth;
        MovementSpeed = InputMovementSpeed;
        AttackPower = InputAttackPower;
        AttackSpeed = InputAttackSpeed;
        AnimationManager = new UnitAnimationHandler(UnitName, AnimationLibrary);
        MovementManager = new UnitMovementHandler(InputPosition, this);
    }
    
    #region Public methods
    //methods that are public

    public void Update(GameTime TimeOfGame, Tile[,] Map) //Update the unit
    {
        MovementManager.Update(TimeOfGame, Map);
        AnimationManager.Update(TimeOfGame, ActiveState, MovementManager.GetDirection());
    }
    
    public void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the unit
    {
        AnimationManager.Draw(ActiveSpriteBatch, MovementManager.Position);
    }

    public Vector2 GetPosition()
    {
        return MovementManager.Position;
    }

    public void MoveTo(List<Point> NewPath)
    {
        if (NewPath != null)
        {
            ActiveState = UnitState.Walking;
            MovementManager.MoveTo(NewPath);
        }
    }
    
    #endregion
}
