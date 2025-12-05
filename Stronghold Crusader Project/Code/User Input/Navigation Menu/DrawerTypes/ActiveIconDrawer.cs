namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;

public class ActiveIconDrawer : BaseButtonDrawer //A drawer that will draw an icon button that only changes when you hover over it
{
    //Class Variables
    private Texture2D Icon;
    private Texture2D HoverIcon;
    private Texture2D ActiveIcon;

    //Class Methods
    public ActiveIconDrawer(Texture2D Input_Icon, Texture2D Input_HoverIcon, Texture2D Input_ActiveIcon)
    {
        Icon = Input_Icon;
        HoverIcon = Input_HoverIcon;
        ActiveIcon = Input_ActiveIcon;
    }

    public void Draw(SpriteBatch ActiveSpriteBatch, Button ActiveButton)
    {
        Texture2D DrawTexture = Icon;
        if (ActiveButton.Active == true) //if it has been clicked
        {
            DrawTexture = HoverIcon;
        }
        else if (ActiveButton.Hover == true) //if it is being hovered over
        {
            DrawTexture = ActiveIcon;
        }
        ActiveSpriteBatch.Draw(DrawTexture, ActiveButton.Bounds, Color.White);
    }
}



