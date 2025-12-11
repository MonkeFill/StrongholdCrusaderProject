namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Other;

public static class GlobalBasicTextButton
{
    //Class Methods
    public static Button GetGlobalBasicTextButton(ContentManager Content, Vector2 Position, Action ButtonAction, string Text, float Scale, Color TextColour)
    {
        float TextScale = Scale * 0.5f; //Adding the scale onto the origianl text scale
        Texture2D TextBackground = Content.Load<Texture2D>(Path.Combine(GlobalButtonsFolder, "Text_Background"));
        Texture2D TextBackgroundHover = Content.Load<Texture2D>(Path.Combine(GlobalButtonsFolder, "Text_Background_Hover"));
        SpriteFont Font = Content.Load<SpriteFont>("DefaultFont");
        TextHoverDrawer ButtonDrawer = new TextHoverDrawer(Text, TextBackground, TextBackgroundHover, TextScale, Font, TextColour);

        int OffSetX = (int)(TextBackground.Width * Scale) / 2;
        int OffSetY = (int)(TextBackground.Height * Scale) / 2;
        return new Button(Text, "", new Rectangle((int)Position.X - OffSetX, (int)Position.Y - OffSetY, (int)(TextBackground.Width * Scale), (int)(TextBackground.Height * Scale)), ButtonDrawer, ButtonAction);
    }
}

