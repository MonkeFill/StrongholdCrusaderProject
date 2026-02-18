namespace Stronghold_Crusader_Project.Code.Buildings;

/// <summary>
/// This is a manager for all the buildings
/// </summary>

public class BuildingManager
{
    //Class Variables
    private BuildingTemplate SelectedBuilding;
    private Tile[,] Map;
    private InputManager InputHandler;
    private Camera2D Camera; 
    
    //Class Methodsj

    #region Public Facing Methods
    //Methods that can be accessed pubically
    public BuildingManager(Tile[,] InputMap, InputManager Input_InputHandler, Camera2D InputCamera)
    {
        Map = InputMap;
        InputHandler = Input_InputHandler;
        Camera = InputCamera;
    }

    public void SelectBuilding(BuildingTemplate Building)
    {
        SelectedBuilding = Building;
    }

    public void Update(GameTime TimeOfGame)
    {if (SelectedBuilding != null)
        {
            Vector2 MouseWorld = InputHandler.GetMouseWorldPosition(Camera);
            Point GridPosition = GridHelper.WorldToGrid(MouseWorld);
            
        }
        foreach (Tile ActiveTile in Map)
        {
            ActiveTile.Building.Update(TimeOfGame);
        }
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        foreach (Tile ActiveTile in Map)
        {
            ActiveTile.Building.Draw(ActiveSpriteBatch);
        }
    }
    
    #endregion
    
    #region Helper Methods
    //Methods to help the class

    private void PlaceBuilding(BuildingTemplate ActiveBuilding, Point GridPosition)
    {
        ActiveBuilding.Position = GridHelper.GridToWorld(GridPosition);
        for (int PositionX = 0; PositionX < ActiveBuilding.Size.X; PositionX++)
        {
            for (int PositionY = 0; PositionY < ActiveBuilding.Size.Y; PositionY++)
            {
                Map[GridPosition.X + PositionY, GridPosition.Y + PositionY].Building = ActiveBuilding;
            }
        }
        
    }

    private void RemoveBuilding(Point GridPosition)
    {
        Tile ActiveTile = Map[GridPosition.X, GridPosition.Y];
        BuildingTemplate ActiveBuilding = ActiveTile.Building;

        if (ActiveBuilding != null) //If there is no building on that tile
        {
            return;
        }
        
        Point StartGrid = GridHelper.WorldToGrid(ActiveBuilding.Position);
        
        for (int PositionX = 0; PositionX < ActiveBuilding.Size.X; PositionX++)
        {
            for (int PositionY = 0; PositionY < ActiveBuilding.Size.Y; PositionY++)
            {
                Map[GridPosition.X + PositionY, GridPosition.Y + PositionY].Building = null;
            }
        }
    }
    
    #endregion
}
