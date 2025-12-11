namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;
public class LoadGameMenu : BaseFileMenu //Menu for loading maps
{

    //Class Methods
    public LoadGameMenu(MenuManager Input_MenuManager, bool InputIsSubMenu) : base(Input_MenuManager, InputIsSubMenu, Path.Combine(MenuFolder, "LoadingMap"), "LoadMap") {}
}
