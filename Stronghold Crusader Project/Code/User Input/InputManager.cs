namespace Stronghold_Crusader_Project.Code.User_Input;

public class InputManager
{
    //New 
    private MenuManager GameMenuManager;
    private KeyboardState ActiveKeyboardState;
    private KeyboardState PreviousKeyboardState;
    private MouseState ActiveMouseState;
    private MouseState PreviousMouseState;
    private KeyMap GlobalKeybindManager;
    
    //Methods

    public InputManager(MenuManager Input_GameMenuManager)
    {
        GameMenuManager = Input_GameMenuManager;
    }
    public void Update()
    {
        //new
        PreviousKeyboardState = ActiveKeyboardState;
        PreviousMouseState = ActiveMouseState;
        ActiveKeyboardState = Keyboard.GetState();
        ActiveMouseState = Mouse.GetState();
        if (GameMenuManager.Update(ActiveKeyboardState, ActiveMouseState))
        {
            
        }
    }

    private bool MouseClicked()
    {
        if (PreviousMouseState.LeftButton != ButtonState.Pressed && ActiveMouseState.LeftButton == ButtonState.Pressed)
        {
            return true;
        }
        return false;
    }

    private bool KeybindPressed()
    {
        return true;
    }

    private Point GetScaledMousePosition() //Convert the original mouse position to whatever resolution we already upscale the UI to
    {
        Matrix InverseScaleMatrix = Matrix.Invert(MatrixScale); //Inversing the matrix scale since we are going from upscaled to original size
        Vector2 NewPosition = Vector2.Transform(new Vector2(ActiveMouseState.X, ActiveMouseState.Y), InverseScaleMatrix);
        return new Point((int)NewPosition.X, (int)NewPosition.Y);
    }
}
