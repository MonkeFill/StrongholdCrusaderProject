namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;
public class LoadGameMenu : BaseMenu 
{
    //Class Variables
    private List<string> FileNames;
    private Texture2D Background;
    private Box MainRectangle;
    private Box Title;
    private Box SavesBox;
    private SpriteFont TitleFont;
    private string ActiveFile;

    //Class Methods
    public LoadGameMenu(MenuManager Input_MenuManager) : base(Input_MenuManager)
    {
        FileNames = Directory.GetFiles(SavesFolder).ToList();
        Manager = Input_MenuManager;
        ContentManager Content = Manager.Content;
        string Assets = Path.Combine(MenuFolder, "LoadingMap");
        Background = Content.Load<Texture2D>(Path.Combine(Assets, "Background"));
        MainRectangle = new Box(new Rectangle(175, 184, 675, 400), Color.FromNonPremultiplied(0, 0, 0, 175), Content, Manager.GraphicDevice);
        Title = new Box(new Rectangle(200, 210, 270, 65), Color.FromNonPremultiplied(150, 15, 15, 200), Content, Manager.GraphicDevice);
        TitleFont = Content.Load<SpriteFont>("DefaultFont");
        SavesBox = new Box(new Rectangle(500, 210, 310, 350), Color.FromNonPremultiplied(0, 0, 0, 0), Content, Manager.GraphicDevice);
        BaseButtonDrawer TestDraw = new FileSelecterDrawer("TestFile", TitleFont, Color.FromNonPremultiplied(110, 25, 0, 150), new Texture2D(Manager.GraphicDevice, 1, 1));
        Button TestButton = new Button("test", "test", new Rectangle(200, 900, 310, 25), TestDraw, null);
        MenuButtons.Add(TestButton);
    }

    public override void Draw(SpriteBatch ActiveSpriteBatch)
    {
        base.Draw(ActiveSpriteBatch);
        ActiveSpriteBatch.Draw(Background, new Rectangle(0, 0, VirtualWidth, VirtualHeight), Color.White);
        MainRectangle.Draw(ActiveSpriteBatch);
        Title.Draw(ActiveSpriteBatch);
        SavesBox.Draw(ActiveSpriteBatch);
        ActiveSpriteBatch.DrawString(TitleFont, "Load Map", new Vector2(220, 220), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
    }
}
