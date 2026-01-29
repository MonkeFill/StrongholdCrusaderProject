namespace Stronghold_Crusader_Project.Code.Game;

/// <summary>
/// The game environment is a call that will contain everything needed for the game
/// </summary>

public class PlayGame
{
    //Class Variables
    public GameEnvironment GameManager;

    public PlayGame(Game1 Game)
    {
        GameManager = new GameEnvironment(Game.Content, Game.GraphicsDevice);
        GameManager.LoadContent(Game);
    }

    #region Public Methods
    //Methods that are publicly accessible

    public void Update(GameTime TimeOfGame) //Updates all the game elements
    {
        GameManager.Update(TimeOfGame);
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        GameManager.Draw(ActiveSpriteBatch);
    }

    #endregion
}

