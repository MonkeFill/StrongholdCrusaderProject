namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;
public class LoadGameMenu : BaseMenu 
{
    //Class Variables
    Texture2D Background;
    Box MainRectangle;

    //Class Methods
    public LoadGameMenu(MenuManager Input_MenuManager) : base(Input_MenuManager)
    {
        Manager = Input_MenuManager;
        ContentManager Content = Manager.Content;
        string Assets = Path.Combine(MenuFolder, "LoadingMap");
        Background = Content.Load<Texture2D>(Path.Combine(Assets, "Background"));
        MainRectangle = new Box(new Rectangle(175, 184, 675, 400), Color.FromNonPremultiplied(0, 0, 0, 175), Content, Manager.GraphicDevice);
    }

    public override void Draw(SpriteBatch ActiveSpriteBatch)
    {
        base.Draw(ActiveSpriteBatch);
        ActiveSpriteBatch.Draw(Background, new Rectangle(0, 0, VirtualWidth, VirtualHeight), Color.White);
        MainRectangle.Draw(ActiveSpriteBatch);
    }
}
