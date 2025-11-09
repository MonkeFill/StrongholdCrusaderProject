namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu;

public class Button
{
    //Class Variables
    public bool Active;
    public bool Hover;
    public Rectangle Bounds;
    private BaseButtonDrawer Drawer;
    public Action OnClick;
    public Button(Rectangle Input_Bounds, BaseButtonDrawer Input_Drawer, Action Input_OnClick)
    {
        Active = false;
        Hover = false;
        Bounds = Input_Bounds;
        Drawer = Input_Drawer;
        OnClick = Input_OnClick;
    }

    public bool Update(MouseState ActiveMouse)
    {
        Hover = false;
        if (Bounds.Contains(ActiveMouse.Position))
        {
            Hover = true;
            if (ActiveMouse.LeftButton == ButtonState.Pressed)
            {
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

