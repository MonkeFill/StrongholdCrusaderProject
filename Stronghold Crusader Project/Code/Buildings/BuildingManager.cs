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
    private Texture2D ActiveBuildingTexture;
    private Texture2D RemoveBuildingTexture;
    private BuildingFactory BuildingsFactory;

    //Class Methodsj

    #region Public Facing Methods
    //Methods that can be accessed pubically
    public BuildingManager(Tile[,] InputMap, ContentManager Content)
    {
        Map = InputMap;
        RemoveBuildingTexture = Content.Load<Texture2D>(Path.Combine(SelectionFolder, "RemoveBuilding"));
        BuildingsFactory = new BuildingFactory(Content);
        CycleBuilding(0);
    }

    public void DrawBuildingSelection(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler) //Draws the building that is currently being selected to place
    {
        ActiveSpriteBatch.Draw(ActiveBuildingTexture, GetCorrectMouse(InputHandler, CameraHandler), Color.White * 0.75f);
    }

    public void Update(Tile[,] UpdateMap)
    {
        Map = UpdateMap;
    }

    public void Draw(SpriteBatch ActiveSpriteBatch)
    {
        foreach (Tile ActiveTile in Map)
        {
            if (ActiveTile == null)
            {
                continue;
            }
            if (ActiveTile.Building == null)
            {
                continue;
            }
            ActiveTile.Building.Draw(ActiveSpriteBatch);
        }
    }

    private Vector2 GetCorrectMouse(InputManager InputHandler, Camera2D CameraHandler)
    {
        Vector2 Position = GridHelper.GridToWorld(GridHelper.WorldToGrid(InputHandler.GetMouseWorldPosition(CameraHandler)));
        return new Vector2(Position.X - (TileSize.X / 2), Position.Y - (TileSize.Y / 2));
    }

    public void CreateBuilding(InputManager InputHandler, Camera2D CameraHandler)
    {
        HandleBuildingPlacing(InputHandler, CameraHandler);
    }

    public void DrawRemoveBuilding(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler)
    {
        ActiveSpriteBatch.Draw(RemoveBuildingTexture, GetSelectionMousePosition(InputHandler, CameraHandler), Color.White * 0.75f);
    }

    public void HandleBuildingDeletion(InputManager InputHandler, Camera2D CameraHandler)
    {
        if (InputHandler.IsLeftClickedOnce())
        {
            RemoveBuilding(GridHelper.WorldToGrid(GetCorrectMouse(InputHandler, CameraHandler)));
        }
    }
    #endregion

    #region Helper Methods
    //Methods to help the class

    private void PlaceBuilding(BuildingTemplate ActiveBuilding, Point GridPosition)
    {
        ActiveBuilding.Position = GridHelper.GridToWorld(GridPosition);
        ActiveBuilding.Position = new Vector2(ActiveBuilding.Position.X - (TileSize.X / 2), ActiveBuilding.Position.Y - (TileSize.Y / 2));
        for (int PositionX = 0; PositionX < ActiveBuilding.Size.X; PositionX++)
        {
            for (int PositionY = 0; PositionY < ActiveBuilding.Size.Y; PositionY++)
            {
                Map[GridPosition.X + PositionX, GridPosition.Y + PositionY].Building = ActiveBuilding;
            }
        }
    }

    public void RemoveBuilding(Point GridPosition)
    {
        Tile ActiveTile = Map[GridPosition.X, GridPosition.Y];
        BuildingTemplate ActiveBuilding = ActiveTile.Building;

        if (ActiveBuilding == null) //If there is no building on that tile
        {
            return;
        }
        
        Point StartGrid = GridHelper.WorldToGrid(ActiveBuilding.Position);
        
        for (int PositionX = 0; PositionX < ActiveBuilding.Size.X; PositionX++)
        {
            for (int PositionY = 0; PositionY < ActiveBuilding.Size.Y; PositionY++)
            {
                Map[GridPosition.X + PositionX, GridPosition.Y + PositionY].Building = null;
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
        switch (AvailableBuildingTypes[CurrentBuildingSelection])
        {
            case "Church":
                ActiveBuildingTexture = BuildingsFactory.GetChurch(Vector2.Zero).Texture;
                break;
            case "Hotel":
                ActiveBuildingTexture = BuildingsFactory.GetHotel(Vector2.Zero).Texture;
                break;
        }
        LogEvent($"Selected building Type: {AvailableBuildingTypes[CurrentBuildingSelection]}", LogType.Info);
    }

    private Vector2 GetSelectionMousePosition(InputManager InputHandler, Camera2D CameraHandler) //Getting how far down to draw the selection stuff
    {
        Vector2 MousePosition = InputHandler.GetMouseWorldPosition(CameraHandler);
        Vector2 BelowMousePosition = new Vector2(MousePosition.X, MousePosition.Y - (TileSize.Y / 2f));
        return BelowMousePosition;
    }
    
    private void HandleBuildingPlacing(InputManager InputHandler, Camera2D CameraHandler)
    {
        if (InputHandler.IsKeybindPressedOnce(KeyAction.NextSelection))
        {
            CycleBuilding(1);
        }
        if (InputHandler.IsKeybindPressedOnce(KeyAction.PreviousSelection))
        {
            CycleBuilding(-1);
        }
        if (InputHandler.IsLeftClickedOnce())
        {
            BuildingTemplate ActiveBuilding = null;
            switch (AvailableBuildingTypes[CurrentBuildingSelection])
            {
                case "Church":
                    ActiveBuilding = BuildingsFactory.GetChurch(GetCorrectMouse(InputHandler, CameraHandler));
                    break;
                case "Hotel":
                    ActiveBuilding = BuildingsFactory.GetHotel(GetCorrectMouse(InputHandler, CameraHandler));
                    break;
            }
            PlaceBuilding(ActiveBuilding, GridHelper.WorldToGrid(GetCorrectMouse(InputHandler, CameraHandler)));
        }
        
    }

    #endregion
}
