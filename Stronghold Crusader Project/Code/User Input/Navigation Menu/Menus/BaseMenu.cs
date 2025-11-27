namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

public abstract class BaseMenu //Abstract class for all menus
{
    //Class Variables
    protected List<Button> MenuButtons = new List<Button>();
    protected MenuManager Manager;
    public bool IsSubMenu;
    
    //Class Methods
    public BaseMenu(MenuManager Input_MenuManager)
    {
        Manager = Input_MenuManager;
    }

    public virtual void Update(MouseState ActiveMouse) //Updating the menu buttons
    {
        foreach (Button ActiveButton in MenuButtons)
        {
            ActiveButton.Hover = false;
            if (ActiveButton.Update(ActiveMouse)) //If the mouse gets clicked on
            {
                if (ActiveButton.Category != "")
                {
                    foreach (Button SecondButton in MenuButtons)
                    {
                        if (SecondButton.Category == ActiveButton.Category)
                        {
                            SecondButton.Active = false;
                        }
                    }
                }
                ActiveButton.OnClick.Invoke(); //if button is clicked on run its code
            }
        }
    }

    public virtual void Draw(SpriteBatch ActiveSpriteBatch) //Drawing all buttons in a menu
    {
        foreach (Button ActiveButton in MenuButtons)
        {
            ActiveButton.Draw(ActiveSpriteBatch);
        }
    }

}