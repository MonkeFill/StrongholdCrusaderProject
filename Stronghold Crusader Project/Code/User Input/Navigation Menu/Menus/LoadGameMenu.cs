namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;
public class LoadGameMenu : BaseMenu 
{
    Texture2D Background;
    public LoadGameMenu(MenuManager Input_MenuManager, ContentManager Content) : base(Input_MenuManager, Content)
    {
        Manager = Input_MenuManager;
    }
}
