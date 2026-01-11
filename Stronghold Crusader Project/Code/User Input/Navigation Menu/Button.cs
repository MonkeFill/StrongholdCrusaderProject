namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu;

public class Button
{
    //Class Variables
    public string Name;
    public string Category;
    public bool Active;
    public bool Hover;
    public Rectangle Bounds;
    private BaseButtonDrawer Drawer;
    public Action OnClick;
    public Button(string Input_Name, string Input_Category, Rectangle Input_Bounds, BaseButtonDrawer Input_Drawer, Action Input_OnClick)
    {
        Name = Input_Name;
        Category = Input_Category;
        Active = false;
        Hover = false;
        Bounds = Input_Bounds;
        Drawer = Input_Drawer;
        OnClick = Input_OnClick;
    }

    public bool Update(InputManager InputHandler)
    {
        Hover = false;
        if (Bounds.Contains(InputHandler.GetMousePosition()))
        {
            Hover = true;
            if (InputHandler.IsLeftClickedOnce())
            {
                    OnClick.Invoke();
                    Active = true;
                    Hover = false;
                    return true;
            }
        }
        return false;
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        Drawer.Draw(ActiveSpriteBatch, this);
    }
}

