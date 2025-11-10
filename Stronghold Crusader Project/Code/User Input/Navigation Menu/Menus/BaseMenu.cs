namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

public abstract class BaseMenu
{
    //Class Variables
    protected List<Button> MenuButtons = new List<Button>();
    protected MenuManager Manager;
    public bool IsSubMenu;
    
    //Class Methods
    public BaseMenu(MenuManager Input_MenuManager, ContentManager Content)
    {
        Manager = Input_MenuManager;
    }

    public virtual void Update(MouseState ActiveMouse)
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
                ActiveButton.OnClick.Invoke();
            }
        }
    }

    public virtual void Draw(SpriteBatch ActiveSpriteBatch)
    {
        foreach (Button ActiveButton in MenuButtons)
        {
            ActiveButton.Draw(ActiveSpriteBatch);
        }
    }

}