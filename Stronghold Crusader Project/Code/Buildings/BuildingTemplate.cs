namespace Stronghold_Crusader_Project.Code.Buildings;

/// <summary>
/// This is a template that will hold data and methods that buildings will use
/// </summary>

public class BuildingTemplate
{
    //Class Variables
    private string Name;
    public Texture2D Texture;
    public Vector2 Position;
    public Point Size;

    //Class Methods

    #region Public Facing Methods
    //Methods that can be accessed pubically
    public BuildingTemplate(ContentManager Content, string InputName, Vector2 InputPosition, Point InputSize)
    {
        Name = InputName;
        try
        {
            Texture = Content.Load<Texture2D>(Path.Combine(BuildingsFolder, InputName));
        }
        catch
        {
            LogEvent($"Texture for {InputName}, not found", LogType.Error);
        }
        Position = InputPosition;
        Size = InputSize;
    }

    public Rectangle ReturnBounds()
    {
        return new Rectangle((int)Position.X, (int)Position.Y, (Size.X * TileSize.X), (Size.Y * TileSize.Y));
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        ActiveSpriteBatch.Draw(Texture, Position, Color.White);
    }

    #endregion

    #region Helper Methods
    //Methods to help the class

    #endregion
    }

