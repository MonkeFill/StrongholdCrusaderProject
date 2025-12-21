namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Buttons;

public class BasicTextButton
{
    //Class Methods
    public BasicTextButton(){}
    public Button GetButton(ContentManager Content, Vector2 Position, Action ButtonAction, string Text, float Scale, Color TextColour)
    {
        Texture2D TextBackground = Content.Load<Texture2D>(Path.Combine(GlobalButtonsFolder, "Text_Background"));
        Texture2D TextBackgroundHover = Content.Load<Texture2D>(Path.Combine(GlobalButtonsFolder, "Text_Background_Hover"));
        SpriteFont Font = Content.Load<SpriteFont>("DefaultFont");
        float TextScale = Scale * (25f / Font.LineSpacing); //Adding the scale onto the original text scale
        TextHoverDrawer ButtonDrawer = new TextHoverDrawer(Text, TextBackground, TextBackgroundHover, TextScale, Font, TextColour);

        int OffSetX = (int)(TextBackground.Width * Scale) / 2;
        int OffSetY = (int)(TextBackground.Height * Scale);
        return new Button(Text, "", new Rectangle((int)Position.X - OffSetX, (int)Position.Y - OffSetY, (int)(TextBackground.Width * Scale), (int)(TextBackground.Height * Scale)), ButtonDrawer, ButtonAction);
    }
}

