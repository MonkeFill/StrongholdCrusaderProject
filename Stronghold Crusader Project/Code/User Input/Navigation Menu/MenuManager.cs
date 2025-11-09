using Stronghold_Crusader_Project.Code.User_Input.User_Interface;

namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu;

public class MenuManager
{
    //Class Variables
    enum GameState //What the game is currently doing
    {
        MainMenu,
        Game,
        Error,
        Mission,
        Exit
    }

    enum SubGameState //What is going on above GameState
    {
        Settings,
        LoadGame,
        SaveGame
    }
    
    private Dictionary<GameState, BaseMenu> MenuUI = new Dictionary<GameState, BaseMenu>();
    private Dictionary<SubGameState, BaseMenu> SubMenuUI = new Dictionary<SubGameState, BaseMenu>();
    public MenuManager()
    {

    }
}

