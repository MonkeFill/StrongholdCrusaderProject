namespace Stronghold_Crusader_Project.Code.User_Input;

public static class InputManager
{
    //Class Variables
    private static int PreviousMouseScrollValue = 0;
    private static Vector2 PreviousMousePosition;
    private static KeyboardState PreviousKeyboardState;
    private static bool SelectingTroops = false;
    private static bool Positioning = false;
    private static BuildingTemplate ActiveBuilding;
    private static TroopTemplate ActiveTroop;
    
    //Methods
    public static void UpdateInputManager(GameTime InputGameTime)
    {
        Vector2 PositionChange = Vector2.Zero;
        float RotationChange = 0f;
        float ZoomChange = 0f;
        CameraAction NewAction = CameraAction.None;
        MouseState ActiveMouseState = Mouse.GetState();
        int ActiveMouseScrollValue = ActiveMouseState.ScrollWheelValue;
        KeyboardState ActiveKeyboardState = Keyboard.GetState();
        
        if (ActiveMouseScrollValue != PreviousMouseScrollValue) //Zooming
        {
            ActiveMouseScrollValue /= 120;
            int MouseScrollChange = PreviousMouseScrollValue - ActiveMouseScrollValue;
            ZoomChange = MouseScrollChange * ZoomSensitivity * -1;
            NewAction = CameraAction.Zoom;
            PreviousMouseScrollValue = ActiveMouseScrollValue;
        }
        if (ActiveKeyboardState.IsKeyDown(GetKeyFromControl("MoveUp"))) //Move forward
        {
            PositionChange.Y -= MovementAmount;
            NewAction = CameraAction.Move;
        }
        if (ActiveKeyboardState.IsKeyDown(GetKeyFromControl("MoveDown"))) //Move down
        {
            PositionChange.Y += MovementAmount;
            NewAction = CameraAction.Move;
        }
        if (ActiveKeyboardState.IsKeyDown(GetKeyFromControl("MoveLeft"))) //Move left
        {
            PositionChange.X -= MovementAmount;
            NewAction = CameraAction.Move;
        }
        if (ActiveKeyboardState.IsKeyDown(GetKeyFromControl("MoveRight"))) //Move right
        {
            PositionChange.X += MovementAmount;
            NewAction = CameraAction.Move;
        }
        
        UpdateCamera(InputGameTime, NewAction, PositionChange, RotationChange, ZoomChange);
    }
    
    
}
