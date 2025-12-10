namespace Stronghold_Crusader_Project.Code.User_Input;

public static class InputManager
{
    //Variables 
    private static MenuManager GameMenuManager;
    private static KeyboardState ActiveKeyboardState;
    private static KeyboardState PreviousKeyboardState;
    private static MouseState ActiveMouseState;
    private static MouseState PreviousMouseState;
    private static KeyManager GlobalKeybindManager;

    public enum MouseButtonType
    {
        Left,
        Right,
    }

    public enum KeybindActiveType
    {
        PressedOnce,
        PressedMultiple
    }
    
    //Methods
    public static void InputManagerInitialiser(MenuManager Input_GameMenuManager)
    {
        GameMenuManager = Input_GameMenuManager;
        GlobalKeybindManager = new KeyManager("GlobalKeybindManager");
        GlobalKeybindManager.AddNewKeybind("PreviousMenu", Keys.Escape, () => GameMenuManager.RemoveMenu(), KeybindActiveType.PressedOnce);
    }
    public static void InputManagerUpdate()
    {
        PreviousKeyboardState = ActiveKeyboardState;
        PreviousMouseState = ActiveMouseState;
        ActiveKeyboardState = Keyboard.GetState();
        ActiveMouseState = Mouse.GetState();
    }

    public static void CheckIfKeybindsPressed(Dictionary<string, KeyMap> LocalKeybinds) //Checks if any of the keybinds have been pressed
    {
        foreach (KeyMap ActiveKey in LocalKeybinds.Values)
        {
            CheckKeybind(ActiveKey);
        }
        foreach (KeyMap ActiveKey in GlobalKeybindManager.Keybinds.Values)
        {
            CheckKeybind(ActiveKey);
        }
    }

    public static bool MouseWithinRectangle(Rectangle Bounds) //if a mouse position is within a rectangle
    {
        if (Bounds.Contains(ActiveMouseState.Position))
        {
            return true;
        }
        return false;
    }
    
    public static bool SingleMouseClick(MouseButtonType ButtonToBeChecked)//Check to make sure the mouse clicked only in the current frame
    {
        switch (ButtonToBeChecked)
        {
            case MouseButtonType.Left:
                if (ActiveMouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton != ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case MouseButtonType.Right:
                if (ActiveMouseState.RightButton == ButtonState.Pressed && PreviousMouseState.RightButton != ButtonState.Pressed)
                {
                    return true;
                }
                break; 
        }
        return false;
    }

    private static bool KeyHeldDown(Keys KeyToBeChecked) //If a key is pressed on multiple frames
    {
        if (ActiveKeyboardState.IsKeyDown(KeyToBeChecked) && PreviousKeyboardState.IsKeyDown(KeyToBeChecked)) //If both have the key pressed
        {
            return true;
        }
        return false;
    }

    private static bool KeyPressedOnce(Keys KeyToBeChecked)
    {
        if (ActiveKeyboardState.IsKeyDown(KeyToBeChecked) && !PreviousKeyboardState.IsKeyDown(KeyToBeChecked)) //If it is pressed on this frame but not on the old frame
        {
            return true;
        }
        return false;
    }
    
    private static Point GetScaledMousePosition() //Convert the original mouse position to whatever resolution we already upscale the UI to
    {
        Matrix InverseScaleMatrix = Matrix.Invert(MatrixScale); //Inversing the matrix scale since we are going from upscaled to original size
        Vector2 NewPosition = Vector2.Transform(new Vector2(ActiveMouseState.X, ActiveMouseState.Y), InverseScaleMatrix);
        return new Point((int)NewPosition.X, (int)NewPosition.Y);
    }

    private static void CheckKeybind(KeyMap Keybind) //Checks if a keybind is used
    {
        bool Activated = false;
        switch (Keybind.KeybindActivationType)
        {
            case KeybindActiveType.PressedMultiple:
                if (KeyHeldDown(Keybind.CurrentKey))
                {
                    Activated = true;
                }
                break;
            case KeybindActiveType.PressedOnce:
                if (KeyPressedOnce(Keybind.CurrentKey))
                {
                    Activated = true;
                }
                break;
        }
        if (Activated)
        {
            Keybind.KeybindAction.Invoke();
        }
    }
}
