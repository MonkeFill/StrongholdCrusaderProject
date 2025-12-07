namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

public abstract class BaseMenu
{
    //Class Variables
    public bool IsSubMenu = false;
    protected List<Button> MenuButtons = new List<Button>();
    protected KeyManager KeybindsManager;
    protected MenuManager Menus;
    
    
    public BaseMenu(MenuManager Input_Menus)
    {
        Menus = Input_Menus;
    }

    public void Update() //Updating the menu
    {
        UpdateButtons();
        CheckIfKeybindsPressed(KeybindsManager.Keybinds);
    }
    
    public virtual void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the UI
    {
        foreach (Button ActiveButton in MenuButtons) //Draw every button
        {
            ActiveButton.Draw(ActiveSpriteBatch);
        }
    }

    private void UpdateButtons() //Updating the buttons on the UI
    {
        Button ButtonClicked = null;
        foreach (Button ActiveButton in MenuButtons) //Looping through all the buttons updating them
        {
            if (ActiveButton.Update()) //If the button is clicked on it will return true
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
}

