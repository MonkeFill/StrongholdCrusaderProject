namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Other;

public static class GlobalNavigationButton
{
    //Class Methods
    public static Button GetGlobalNavigationButton(bool Previous, ContentManager Content, Vector2 Position, Action ButtonAction, string Name, float Scale)
    {
        string Assets = Path.Combine(GlobalMenuFolder, "Buttons");
        Texture2D Icon;
        Texture2D Icon_Hover;
        int OffSetX = 1;
        if (Previous)
        {
            Icon = Content.Load<Texture2D>(Path.Combine(Assets, "Previous"));
            Icon_Hover = Content.Load<Texture2D>(Path.Combine(Assets, "Previous_hover"));
            OffSetX = 0;
        }
        else
        {
            Icon = Content.Load<Texture2D>(Path.Combine(Assets, "Next"));
            Icon_Hover = Content.Load<Texture2D>(Path.Combine(Assets, "Next_Hover"));
        }
        IconDrawer ButtonDrawer = new IconDrawer(Icon, Icon_Hover);
        OffSetX = (int)(Icon.Width * Scale * OffSetX);
        int Height = (int)((Icon.Height) * Scale);
        return new Button(Name, "", new Rectangle((int)Position.X - OffSetX, (int)Position.Y - Height, (int)(Icon.Width * Scale), Height), ButtonDrawer, ButtonAction);
    }
}

