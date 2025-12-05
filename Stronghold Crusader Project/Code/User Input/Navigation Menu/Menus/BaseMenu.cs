namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

public abstract class BaseMenu
{
    //Class Variables
    public bool IsSubMenu = false;
    protected List<Button> MenuButtons = new List<Button>();
    protected KeyMap KeybindsManager;
    protected MenuManager Menus;
    
    
    public BaseMenu(MenuManager Input_Menus)
    {
        Menus = Input_Menus;
    }

    public bool Update(KeyboardState ActiveKeyboard, MouseState ActiveMouse) //Updating everything the menu contains
    {
        UpdateButtons(ActiveMouse);
        if (UpdateKeyPressed(ActiveKeyboard, ActiveMouse)) //If a keybind is pressed it will return true
        {
            return true;
        }
        return false;
    }
    
    public virtual void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the UI
    {
        foreach (Button ActiveButton in MenuButtons) //Draw every button
        {
            ActiveButton.Draw(ActiveSpriteBatch);
        }
    }

    private void UpdateButtons(MouseState ActiveMouse) //Updating the buttons on the UI
    {
        Button ButtonClicked = null;
        foreach (Button ActiveButton in MenuButtons) //Looping through all the buttons updating them
        {
            if (ActiveButton.Update(ActiveMouse)) //If the button is clicked on it will return true
            {
                ButtonClicked = ActiveButton;
            }
        }

        if (ButtonClicked != null) //A button got clicked
        {
            foreach (Button ActiveButton in MenuButtons) //Looping through all buttons
            {
                if (ActiveButton.Category == ButtonClicked.Category && ActiveButton != ButtonClicked) //Button is in same category and is not the same as the clicked button
                {
                    ActiveButton.Active = false; //Making it false
                }
            }
        }
    }

    private bool UpdateKeyPressed(KeyboardState ActiveKeyboard, MouseState ActiveMouse) //Checking to see if any of the menus keybinds are pressed
    {
        return false;
    }
}

