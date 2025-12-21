namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Buttons;

public abstract class BaseNavigationButton
{
    //Class Variables
    protected string PreviousIconName;
    protected string NextIconName;
    
    //Class Methods
    public BaseNavigationButton(){}
    public Button GetButton(bool Previous, ContentManager Content, Vector2 Position, Action ButtonAction, string Name, float Scale)
    {
        Texture2D Icon;
        Texture2D Icon_Hover;
        int OffSetX = 1;
        if (Previous)
        {
            Icon = Content.Load<Texture2D>(Path.Combine(GlobalButtonsFolder, PreviousIconName));
            Icon_Hover = Content.Load<Texture2D>(Path.Combine(GlobalButtonsFolder, (PreviousIconName + HoverAdd)));
            OffSetX = 0;
        }
        else
        {
            Icon = Content.Load<Texture2D>(Path.Combine(GlobalButtonsFolder, NextIconName));
            Icon_Hover = Content.Load<Texture2D>(Path.Combine(GlobalButtonsFolder, (NextIconName + HoverAdd)));
        }
        IconDrawer ButtonDrawer = new IconDrawer(Icon, Icon_Hover);
        OffSetX = (int)(Icon.Width * Scale * OffSetX);
        int Height = (int)((Icon.Height) * Scale);
        return new Button(Name, "", new Rectangle((int)Position.X - OffSetX, (int)Position.Y - Height, (int)(Icon.Width * Scale), Height), ButtonDrawer, ButtonAction);
    }
}

