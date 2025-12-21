namespace Stronghold_Crusader_Project.Code.Mapping;

public class MapTextureTile //Class for the textures of a tile
{
    //Class Variables
    public string VariantName;
    public string VariantKey;
    public Color BasicColour;
    public Texture2D Texture;


    //Class Methods
    public MapTextureTile(string Input_VariantName, string Input_VariantKey, Texture2D Input_Texture)
    {
        VariantName = Input_VariantName;
        VariantKey = Input_VariantKey;
        Texture = Input_Texture;
        //Getting the basic colour
        Rectangle TexturePixel = new Rectangle(Texture.Width / 2, Texture.Height / 2, 1, 1);
        Color[] TextureColour = new Color[1];
        Texture.GetData(0, TexturePixel, TextureColour, 0, 1);
        BasicColour = TextureColour[0];
    }

}

