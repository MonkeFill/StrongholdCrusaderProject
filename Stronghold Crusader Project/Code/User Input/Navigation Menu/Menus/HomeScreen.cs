namespace Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

public class HomeScreen : BaseMenu //First screen you get when yu open the game
{
    //Class Variables
    Texture2D Background;

    //Class Methods
    public HomeScreen(MenuManager Input_MenuManager) : base(Input_MenuManager)
    {
        Menus = Input_MenuManager;
        ContentManager Content = Menus.Content;
        
        string Assets = Path.Combine(MenuFolder, "HomeScreen");
        string ButtonAssets = Path.Combine(Assets, "Buttons");
        float FontScale = 1.5f;
        Color TextColour = Color.White;
        int ButtonOffSetX = 260;
        int ButtonOffSetY = 275;
        int ButtonHeightDifference = 75;
        
        List<string> ButtonNames = new List<string>{"New Game", "Load Game", "Setting"};
        List<Action> ButtonActions = new List<Action> 
        {
            null,
            () => Menus.AddMenu(new LoadGameMenu(Menus, false)), //When invoked
            null,
        };
        SpriteFont Font = Content.Load<SpriteFont>("DefaultFont");
        
        Background = Content.Load<Texture2D>(Path.Combine(Assets, "Background"));
        Texture2D Axe = Content.Load<Texture2D>(Path.Combine(Assets, "Selected"));
        BaseButtonDrawer TempDrawer;
        Button TempButton;
        
        for (int Count = 1; Count <= 3; Count++) //Adding all the buttons I need
        {
            Texture2D ButtonBackground = Content.Load<Texture2D>(Path.Combine(ButtonAssets, $"Button{Count}"));
            Texture2D ButtonBackgroundHover = Content.Load<Texture2D>(Path.Combine(ButtonAssets, $"Button{Count}{HoverAdd}"));
            TempDrawer = new SelectionDrawer(ButtonBackground, ButtonBackgroundHover, Axe, Font, FontScale, TextColour, ButtonNames[Count - 1]);
            int TempY = ButtonOffSetY + (ButtonHeightDifference * (Count - 1));
            TempButton = new Button(ButtonNames[Count - 1], "", new Rectangle(ButtonOffSetX, TempY, ButtonBackground.Width, ButtonBackground.Height), TempDrawer, ButtonActions[Count - 1]);
            MenuButtons.Add(TempButton);
        }
        Texture2D ExitButton = Content.Load<Texture2D>(Path.Combine(Assets, "ExitButton"));
        TempDrawer = new IconDrawer(ExitButton, Content.Load<Texture2D>(Path.Combine(Assets, $"ExitButton{HoverAdd}")));
        TempButton = new Button("Exit", "", new Rectangle(145, 590, ExitButton.Width, ExitButton.Height), TempDrawer, Menus.RemoveMenu);
        MenuButtons.Add(TempButton);

    }

    public override void Draw(SpriteBatch ActiveSpriteBatch) //Drawing the button
    {
        ActiveSpriteBatch.Draw(Background, new Rectangle(0, 0, VirtualWidth, VirtualHeight), Color.White);
        if (Menus.TopSubMenu == null) //if there is a sub menu only show the background
        {
            base.Draw(ActiveSpriteBatch);
        }
    }
}

