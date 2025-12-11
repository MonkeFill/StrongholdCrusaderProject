namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;

public class TextHoverDrawer : BaseButtonDrawer //A drawer that will draw an icon button that only changes when you hover over it
{
    //Class Variables
    private string Text;
    private float FontScale;
    private Texture2D Background;
    private Texture2D Background_Hover;
    private SpriteFont Font;
    private Color TextColour;

    //Class Methods
    public TextHoverDrawer(string Input_Text, Texture2D Input_Background, Texture2D Input_Background_Hover, float Input_FontScale, SpriteFont Input_Font, Color Input_TextColour)
    {
        Text = Input_Text;
        Background = Input_Background;
        Background_Hover = Input_Background_Hover;
        FontScale = Input_FontScale;
        Font = Input_Font;
        TextColour = Input_TextColour;
    }

    public void Draw(SpriteBatch ActiveSpriteBatch, Button ActiveButton)
    {
        Texture2D DrawTexture = Background;
        if (ActiveButton.Hover == true) //if it is being hovered over
        {
            DrawTexture = Background_Hover;
        }
        ActiveSpriteBatch.Draw(DrawTexture, ActiveButton.Bounds, Color.White);
        Vector2 TextLength = Font.MeasureString(Text) * FontScale;
        Vector2 TextPosition = new Vector2(ActiveButton.Bounds.X + ((ActiveButton.Bounds.Width - TextLength.X) / 2f), ActiveButton.Bounds.Y + ((ActiveButton.Bounds.Height - TextLength.Y) / 2f));
        ActiveSpriteBatch.DrawString(Font, Text, TextPosition, TextColour, 0f, Vector2.Zero, FontScale, SpriteEffects.None, 0f);
    }
}



