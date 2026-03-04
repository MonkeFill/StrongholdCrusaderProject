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
    public float AttackPower;
    protected float AttackSpeed;
    
    //Variables that don't depend on the unit type
    public float CurrentHealth;
    private UnitAnimationHandler AnimationManager;
    public UnitMovementHandler MovementManager;
    public UnitState ActiveState;
    private float CurrentAttackCooldown = 0f;
    


    //Class Methods
    public UnitTemplate(string InputName, float InputMaxHealth, float InputMovementSpeed, float InputAttackPower, float InputAttackSpeed, UnitAnimationLibrary AnimationLibrary, Vector2 InputPosition)
    {
        ActiveState = UnitState.Attacking;
        UnitName = InputName;
        MaxHealth = InputMaxHealth;
        CurrentHealth = MaxHealth;
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
        
        if (CurrentAttackCooldown > 0)
        {
            CurrentAttackCooldown -= (float)TimeOfGame.ElapsedGameTime.TotalSeconds;
        }

        if (CurrentHealth <= 0)
        {
            ActiveState = UnitState.Dead;
        }
    }

    public bool CheckUnitDeath()
    {
        if (CurrentHealth < 0)
        {
            return true;
        }
        return false;
    }
    
    public void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the unit
    {
        AnimationManager.Draw(ActiveSpriteBatch, MovementManager.Position);
    }

    public Texture2D GetIdleTexture()
    {
        return AnimationManager.FramesList[0];
    }

    public Vector2 GetPosition()
    {
        return MovementManager.Position;
    }

    public void MoveTo(List<Point> NewPath)
    {
        if (ActiveState == UnitState.Dead)
        {
            return;
        }
        if (NewPath != null)
        {
            ActiveState = UnitState.Walking;
            MovementManager.MoveTo(NewPath);
        }
    }
    
    #endregion
}
