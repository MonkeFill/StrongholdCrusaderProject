namespace Stronghold_Crusader_Project.Code.Buildings;

/// <summary>
/// This is a template that will hold data and methods that buildings will use
/// </summary>

public abstract class BuildingTemplate
{
    //Class Variables
    string Name;
    Texture2D Texture;
    Point Position;
    Vector2 Size;
    
    //Class Methods

    #region Public Facing Methods
    //Methods that can be accessed pubically
    public BuildingTemplate(ContentManager Content, string InputName, string InputTexture, Point InputPosition, Vector2 InputSize)
    {
        Name = InputName;
        Texture = Content.Load<Texture2D>(InputTexture);
        Position = InputPosition;
        Size = InputSize;
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {/*
        ActiveSpriteBatch.Draw(Texture, Position, Color.White);*/
    }
    
    #endregion

    public Rectangle GetBounds()
    {
        return new Rectangle(0,0,0,0);
        /*
        return new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);*/
    }
    
    #region Helper Methods
    //Methods to help the class
    
    #endregion
}

