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
    
    private Dictionary<GameState, BaseUI> MenuUI = new Dictionary<GameState, BaseUI>();
    private Dictionary<SubGameState, BaseUI> SubMenuUI = new Dictionary<SubGameState, BaseUI>();
    public MenuManager()
    {

    }
}

