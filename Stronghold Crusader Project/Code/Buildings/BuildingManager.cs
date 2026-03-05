namespace Stronghold_Crusader_Project.Code.Buildings;

/// <summary>
/// This is a manager for all the buildings
/// </summary>

public class BuildingManager
{
    //Class Variables
    private Tile[,] Map;
    private List<string> AvailableBuildingTypes = new List<string> { "Hotel", "Church" };
    private int CurrentBuildingSelection = 0;
    private BuildingTemplate ActiveBuilding;
    private Texture2D RemoveBuildingTexture;

    //Class Methodsj

    #region Public Facing Methods
    //Methods that can be accessed pubically
    public BuildingManager(Tile[,] InputMap, ContentManager Content)
    {
        Map = InputMap;
        RemoveBuildingTexture = Content.Load<Texture2D>(Path.Combine(SelectionFolder, "RemoveBuilding"));
    }

    public void DrawBuildingSelection(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler) //Draws the building that is currently being selected to place
    {
        Vector2 BuildingPosition = GridHelper.GridToWorld(GridHelper.WorldToGrid(InputHandler.GetMouseWorldPosition(CameraHandler))); //Getting the mouse position and converting it to grid and then back to position to lock it to the centre of a tile
        ActiveSpriteBatch.Draw(ActiveBuilding.Texture, ActiveBuilding.ReturnBounds(), Color.White * 0.75f);
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        foreach (Tile ActiveTile in Map)
        {
            ActiveTile.Building.Draw(ActiveSpriteBatch);
        }
    }

    public void CreateBuilding(InputManager InputHandler, Camera2D CameraHandler)
    {

    }

    public void DrawRemoveBuilding(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler)
    {
        ActiveSpriteBatch.Draw(RemoveBuildingTexture, GetSelectionMousePosition(InputHandler, CameraHandler), Color.White * 0.75f);
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

    private void CycleBuilding(int Direction) //A class that will go through the buildings available to place
    {
        CurrentBuildingSelection += Direction;
        if (CurrentBuildingSelection < 0) //If it has gone minus to loop it
        {
            CurrentBuildingSelection = AvailableBuildingTypes.Count - 1;
        }
        else if (CurrentBuildingSelection >= AvailableBuildingTypes.Count) //if it has gone higher then available amount, loop it
        {
            CurrentBuildingSelection = 0;
        }

        LogEvent($"Selected building Type: {AvailableBuildingTypes[CurrentBuildingSelection]}", LogType.Info);
    }

    private Vector2 GetSelectionMousePosition(InputManager InputHandler, Camera2D CameraHandler) //Getting how far down to draw the selection stuff
    {
        Vector2 MousePosition = InputHandler.GetMouseWorldPosition(CameraHandler);
        Vector2 BelowMousePosition = new Vector2(MousePosition.X, MousePosition.Y - (TileSize.Y / 2f));
        return BelowMousePosition;
    }

    #endregion
}
