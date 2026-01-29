namespace Stronghold_Crusader_Project.Code.Game;

/// <summary>
/// A class for initialising a camera for the game when using the map
/// only one camera per game will be made
/// </summary>

public class Camera2D
{
    //Class Variables
    public Vector2 Position;
    public Vector2 MapSize = new Vector2((MapWidth * TileWidth) + (TileWidth / 2f), (MapHeight + 1) * (TileHeight / 2f));
    public float Zoom;
    public float Rotation;
    public Matrix ViewMatrix => GetViewMatrix();
    private Vector2 TargetPosition;
    private Viewport WindowFrame;
    private float MinZoom;
    private float MaxZoom;
    
    //Class Methods

    public Camera2D(Viewport Input_WindowFrame)
    {
        WindowFrame = Input_WindowFrame;
        Zoom = 1.5f;
        Rotation = 0f;
        Position = new Vector2(MapSize.X / 2f, MapSize.Y / 2f);
        TargetPosition = Position;
        CalculateMinZoom();
    }

    #region Public Facing
    //methods that you can access pubically
    public void Update(GameTime Time, Vector2 MovementInput, float ZoomInput, float RotationInput) //Updating the cameras position, zoom and rotation
    {
        float DeltaTime = (float)Time.ElapsedGameTime.TotalSeconds;
        if (ZoomInput != 0) //if there is a zoom
        {
            Zoom += ((ZoomInput / ZoomDelta) * ZoomSensitivity);
            ClampZoom();
        }

        if (RotationInput != 0) //if there is a rotation
        {
            Rotation += (RotationInput * RotationAmount);
            ClampRotation();
        }

        if (MovementInput != Vector2.Zero) //If there is movement
        {
            Vector2 RotatedMovement = Vector2.Transform(MovementInput, Matrix.CreateRotationZ(-Rotation));
            TargetPosition += (RotatedMovement * MovementSpeed * DeltaTime);
        }
        Position = Vector2.Lerp(Position, TargetPosition, MovementSpeed * DeltaTime);
        ClampPosition();
    }

    public Vector2 ScreenToWorld(Vector2 ScreenPosition) //Translates a vector from how it looks on the screen to the world
    {
        return Vector2.Transform(ScreenPosition, Matrix.Invert(ViewMatrix));
    }

    public Vector2 WorldToScreen(Vector2 WorldPosition) //Translates a vector from how its on the world to how it looks on the screen
    {
        return Vector2.Transform(WorldPosition, ViewMatrix);
    }
    
    #endregion

    #region Helpers 
    //Helps with parts of the camera or world
    private Matrix GetViewMatrix() //Returns the matrix for how to translate everything
    {
        Vector2 ScreenCentre = new Vector2(WindowFrame.Width / 2f, WindowFrame.Height / 2f);
        Matrix MatrixTranslation = Matrix.CreateTranslation(-Position.X, -Position.Y, 0);
        Matrix MatrixRotation = Matrix.CreateRotationZ(Rotation);
        Matrix MatrixScale = Matrix.CreateScale(Zoom, Zoom, 1);
        Matrix MatrixScreenCentre = Matrix.CreateTranslation(ScreenCentre.X, ScreenCentre.Y, 0);
        return MatrixTranslation * MatrixRotation * MatrixScale * MatrixScreenCentre;
    }

    private Vector2 GetRotatedMapSize() //Returns what the mapsize will be when taking into account rotations
    {
        float Cos = MathF.Abs(MathF.Cos(Rotation));
        float Sin = MathF.Abs(MathF.Sin(Rotation));

        //using cos and sin to figure out if the line if each line os horizontal or vertical
        float RotatedWidth = (MapSize.X * Cos) + (MapSize.X * Sin);
        float RotatedHeight = (MapSize.Y * Cos) + (MapSize.Y * Sin);

        return new Vector2(RotatedWidth, RotatedHeight);
    }
    
    private void CalculateMinZoom() //Uses the resolution of the monitor to return how much it can zoom in and out at most
    {
        MinZoom = 0.5f;
        MaxZoom = 2f;
    }
    
    #endregion

    
    #region Clampers
    //Clamps position, zoom and rotation
    private void ClampPosition()
    {
        //Creating minimum positions for how close to origin the camera can go
        float ViewWorldWidth = WindowFrame.Width / Zoom;
        float ViewWorldHeight = WindowFrame.Height / Zoom;
        
        float MinPositionX = (ViewWorldWidth / 2f) - BorderWidth + (TileWidth / 2f);
        float MinPositionY = (ViewWorldHeight / 2f) - BorderHeight + (TileHeight / 2f);
        //Creating maximum positions for how far away from origin the camera can go
        float MaxPositionX = MapSize.X + BorderWidth - (TileWidth / 2f) - (ViewWorldWidth / 2f);
        float MaxPositionY = MapSize.Y + BorderHeight - (ViewWorldHeight / 2f) - (TileHeight / 2f);
        
        //Checking if the map is smaller than the screen size to make sure the camera is locked 
        if (MapSize.X <= ViewWorldWidth)
        {
            MinPositionX = MaxPositionX = MapSize.X / 2f;
        }
        if (MapSize.Y <= ViewWorldHeight)
        {
            MinPositionY = MaxPositionY = MapSize.Y / 2f;
        }
        
        //Clamping all the positions
        Position.X = MathHelper.Clamp(Position.X, MinPositionX, MaxPositionX);
        Position.Y = MathHelper.Clamp(Position.Y, MinPositionY, MaxPositionY);
        TargetPosition.X = MathHelper.Clamp(TargetPosition.X, MinPositionX, MaxPositionX);
        TargetPosition.Y = MathHelper.Clamp(TargetPosition.Y, MinPositionY, MaxPositionY);
    }

    private void ClampZoom()
    {
        Zoom  = MathHelper.Clamp(Zoom, MinZoom, MaxZoom);
    }

    private void ClampRotation()
    {
        Rotation = MathHelper.WrapAngle(Rotation);
    }
    
    #endregion
}
