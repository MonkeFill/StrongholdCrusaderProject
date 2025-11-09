namespace Stronghold_Crusader_Project.Code.User_Input.User_Interface;

public abstract class BaseMenu
{
    //Class Variables
    public bool IsSubMenu = false;
    protected List<Button> MenuButtons = new List<Button>();
    protected MenuManager Manager;
    
    
    public BaseMenu(MenuManager Input_Manager)
    {
        Manager = Input_Manager;
    }

    public void Update(MouseState ActiveMouse) //Updating the buttons within the UI
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
    
    public virtual void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the UI
    {
        foreach (Button ActiveButton in MenuButtons) //Draw every button
        {
            ActiveButton.Draw(ActiveSpriteBatch);
        }
    }
}

