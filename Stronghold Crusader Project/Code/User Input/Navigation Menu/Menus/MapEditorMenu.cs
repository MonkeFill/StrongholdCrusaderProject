using System.Net.Mime;

namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

public class MapEditorMenu : BaseMenu //First screen you get when yu open the game
{
    //Class Variables
    private Texture2D Overlay;

    //Class Methods
    public MapEditorMenu(MenuManager Input_MenuManager) : base(Input_MenuManager)
    {
        Menus = Input_MenuManager;
        ContentManager Content = Menus.Content;
        KeybindsManager = new KeyManager("MapEditorMenu");
        string Assets = Path.Combine(MenuFolder, "MapEditor");
        Overlay = Content.Load<Texture2D>(Path.Combine(Assets, "Map"));
    }

    public override void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the button
    {
        ActiveSpriteBatch.Draw(Overlay, new Rectangle(0, VirtualHeight - Overlay.Height, VirtualWidth, Overlay.Height), Color.White);
        if (Menus.TopSubMenu == null) //if there is a sub menu only show the background
        {
            base.Draw(ActiveSpriteBatch);
        }
    }
}

