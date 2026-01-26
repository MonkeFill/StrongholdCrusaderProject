using System.Net.Mime;

namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

public class HomeScreen : BaseMenu //First screen you get when yu open the game
{
    //Class Variables
    private Texture2D Background;
    private GameWorld GameWorldHandler;

    //Class Methods
    public HomeScreen(MenuManager Input_MenuManager, GameWorld InputGameWorld) : base(Input_MenuManager)
    {
        Menus = Input_MenuManager;
        ContentManager Content = Menus.Content;
        GameWorldHandler = InputGameWorld;
        string Assets = Path.Combine(MenusFolder, "HomeScreen");
        Background = Content.Load<Texture2D>(Path.Combine(Assets, "Background"));
        //Adding the exit button
        Texture2D ActiveTexture = Content.Load<Texture2D>(Path.Combine(Assets, "ExitButton"));
        Texture2D ActiveTextureHover = Content.Load<Texture2D>(Path.Combine(Assets, $"ExitButton{HoverAddon}"));
        IconDrawer TempDrawer = new IconDrawer(ActiveTexture, ActiveTextureHover);
        MenuButtons.Add(new Button("Exit", "", new Rectangle(145, 590, ActiveTexture.Width, ActiveTexture.Height), TempDrawer, Menus.RemoveMenu));
        ActiveTexture = Content.Load<Texture2D>(Path.Combine(Assets, "SpecialButton"));
        ActiveTextureHover = Content.Load<Texture2D>(Path.Combine(Assets, $"SpecialButton{HoverAddon}"));
        TempDrawer = new IconDrawer(ActiveTexture, ActiveTextureHover);
        MenuButtons.Add(new Button("Special", "", new Rectangle(175, 500, ActiveTexture.Width, ActiveTexture.Height), TempDrawer, Menus.RemoveMenu));

        CreateMainButtons(Assets);
    }

    public override void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the button
    {
        ActiveSpriteBatch.Draw(Background, new Rectangle(0, 0, VirtualScreenWidth, VirtualScreenHeight), Color.White);
        if (Menus.TopSubMenu == null) //if there is a sub menu only show the background
        {
            base.Draw(ActiveSpriteBatch);
        }
    }

    private void CreateMainButtons(string AssetsFolder)
    {
        //Button Variables
        ContentManager Content = Menus.Content;
        SpriteFont ButtonFont = Content.Load<SpriteFont>("DefaultFont");
        string ButtonFolder = Path.Combine(AssetsFolder, "Buttons");
        int ButtonOffSetX = 260; //How far from the left the button should start from
        int ButtonOffSetY = 275; //How far from the top the button should start from
        int ButtonYDifference = 75; //How far each button should be from each other
        float TextScale = 25f / ButtonFont.LineSpacing; //How much to scale the font by using the LineSpacing
        Color TextColour = Color.White; //The colour of the text
        
        List<string> ButtonNames = new List<string> { "New Game", "Load Game", "Map Editor" };
        List<Action> ButtonActions = new List<Action>
        {
            () => GameWorldHandler.SetupNewMap(),
            () => Menus.AddMenu(new LoadGameMenu(Menus, false, GameWorldHandler)), //When invoked
            () => Menus.AddMenu(new MapEditorMenu(Menus, GameWorldHandler)),
        };

        //Button Creation
        Texture2D AxeTexture = Content.Load<Texture2D>(Path.Combine(AssetsFolder, "Selected"));
        
        for (int Count = 0; Count < ButtonActions.Count; Count++) //Looping through all the buttons to add them
        {
            Texture2D ButtonBackground = Content.Load<Texture2D>(Path.Combine(ButtonFolder, $"Button{Count + 1}"));
            Texture2D ButtonBackgroundHover = Content.Load<Texture2D>(Path.Combine(ButtonFolder, $"Button{Count + 1}{HoverAddon}"));
            BaseButtonDrawer ButtonDrawer = new SelectionDrawer(ButtonBackground, ButtonBackgroundHover, AxeTexture, ButtonFont, TextScale, TextColour, ButtonNames[Count]);
            int ButtonNewY = ButtonOffSetY + (ButtonYDifference * Count);
            Button TempButton = new Button(ButtonNames[Count], "", new Rectangle(ButtonOffSetX, ButtonNewY, ButtonBackground.Width, ButtonBackground.Height), ButtonDrawer, ButtonActions[Count]);
            MenuButtons.Add(TempButton);
        }
    }
}

