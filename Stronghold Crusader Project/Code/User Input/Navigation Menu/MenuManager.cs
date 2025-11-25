namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu;

public class MenuManager //Manages Menus
{
    //Class Variables
    private Stack<BaseMenu> Menus = new Stack<BaseMenu>();
    private Game ActiveGame;
    public ContentManager Content;
    public GraphicsDevice GraphicDevice;
    private BaseMenu TopMenu;
    public BaseMenu TopSubMenu;
    public string ActiveFilePath;

    //Class Methods
    public MenuManager(Game Input_ActiveGame)
    {
        ActiveGame = Input_ActiveGame;
        Content = ActiveGame.Content;
        GraphicDevice = ActiveGame.GraphicsDevice;
        BaseMenu StartingMenu = new HomeScreen(this);
        AddMenu(StartingMenu);
    }

    public void Update(MouseState ActiveMouseState) //Updates the menus logic (buttons)
    {
        Menus.ElementAt(0).Update(ActiveMouseState);
    }

    public void Draw(SpriteBatch ActiveSpriteBatch) //Draws the menus
    {
        if (TopMenu != null) //If there is a top menu draw it
        {
            TopMenu.Draw(ActiveSpriteBatch);
        }
        if (TopSubMenu != null)
        {
            TopSubMenu.Draw(ActiveSpriteBatch);
        }
    }

    private void UpdateTopMenus() //Updates what is the top menu and top sub menu
    {
        if (Menus.Count != 0) //If there are no menus
        {
            TopMenu = null;
            TopSubMenu = null;
            for (int Count = 0; TopMenu == null || TopMenu == null && Menus.Count < Menus.Count(); Count++) //Loops through all menus as long as it doesn't find a menu for both top and sub top
            {

                if (TopSubMenu == null) //If there is no top sub menu check if there is one suitable
                {
                    if (Menus.ElementAt(Count).IsSubMenu)
                    {
                        TopSubMenu = Menus.ElementAt(Count);
                    }
                }
                if (TopMenu == null)//If there is no top menu check if there is one suitable
                {
                    if (!Menus.ElementAt(Count).IsSubMenu)
                    {
                        TopMenu = Menus.ElementAt(Count);
                    }
                }
            }
        }
        else
        {
            ActiveGame.Exit(); //Exit the game
        }
    }

    public void AddMenu(BaseMenu MenuToAdd) //AddMenu replaces Push to log it and update the top menus
    {
        Menus.Push(MenuToAdd);
        LogEvent($"Added new menu {MenuToAdd}", LogType.Info);
        UpdateTopMenus();
    }

    public void RemoveMenu() //RemoveMenu replaces pop to log it and update the top menus
    {
        if (Menus.Count != 0)
        {
            LogEvent($"Removed menu {Menus.ElementAt(Menus.Count - 1)}", LogType.Info);
            Menus.Pop();
            UpdateTopMenus();
        }
        else
        {
            LogEvent($"No menus inside of stack", LogType.Error);
        }
    }

}

