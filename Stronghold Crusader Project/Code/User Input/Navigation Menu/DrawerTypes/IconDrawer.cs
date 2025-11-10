namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;

public class IconDrawer : BaseButtonDrawer //A drawer that will draw an icon button that only changes when you hover over it
{
    //Class Variables
    private Texture2D Icon;
    private Texture2D HoverIcon;
    
    //Class Methods
    public IconDrawer(Texture2D Input_Icon, Texture2D Input_HoverIcon)
    {
        Icon = Input_Icon;
        HoverIcon = Input_HoverIcon;
    }

    public void Draw(SpriteBatch ActiveSpriteBatch, Button ActiveButton)
    {
        Texture2D DrawTexture = Icon;
        if (ActiveButton.Hover == true) //if it is being hovered over
        {
            DrawTexture = HoverIcon;
        }
        ActiveSpriteBatch.Draw(DrawTexture, ActiveButton.Bounds, Color.White);
    }
}

