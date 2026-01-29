namespace Stronghold_Crusader_Project.Code.Units.UnitHandlers;

/// <summary>
/// This class will handle the animations of a unit
/// </summary>

public class UnitAnimationHandler
{
    //Class Variables

    private string UnitName;
    private int ActiveFrame;
    private double FrameTimer;
    private UnitDirection PreviousDirection;
    private UnitState PreviousState;
    private Texture2D[] FramesList;
    private Texture2D ActiveFrameTexture;
    private UnitAnimationLibrary AnimationLibrary;

    //Class Methods

    public UnitAnimationHandler(string InputUnitName, UnitAnimationLibrary InputAnimationLibrary)
    {
        UnitName = InputUnitName;
        AnimationLibrary = InputAnimationLibrary;
    }

    #region Public Facing
    //Methods that are accessabile to the public

    public void Update(GameTime TimeOfGame, UnitState ActiveState, UnitDirection ActiveDirection) //Updates the animation
    {
        if(ChangeAnimationList(ActiveState, ActiveDirection)) //If it has changed update the frames list
        {
            FramesList = AnimationLibrary.GetAnimationList(UnitName, ActiveState, ActiveDirection);
            ActiveFrame = 0;
        }

        if(FramesList == null || FramesList.Length < 1) //if no frame list has been found
        {
            LogEvent($"No frame list found for unit, {UnitName} doing {ActiveState} facing {ActiveDirection}", LogType.Warning);
            return;
        }

        if(FrameTimer > AnimationFrameSpeed) //If this is a frame to update it on
        {
            NextActiveFrame();
            FrameTimer = 0;
        }

        FrameTimer += TimeOfGame.ElapsedGameTime.TotalSeconds;
        ActiveFrameTexture = FramesList[ActiveFrame];

        PreviousState = ActiveState;
        PreviousDirection = ActiveDirection;
    }

    public void Draw(SpriteBatch ActiveSpriteBatch, Vector2 Position) //Draws the characater
    {
        //Rectangle UnitRectangle = new Rectangle((int)Position.X - (ActiveFrameTexture.Width / 2), (int)Position.Y, ActiveFrameTexture.Width, ActiveFrameTexture.Height);
        //ActiveSpriteBatch.Draw(ActiveFrameTexture, UnitRectangle, Color.White);
    }

    #endregion

    #region Helper Classes
    //Methods to help this class

    private bool ChangeAnimationList(UnitState ActiveState, UnitDirection ActiveDirection) //A class that checks if the direction or state of the unit changes
    {
        if(ActiveDirection != PreviousDirection)
        {
            return true;
        }

        if(ActiveState != PreviousState)
        {
            return true;
        }
        return false;
    }

    private void NextActiveFrame() //A class that goes to the next active frame
    {
        ActiveFrame++;
        ActiveFrame = ActiveFrame % FramesList.Length; //Clamps the frame
    }

    #endregion
}
