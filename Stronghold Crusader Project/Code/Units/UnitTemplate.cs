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
    private UnitAnimationHandler _unitAnimationManager;
    private UnitMovementHandler _unitMovementManager;
    public UnitState ActiveState;
    


    //Class Methods
    public UnitTemplate(string InputName, float InputMaxHealth, float InputMovementSpeed, float InputAttackPower, float InputAttackSpeed, UnitAnimationLibrary AnimationLibrary, Vector2 InputPosition)
    {
        UnitName = InputName;
        MaxHealth = InputMaxHealth;
        MovementSpeed = InputMovementSpeed;
        AttackPower = InputAttackPower;
        AttackSpeed = InputAttackSpeed;
        _unitAnimationManager = new UnitAnimationHandler(UnitName, AnimationLibrary);
        _unitMovementManager = new UnitMovementHandler(InputPosition, this);
    }
    
    #region Public methods
    //methods that are public

    public void Update(GameTime TimeOfGame, Tile[,] Map) //Update the unit
    {
        _unitMovementManager.Update(TimeOfGame, Map);
        _unitAnimationManager.Update(TimeOfGame, ActiveState, _unitMovementManager.GetDirection());
    }
    
    public void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the unit
    {
        _unitAnimationManager.Draw(ActiveSpriteBatch, _unitMovementManager.Position);
    }

    public Vector2 GetPosition()
    {
        return _unitMovementManager.Position;
    }

    public void MoveTo(List<Point> NewPath)
    {
        _unitMovementManager.MoveTo(NewPath);
    }
    
    #endregion
}
