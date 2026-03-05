using Microsoft.Toolkit.HighPerformance.Helpers;

namespace Stronghold_Crusader_Project.Code.Units;


/// <summary>
/// A class that will manager all the units on screen
/// the class will handle hostile and non hostile units
/// </summary>

public class UnitManager
{
    //Class Variables
    private List<UnitTemplate> Units;
    private Pathing PathManager;
    private UnitAnimationLibrary AnimationLibrary;
    private UnitDebugging Debugger;
    private UnitFactory UnitCreator;
    private List<UnitTemplate> SelectedUnits;
    private List<string> AvailableUnitTypes = new List<string>{"Archer", "Maceman"};
    private int CurrentUnitSelection = 0;
    private UnitType TypeOfUnit;

    private Texture2D PathFindingTexture;
    private Texture2D RemoveUnitTexture;

    //Class Methods

    public UnitManager(ContentManager Content, GraphicsDevice Graphics, UnitType InputTypeOfUnit)
    {
        TypeOfUnit = InputTypeOfUnit;
        Units = new List<UnitTemplate>();
        PathManager = new Pathing();
        AnimationLibrary = new UnitAnimationLibrary(Content, TypeOfUnit);
        UnitCreator = new UnitFactory(AnimationLibrary);
        SelectedUnits = new List<UnitTemplate>();
        Debugger = new UnitDebugging(Graphics);

        switch (InputTypeOfUnit)
        {
            case UnitType.Ally:
                PathFindingTexture = Content.Load<Texture2D>(Path.Combine(SelectionFolder, "Pathing"));
                RemoveUnitTexture = Content.Load<Texture2D>(Path.Combine(SelectionFolder, "RemoveUnit"));
                break;
            case UnitType.Enemy:
                PathFindingTexture = Content.Load<Texture2D>(Path.Combine(SelectionFolder, "EnemyPathing"));
                RemoveUnitTexture = Content.Load<Texture2D>(Path.Combine(SelectionFolder, "RemoveEnemyUnit"));
                break;
        }
    }

    #region Public Methods
    //Classes that are publicly accessible

    public void Update(GameTime TimeOfGame, Tile[,] Map, InputManager InputHandler, Camera2D CameraHandler, UnitManager EnemyManager) //Updates all the units
    {
        if (Units.Count == 0)
        {
            return;
        }
        for (int Count = 0; Count < Units.Count; Count++) //Update all the units
        {
            UnitTemplate ActiveUnit = Units[Count];
            ActiveUnit.Update(TimeOfGame, Map);
            if (ActiveUnit.CheckUnitDeath())
            {
                Units.Remove(ActiveUnit);
            }
        }
        HandleCombat(EnemyManager);

    }

    public void Draw(SpriteBatch ActiveSpriteBatch) //A class that draws all the units
    {
        if (Units.Count == 0)
        {
            return;
        }
        foreach (UnitTemplate ActiveUnit in Units)
        {
            ActiveUnit.Draw(ActiveSpriteBatch);
        }
        Debugger.DrawDebug(ActiveSpriteBatch);
    }

    public void DrawUnitSelection(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler) //Draws the unit that is currently being selected to place
    {
        string UnitName = AvailableUnitTypes[CurrentUnitSelection];
        Vector2 UnitPosition = GridHelper.GridToWorld(GridHelper.WorldToGrid(InputHandler.GetMouseWorldPosition(CameraHandler))); //Getting the mouse position and converting it to grid and then back to position to lock it to the centre of a tile
        UnitTemplate NewUnit = GetUnitAvailable(UnitName, UnitPosition);
        Texture2D UnitTexture = NewUnit.GetIdleTexture();
        Vector2 ActualUnitPosition = new Vector2(UnitPosition.X - (UnitTexture.Width / 2f), UnitPosition.Y - (UnitTexture.Height / 2f) - (TileSize.Y / 2f));
        ActiveSpriteBatch.Draw(UnitTexture, ActualUnitPosition, Color.White * 0.75f);
    }

    public void DrawRemoveUnit(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler)
    {
        ActiveSpriteBatch.Draw(RemoveUnitTexture, GetSelectionMousePosition(InputHandler, CameraHandler), Color.White * 0.75f);
    }
    
    public void DrawPathing(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler)
    {
        ActiveSpriteBatch.Draw(PathFindingTexture, GetSelectionMousePosition(InputHandler, CameraHandler), Color.White * 0.75f);
    }
    

    public void PathingUnits(InputManager InputHandler, Camera2D CameraHandler, Tile[,] Map) //A class for using units path fidning
    {
        HandleUnitPathing(InputHandler, CameraHandler, Map);
    }

    public void PlacingUnit(InputManager InputHandler, Camera2D CameraHandler) //A class for placing a unit
    {
        HandleUnitPlacing(InputHandler, CameraHandler);
    }

    public void RemovingUnit(InputManager InputHandler, Camera2D CameraHandler) //A class for removing a unit
    {
        if (InputHandler.IsLeftClickedOnce())
        {
            Vector2 MousePosition = InputHandler.GetMouseWorldPosition(CameraHandler);
            UnitTemplate Unit = GetUnitAtPosition(MousePosition);
            if (Unit != null)
            {
                Units.Remove(Unit);
            }
        }
    }
    
    #endregion
    
    #region Helper Classes
    //Methods that help the class

    private void HandleCombat(UnitManager EnemyManager)
    {
        for (int Count = 0; Count < Units.Count; Count++)
        {
            UnitTemplate PlayerUnits = Units[Count];
            if (PlayerUnits.ActiveState != UnitState.Idle && PlayerUnits.ActiveState != UnitState.Attacking)
            {
                continue;
            }
            bool Attacking = false;
            for (int Count2 = 0; Count2 < EnemyManager.Units.Count; Count2++)
            {
                UnitTemplate EnemyUnit = EnemyManager.Units[Count2];
                if (Vector2.Distance(PlayerUnits.GetPosition(), EnemyUnit.GetPosition()) < UnitSelectionRadius)
                {
                    if (!Attacking)
                    {
                        if (PlayerUnits.ActiveState == UnitState.Idle)
                        {
                            PlayerUnits.ActiveState = UnitState.Attacking;
                            Attacking = true;
                        }
                    }
                    if (PlayerUnits.ActiveState == UnitState.Attacking)
                    {
                        Vector2 DirectionVector = EnemyUnit.GetPosition() - PlayerUnits.GetPosition();
                        DirectionVector.Normalize();
                        PlayerUnits.MovementManager.Rotation = MathF.Atan2(DirectionVector.Y, DirectionVector.X) * (180f / (float)Math.PI);
                        if (PlayerUnits.MovementManager.Rotation  < 0)
                        {
                            PlayerUnits.MovementManager.Rotation  += 360;
                        }
                        float AttackPower = 1f;
                        EnemyUnit.CurrentHealth -= AttackPower;
                        Attacking = true;
                    }
                }
                
            }
            if (!Attacking)
            {
                PlayerUnits.ActiveState = UnitState.Idle;
            }
        }
    }
    
    private Vector2 GetSelectionMousePosition(InputManager InputHandler, Camera2D CameraHandler) //Getting how far down to draw the selection stuff
    {
        Vector2 MousePosition = InputHandler.GetMouseWorldPosition(CameraHandler);
        Vector2 BelowMousePosition = new Vector2(MousePosition.X, MousePosition.Y - (TileSize.Y / 2f));
        return BelowMousePosition;
    }
    private void CycleUnit(int Direction) //A class that will go through the units available to place
    {
        CurrentUnitSelection += Direction;
        if (CurrentUnitSelection < 0) //If it has gone minus to loop it
        {
            CurrentUnitSelection = AvailableUnitTypes.Count - 1;
        }
        else if (CurrentUnitSelection >= AvailableUnitTypes.Count) //if it has gone higher then available amount, loop it
        {
            CurrentUnitSelection = 0;
        }
        
        LogEvent($"Selected unit Type: {AvailableUnitTypes[CurrentUnitSelection]}", LogType.Info);
    }

    private void HandleUnitPlacing(InputManager InputHandler, Camera2D CameraHandler)
    {
        if (InputHandler.IsKeybindPressedOnce(KeyAction.NextSelection))
        {
            CycleUnit(1);
        }
        if (InputHandler.IsKeybindPressedOnce(KeyAction.PreviousSelection))
        {
            CycleUnit(-1);
        }
        if (InputHandler.IsLeftClickedOnce())
        {
            string UnitName = AvailableUnitTypes[CurrentUnitSelection];
            Vector2 UnitPosition = GridHelper.GridToWorld(GridHelper.WorldToGrid(InputHandler.GetMouseWorldPosition(CameraHandler))); //Getting the mouse position and converting it to grid and then back to position to lock it to the centre of a tile
            UnitTemplate NewUnit = GetUnitAvailable(UnitName, UnitPosition);
            if (NewUnit == null)
            {
                LogEvent($"{UnitName} not found to place", LogType.Error);
                return;
            }
            Units.Add(NewUnit);
        }
        
    }

    private UnitTemplate GetUnitAtPosition(Vector2 Position) //Returns a unit at a certain position
    {
        foreach (UnitTemplate ActiveUnit in Units)
        {
            if (Vector2.Distance(ActiveUnit.GetPosition(), Position) < UnitSelectionRadius) //If the click within a certain radius
            {
                return ActiveUnit;
            }
        }
        return null;
    }

    private void HandleUnitPathing(InputManager InputHandler, Camera2D CameraHandler, Tile[,] Map) //Handles unit pathing input
    {
        Vector2 MousePosition = InputHandler.GetMouseWorldPosition(CameraHandler);

        if (InputHandler.IsLeftClickedOnce()) //Adding units to the selected units list
        {
            SelectedUnits.Clear();
            UnitTemplate UnitFound = GetUnitAtPosition(MousePosition);
            if (UnitFound != null)
            {
                SelectedUnits = new List<UnitTemplate>{ UnitFound };
            }
        }

        if (InputHandler.IsRightClickedOnce()) //Moving the unit to where the mouse is clicked
        {
            Point EndPoint = GridHelper.WorldToGrid(MousePosition);
            foreach (HostileUnit ActiveUnit in SelectedUnits)
            {
                Point StartPoint = GridHelper.WorldToGrid(ActiveUnit.GetPosition());
                List<Point> ActivePath = PathManager.FindPath(StartPoint, EndPoint, Map);
                ActiveUnit.MoveTo(ActivePath);
                Debugger.UpdateDebug(StartPoint, ActivePath, PathManager.GetNeighbours(EndPoint), MousePosition);
            }
        }
    }

    private UnitTemplate GetUnitAvailable(string UnitName, Vector2 Position) //A class that will return a specific unit
    {
        switch (UnitName)
        {
            case "Archer":
                return UnitCreator.GetArcher(Position);
            case "Maceman":
                return UnitCreator.GetMaceman(Position);
        }
        return null;
    }
    
    #endregion
}

