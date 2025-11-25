namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;

public class SelectionDrawer : BaseButtonDrawer
{
    //Class Variables
    private Texture2D Background;
    private Texture2D HoverBackground;
    private Texture2D HoverIcon;
    private SpriteFont Font;
    private float FontScale;
    private Color TextColour;
    private string Text;

    //Class Methods
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
        Texture2D BackgroundDrawTexture = Background;
        if (ActiveButton.Hover == true) //if it is being hovered over
        {
            BackgroundDrawTexture = HoverBackground;
        }
        ActiveSpriteBatch.Draw(BackgroundDrawTexture, ActiveButton.Bounds, Color.White);
        Vector2 TextSize = Font.MeasureString(Text) * FontScale;
        Vector2 TextPosition = new Vector2(ActiveButton.Bounds.X + 35, ActiveButton.Bounds.Y + (TextSize.Y / 2f));
        ActiveSpriteBatch.DrawString(Font, Text, TextPosition, TextColour, 0f, Vector2.Zero, FontScale, SpriteEffects.None, 0f);
        if (ActiveButton.Hover == true)
        {
            Vector2 IconPosition = new Vector2(ActiveButton.Bounds.X - 75, ActiveButton.Bounds.Y - 10);
            ActiveSpriteBatch.Draw(HoverIcon, IconPosition, Color.White);
        }
    }
}

