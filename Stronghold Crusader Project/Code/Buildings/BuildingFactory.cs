namespace Stronghold_Crusader_Project.Code.Buildings;

/// <summary>
/// This class will hold all buildings possible
/// </summary>

public class BuildingFactory
{

    //Class Methods
    ContentManager Content;

    public BuildingFactory(ContentManager InputContent)
    {
        Content = InputContent;
    }

    #region Buildings
    //Where all the Buildings live
    public BuildingTemplate GetChurch(Vector2 Position)
    {
        return new BuildingTemplate(Content, "Church", Position, new Point(3, 2));
    }

    public BuildingTemplate GetHotel(Vector2 Position)
    {
        return new BuildingTemplate(Content, "Hotel", Position, new Point(2, 2));
    }
    #endregion
}

