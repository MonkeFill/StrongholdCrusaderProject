namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;

public class SelectionDrawer : BaseButtonDrawer
{
    private Texture2D Background;
    private Texture2D HoverBackground;
    private Texture2D HoverIcon;
    private SpriteFont Font;
    private float FontScale;
    private Color TextColour;
    private string Text;
    
    public SelectionDrawer(Texture2D Input_background, Texture2D Input_HoverBackground, Texture2D Input_HoverIcon, SpriteFont Input_Font, float Input_FontScale, Color Input_TextColour, string Input_Text)
    {
        Background = Input_background;
        HoverBackground = Input_HoverBackground;
        HoverIcon = Input_HoverIcon;
        Font = Input_Font;
        FontScale = Input_FontScale;
        TextColour = Input_TextColour;
        Text = Input_Text;
    }

    public void Draw(SpriteBatch ActiveSpriteBatch, Button ActiveButton)
    {
        
    }
}

