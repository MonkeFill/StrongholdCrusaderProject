namespace Stronghold_Crusader_Project.Code.Buildings;

/// <summary>
/// This is a template that will hold data and methods that buildings will use
/// </summary>

public abstract class BuildingTemplate
{
    //Class Variables
    string Name;
    public List<ResourceTemplate> BuildRequirement;
    public int MaxHealth;
    public int CurrentHealth;
    public Texture2D Texture;
    public Point Size;
    public Vector2 Position;
    
    //Class Methods

    #region Public Facing Methods
    //Methods that can be accessed pubically
    public BuildingTemplate(string InputName, List<ResourceTemplate> InputCost, int InputHealth, Point InputSize, Texture2D InputTexture)
    {
        Name = InputName;
        BuildRequirement = InputCost;
        MaxHealth = InputHealth;
        CurrentHealth = InputHealth;
        Texture = InputTexture;
        Size = InputSize;
        Position = new Vector2(0, 0);
        CurrentHealth = MaxHealth;
    }

    public void Update(GameTime TimeOfGame)
    {
        
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        ActiveSpriteBatch.Draw(Texture, Position, Color.White);
    }
    
    #endregion

    public Rectangle GetBounds()
    {
        return new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
    }
    
    #region Helper Methods
    //Methods to help the class
    
    #endregion
}

