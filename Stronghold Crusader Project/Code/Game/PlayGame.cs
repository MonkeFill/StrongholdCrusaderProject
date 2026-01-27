namespace Stronghold_Crusader_Project.Code.Game;

/// <summary>
/// The game environment is a call that will contain everything needed for the game
/// </summary>

public class PlayGame
{
    //Class Variables
    GameEnvironment GameManager;
    bool GameLoaded;
    bool GameActive;

    public PlayGame(ContentManager Content, GraphicsDevice Graphics)
    {
        GameManager = new GameEnvironment(Content, Graphics);
    }

    #region Public Methods
    //Methods that are publicly accessible

    public void Update(GameTime TimeOfGame) //Updates all the game elements
    {
        if (!GameActive)
        {
            return;
        }
        if(!GameLoaded)
        {
            return;
        }

        GameManager.Update(TimeOfGame);
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        if (!GameLoaded)
        {
            return;
        }
        GameManager.Draw(ActiveSpriteBatch);
    }

    #endregion
}

