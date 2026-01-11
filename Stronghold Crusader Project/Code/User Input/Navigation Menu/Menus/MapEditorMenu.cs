using System.Net.Mime;

namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

public class MapEditorMenu : BaseMenu //First screen you get when yu open the game
{
    //Class Variables
    private Texture2D Overlay;
    private Texture2D Pixel;
    private Rectangle MiniMap = new Rectangle(727, 94, 128, 128);
    private GameWorld GameWorldHandler;

    //Class Methods
    public MapEditorMenu(MenuManager Input_MenuManager, GameWorld InputGameWorld) : base(Input_MenuManager)
    {
        Menus = Input_MenuManager;
        GameWorldHandler = InputGameWorld;
        ContentManager Content = Menus.Content;
        string Assets = Path.Combine(MenusFolder, "MapEditor");
        Overlay = Content.Load<Texture2D>(Path.Combine(Assets, "Map"));
        Pixel = new Texture2D(Menus.GraphicDevice, 1, 1);
        GameWorldHandler.SetupNewMap();
    }

    public override void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the button
    {
        ActiveSpriteBatch.Draw(Overlay, new Rectangle(0, VirtualScreenHeight - Overlay.Height, VirtualScreenWidth, Overlay.Height), Color.White);
        if (Menus.TopSubMenu == null) //if there is a sub menu only show the background
        {
            GameWorldHandler.DrawMinimap(ActiveSpriteBatch, MiniMap, Pixel);
            base.Draw(ActiveSpriteBatch);
        }
    }
}

