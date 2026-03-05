namespace Stronghold_Crusader_Project.Code.Game;

/// <summary>
/// This class will hold information about the player
/// </summary>

public class PlayerTemplate
{
    //Class Variables
    public UnitManager UnitHandler;
    BuildingManager BuildingHandler;
    
    
    //Class Methods
    public PlayerTemplate(ContentManager Content, GraphicsDevice Graphics, Tile[,] Map, UnitType TypeOfUnit)
    {
        UnitHandler = new UnitManager(Content, Graphics, TypeOfUnit);
        BuildingHandler = new BuildingManager(Map, Content);
    }
    
    #region Public Facing Methods
    //Methods that are public
    
    public void Update(GameTime TimeOfGame, Tile[,] Map, InputManager InputHandler, Camera2D CameraHandler, UnitManager EnemyManager) //Updates units and buildings
    {
        UnitHandler.Update(TimeOfGame, Map, InputHandler, CameraHandler, EnemyManager);
    }

    public void Draw(SpriteBatch ActiveSpriteBatch) //Draws units and buildings
    {
        UnitHandler.Draw(ActiveSpriteBatch);
        BuildingHandler.Draw(ActiveSpriteBatch);
    }

    public void HandleUnitPath(InputManager InputHandler, Camera2D CameraHandler, Tile[,] Map) //handles units pathing
    {
        UnitHandler.PathingUnits(InputHandler, CameraHandler, Map);
    }

    public void HandleUnitCreation(InputManager InputHandler, Camera2D CameraHandler) //Handles unit creation
    {
        UnitHandler.PlacingUnit(InputHandler, CameraHandler);
    }

    public void HandleUnitDeletion(InputManager InputHandler, Camera2D CameraHandler) //Handles deleting units
    {
        UnitHandler.RemovingUnit(InputHandler, CameraHandler);
    }

    public void DrawUnitSelection(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler) //Draws the unit
    {
        UnitHandler.DrawUnitSelection(ActiveSpriteBatch, InputHandler, CameraHandler);
    }

    public void DrawRemoveUnitSelection(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler)
    {
        UnitHandler.DrawRemoveUnit(ActiveSpriteBatch, InputHandler, CameraHandler);
    }
    
    public void DrawUnitPathing(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler)
    {
        UnitHandler.DrawPathing(ActiveSpriteBatch, InputHandler, CameraHandler);
    }

    public void HandleBuildingCreation(InputManager InputHandler, Camera2D CameraHandler)
    {
        BuildingHandler.CreateBuilding(InputHandler, CameraHandler);
    }

    public void DrawBuildingSelection(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler)
    {
        BuildingHandler.DrawBuildingSelection(ActiveSpriteBatch, InputHandler, CameraHandler);
    }

    public void DrawRemoveBuilding(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler)
    {
        BuildingHandler.DrawRemoveBuilding(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler)
    }

    
    #endregion
}
