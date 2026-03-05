using System.Diagnostics;

namespace Stronghold_Crusader_Project.Code.Game;

/// <summary>
/// A class for managing the players within the game
/// </summary>

public class PlayerManager
{
    //Class Variables
    PlayerTemplate AllyPlayer;
    PlayerTemplate EnemyPlayer;
    SelectionState SelectionType = SelectionState.None;
    
    //Class Methods
    public PlayerManager(ContentManager Content, GraphicsDevice Graphics, Tile[,] Map)
    {
        AllyPlayer = new PlayerTemplate(Content, Graphics, Map, UnitType.Ally);
        EnemyPlayer = new PlayerTemplate(Content, Graphics, Map, UnitType.Enemy);
    }
    
    #region Public Facing Methods
    //Methods that can be accessed to the public

    public void Update(GameTime TimeOfGame, Tile[,] Map, InputManager InputHandler, Camera2D CameraHandler) //Updates the players
    {
        CheckSelectionType(InputHandler);
        InputSelection(InputHandler, CameraHandler, Map);
        AllyPlayer.Update(TimeOfGame, Map, InputHandler, CameraHandler, EnemyPlayer.UnitHandler);
        EnemyPlayer.Update(TimeOfGame, Map, InputHandler, CameraHandler, AllyPlayer.UnitHandler);
    }

    public void Draw(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler) //Draws the players units and building
    {
        AllyPlayer.Draw(ActiveSpriteBatch);
        EnemyPlayer.Draw(ActiveSpriteBatch);
        DrawSelection(ActiveSpriteBatch, InputHandler, CameraHandler);
    }
    
    #endregion
    
    #region Helper Methods
    //Methods that help the class

    private void CheckSelectionType(InputManager InputHandler) //Checks if the type of selection changes
    {
        if (InputHandler.IsKeybindPressedOnce(KeyAction.PathingSwitch))
        {
            SelectionType = SelectionState.Pathing;
        }
        if (InputHandler.IsKeybindPressedOnce(KeyAction.EnemyPathingSwitch))
        {
            SelectionType = SelectionState.EnemyPathing;
        }
        if (InputHandler.IsKeybindPressedOnce(KeyAction.UnitSwitch))
        {
            SelectionType = SelectionState.CreateUnit;
        }
        if (InputHandler.IsKeybindPressedOnce(KeyAction.BuildingSwitch))
        {
            SelectionType = SelectionState.CreateBuilding;
        }
        if (InputHandler.IsKeybindPressedOnce(KeyAction.EnemyUnitSwitch))
        {
            SelectionType = SelectionState.CreateEnemyUnit;
        }
        if (InputHandler.IsKeybindPressedOnce(KeyAction.RemoveUnitSwitch))
        {
            SelectionType = SelectionState.RemoveUnit;
        }
        if (InputHandler.IsKeybindPressedOnce(KeyAction.RemoveBuildingSwitch))
        {
            SelectionType = SelectionState.RemoveBuilding;
        }
        if (InputHandler.IsKeybindPressedOnce(KeyAction.RemoveEnemyUnitSwitch))
        {
            SelectionType = SelectionState.RemoveEnemyUnit;
        }
    }

    private void InputSelection(InputManager InputHandler, Camera2D CameraHandler, Tile[,] Map) //Handles the input depending on the selection type
    {
        switch (SelectionType)
        {
            case SelectionState.Pathing:
                AllyPlayer.HandleUnitPath(InputHandler, CameraHandler, Map);
                break;
            case SelectionState.EnemyPathing:
                EnemyPlayer.HandleUnitPath(InputHandler, CameraHandler, Map);
                break;
            case SelectionState.CreateUnit:
                AllyPlayer.HandleUnitCreation(InputHandler, CameraHandler);
                break;
            case SelectionState.CreateBuilding:
                AllyPlayer.HandleBuildingCreation(InputHandler, CameraHandler);
                break;
            case SelectionState.CreateEnemyUnit:
                EnemyPlayer.HandleUnitCreation(InputHandler, CameraHandler);
                break;
            case SelectionState.RemoveUnit:
                AllyPlayer.HandleUnitDeletion(InputHandler, CameraHandler);
                break;
            case SelectionState.RemoveBuilding:
                AllyPlayer.HandleBuildingDeletion(InputHandler, CameraHandler);
                break;
            case SelectionState.RemoveEnemyUnit:
                EnemyPlayer.HandleUnitDeletion(InputHandler, CameraHandler);
                break;
        }
    }

    private void DrawSelection(SpriteBatch ActiveSpriteBatch, InputManager InputHandler, Camera2D CameraHandler) //Handles drawing different parts
    {
        switch (SelectionType)
        {
            case SelectionState.Pathing:
                AllyPlayer.DrawUnitPathing(ActiveSpriteBatch, InputHandler, CameraHandler);
                break;
            case SelectionState.EnemyPathing:
                EnemyPlayer.DrawUnitPathing(ActiveSpriteBatch, InputHandler, CameraHandler);
                break;
            case SelectionState.CreateUnit:
                AllyPlayer.DrawUnitSelection(ActiveSpriteBatch, InputHandler, CameraHandler);
                break;
            case SelectionState.CreateBuilding:
                AllyPlayer.DrawBuildingSelection(ActiveSpriteBatch, InputHandler, CameraHandler);
                break;
            case SelectionState.CreateEnemyUnit:
                EnemyPlayer.DrawUnitSelection(ActiveSpriteBatch, InputHandler, CameraHandler);
                break;
            case SelectionState.RemoveUnit:
                AllyPlayer.DrawRemoveUnitSelection(ActiveSpriteBatch, InputHandler, CameraHandler);
                break;
            case SelectionState.RemoveBuilding:
                AllyPlayer.DrawRemoveBuilding(ActiveSpriteBatch, InputHandler, CameraHandler);
                break;
            case SelectionState.RemoveEnemyUnit:
                EnemyPlayer.DrawRemoveUnitSelection(ActiveSpriteBatch, InputHandler, CameraHandler);
                break;
        }
    }
    
    #endregion
}

