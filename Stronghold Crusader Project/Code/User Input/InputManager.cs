namespace Stronghold_Crusader_Project.Code.User_Input;

public static class InputManager
{
    //Class Variables
    private static int PreviousMouseScrollValue = 0;
    private static Vector2 PositionChange;
    private static float RotationChange;
    private static float ZoomChange;
    private static CameraAction NewAction;
    private static KeyboardState PreviousKeyBoardState;
    private static MouseState PreviousMouseState;
    private static Rectangle SelectionBox = new Rectangle(0, 0, 0, 0);
    private static bool SelectingTroops = false;
    private static bool Positioning = false;
    private static BuildingTemplate ActiveBuilding;
    private static List<TroopTemplate> CurrentlySelectedTroops = new List<TroopTemplate>();

    
    //Methods
    public static void UpdateInputManager(GameTime InputGameTime)
    {
        PositionChange = Vector2.Zero;
        RotationChange = 0f;
        ZoomChange = 0f;
        NewAction = CameraAction.None;
        HandleKeyboardInput();
        /*
        MouseState ActiveMouseState = Mouse.GetState();
        int ActiveMouseScrollValue = ActiveMouseState.ScrollWheelValue;
        
        if (ActiveMouseScrollValue != PreviousMouseScrollValue) //Zooming
        {
            ActiveMouseScrollValue /= 120;
            int MouseScrollChange = PreviousMouseScrollValue - ActiveMouseScrollValue;
            ZoomChange = MouseScrollChange * ZoomSensitivity * -1;
            NewAction = CameraAction.Zoom;
            PreviousMouseScrollValue = ActiveMouseScrollValue;
        }*/
        
        UpdateCamera(InputGameTime, NewAction, PositionChange, RotationChange, ZoomChange);
    }

    private static void HandleMouseInput() //Handle any mouse inputs
    {
        MouseState ActiveMouseState = Mouse.GetState();
        //Code
        PreviousMouseState = ActiveMouseState;
    }

    private static void HandleKeyboardInput() //Handle any keyboard inputs
    {
        KeyboardState ActiveKeyboardState = Keyboard.GetState();
        HandleMapMovement("MoveUp", ActiveKeyboardState);
        HandleMapMovement("MoveDown", ActiveKeyboardState);
        HandleMapMovement("MoveLeft", ActiveKeyboardState);
        HandleMapMovement("MoveRight", ActiveKeyboardState);
        PreviousKeyBoardState = ActiveKeyboardState;
    }
    
    private static void HandleMapMovement(string Control, KeyboardState ActiveKeyboardState)
    {
        if (ControlCurrentlyPressed(Control, ActiveKeyboardState))
        {
            Vector2 TempPositionChange = (Vector2)GetValueChangeFromControl(Control);
            PositionChange += new Vector2(TempPositionChange.X * MovementAmount, TempPositionChange.Y * MovementAmount);
            NewAction = CameraAction.Move;
        }
    }

    private static bool ControlCurrentlyPressed(string Control, KeyboardState ActiveKeyboardState)
    {
        if (ActiveKeyboardState.IsKeyDown(GetKeyFromControl(Control)))
        {
            return true;
        }
        return false;
    }
}
