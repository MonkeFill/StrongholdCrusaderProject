namespace Stronghold_Crusader_Project.Code.User_Input;

/// <summary>
/// A class that handles all the input detection like mouse and keyboard
/// it bridges together all the inputs managing and game actions together
/// </summary>

public class InputManager
{
    //Class Variables
    private KeyManager KeybindsManager;
    private KeyboardState CurrentKeyboardState;
    private KeyboardState PreviousKeyboardState;
    private MouseState CurrentMouseState;
    private MouseState PreviousMouseState;
    
    //Class Methods
    public InputManager(KeyManager KeybindsManagerInput)
    {
        KeybindsManager = KeybindsManagerInput;
    }

    #region Public Facing Methods
    //Classes that are going to be used outside by other classes
    public void Update()
    {
        PreviousKeyboardState = CurrentKeyboardState;
        CurrentKeyboardState = Keyboard.GetState();
        PreviousMouseState = CurrentMouseState;
        CurrentMouseState = Mouse.GetState();
    }
    
    #endregion

    #region Keyboard Methods
    //Methods for keybinds on the keyboard
    public bool IsKeybindHeldDown(KeyManager.KeyAction ActiveKeybind) //Checks if a keybind is being held down
    {
        Keys ActiveKey = KeybindsManager.GetKeyFromKeybind(ActiveKeybind);
        if (CurrentKeyboardState.IsKeyDown(ActiveKey))
        {
            return true;
        }
        return false;
    }
    
    public bool IsKeybindPressedOnce(KeyManager.KeyAction ActiveKeybind) //Checks if a keybind has been pressed once
    {
        Keys ActiveKey = KeybindsManager.GetKeyFromKeybind(ActiveKeybind);
        if (CurrentKeyboardState.IsKeyDown(ActiveKey) && !PreviousKeyboardState.IsKeyDown(ActiveKey))
        {
            return true;
        }
        return false;
    }
    
    #endregion
    
    #region Mouse Methods
    //methods for the mouse

    public bool IsLeftClickedOnce() //if the left button mouse is clicked once
    {
        if (CurrentMouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton != ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }
    
    public bool IsLeftClickHeld() //If the left button mouse is held down
    {
        if (CurrentMouseState.LeftButton == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }
    
    public bool IsRightClickedOnce() //if the right button mouse is clicked once
    {
        if (CurrentMouseState.RightButton == ButtonState.Pressed && PreviousMouseState.RightButton != ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }
    
    public bool IsRightClickHeld() //if the right button mouse is held down
    {
        if (CurrentMouseState.RightButton == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    public Vector2 GetMousePosition()
    {
        return new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
    }

    public int GetMouseChangedScrollWheel()
    {
        return CurrentMouseState.ScrollWheelValue - PreviousMouseState.ScrollWheelValue;
    }
    
    #endregion
    
}
