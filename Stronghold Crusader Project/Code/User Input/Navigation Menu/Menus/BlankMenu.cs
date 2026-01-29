namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

public class BlankMenu : BaseMenu //First screen you get when yu open the game
{
    //Class Variables
    private Texture2D Background;
    private GameWorld GameWorldHandler;

    //Class Methods
    public BlankMenu(MenuManager Input_MenuManager, GameWorld InputGameWorld) : base(Input_MenuManager)
    {
        
    }

    public override void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the button
    {
    }

    private void CreateMainButtons(string AssetsFolder)
    {
       
    }
}

