using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace Stronghold_Crusader_Project.Code.Other;

public static class Camera2D
{ 
    //Class Variables
    private static Vector2 Position = Vector2.Zero; //Position of camera
    private static Vector2 TargetPosition = Vector2.Zero;
    private static float Zoom; //Camera zoom
    private static Viewport WindowFrame;  //Frame of the window 
    private static int MouseScrollWheelValue; //The value of the scroll wheel
    private static float MinZoom; //Minium zoom the camera can do
    private static float ScreenHeight; //Height of monitor
    private static float ScreenWidth; //Width of monitor
    private static Vector2 ScreenCentre; //Centre of the screen
    public static float CameraRotation; //Rotation of camera

    //Enumerated Variables
    public enum CameraAction
    {
        Move,
        Zoom,
        Rotate,
        None
    }

    //Methods
    public static void Initialize(Viewport viewport) //Initialise of a new camera
    {
        //default values set
        WindowFrame = viewport;
        ScreenHeight = WindowFrame.Height;
        ScreenWidth = WindowFrame.Width;
        MinZoom = GetMinimumZoom();
        Zoom = MinZoom;
        //Zoom = MaxZoom - MinZoom;
        ScreenCentre = new Vector2(ScreenWidth / 2f, ScreenHeight / 2f);
        CameraRotation = 0f;
        MouseScrollWheelValue = Mouse.GetState().ScrollWheelValue; //Getting the current scroll wheel value to save it
        Position = new Vector2(MapWidthSize / 2f, MapHeightSize / 2f);
    }

    public static Matrix GetViewMatrix() //Get how the camera should look and be transformed onto the game
    {
        Matrix NewTranslation = Matrix.CreateTranslation(-Position.X, -Position.Y, 0f);
        Matrix NewRotation = Matrix.CreateRotationZ(CameraRotation);
        Matrix NewScale = Matrix.CreateScale(Zoom, Zoom, 1f);
        Matrix ScreenCentreMatrix = Matrix.CreateTranslation(ScreenCentre.X, ScreenCentre.Y, 0f);
        Matrix NewViewMatrix = NewTranslation * NewRotation * NewScale * ScreenCentreMatrix;
        return NewViewMatrix;
    }

    public static void UpdateCamera(GameTime InputGameTime, CameraAction InputAction, Vector2 PositionChange , float RotationChange, float ZoomChange) //Updating the camera
    {
        float DeltaTime = (float)InputGameTime.ElapsedGameTime.TotalSeconds;
        if (InputAction != CameraAction.None)
        {
            MouseState ActiveMouseState = Mouse.GetState();
            Vector2 MouseCentre = new Vector2(ActiveMouseState.X, ActiveMouseState.Y);
            Vector2 WorldBeforeChange = CameraScreenToWorld(MouseCentre); //Getting how the world it is before 
            switch (InputAction)
            {
                case CameraAction.Move: //Move the camera to a new position
                    PositionChange.Normalize(); //Move same speed horizontally, vertically and diagonally
                    PositionChange = Vector2.Transform(PositionChange, Matrix.CreateRotationZ(-CameraRotation)); //Create the new position change based on the rotation
                    TargetPosition += PositionChange * MovementAmount * DeltaTime; //New position for where it should go
                    break;
                case CameraAction.Zoom: //Zooming into where the mouse is 
                    Zoom += (ZoomChange * ZoomSensitivity); //Adding the zoom
                    break;
                case CameraAction.Rotate:
                    CameraRotation += RotationAmount * RotationChange; //Adding rotations
                    break;
            }
            Vector2 WorldAfterChange = CameraScreenToWorld(MouseCentre);
            Vector2 WorldOffSet = WorldBeforeChange - WorldAfterChange;
            Position += WorldOffSet;
        }
        Position = Vector2.Lerp(Position, TargetPosition, MovementSpeed * DeltaTime); //Move from position to target position slowly
        ClampCamera();
       }
    private static Vector2 CameraScreenToWorld(Vector2 ScreenPosition) //Converting screen position to actual world position
    {
        Matrix InvertedMatrix = Matrix.Invert(GetViewMatrix());
        return Vector2.Transform(ScreenPosition, InvertedMatrix);
    }

    private static void ClampCamera() //A method to make sure that the camera doesn't go outside the bounds
    {
        //Camera Zoom Clamping
        Zoom = MathHelper.Clamp(Zoom, MinZoom, MaxZoom); //Making sure it isn't too high or low
        //Camera Rotation Clamping
        CameraRotation = CameraRotation % MathHelper.TwoPi; //Making it loop through 360 degrees
        if (CameraRotation < 0) //If it goes negative whilst turning left
        {
            CameraRotation += MathHelper.TwoPi; //Adding 360 to keep it within the loop
        }
        
        //Camera Position Clamping
        //Creating minimum positions for how close to origin the camera can go
        float ViewWorldWidth = ScreenWidth / Zoom;
        float ViewWorldHeight = ScreenHeight / Zoom;
        
        float MinPositionX = (ViewWorldWidth / 2f) - BorderWidth + (TileWidth / 2f);
        float MinPositionY = (ViewWorldHeight / 2f) - BorderHeight + (TileHeight / 2f);
        //Creating maximum positions for how far away from origin the camera can go
        float MaxPositionX = MapWidthSize + BorderWidth - (TileWidth / 2f) - (ViewWorldWidth / 2f);
        float MaxPositionY = MapHeightSize + BorderHeight - (ViewWorldHeight / 2f) - (TileHeight / 2f);
        
        //Checking if the map is smaller than the screen size to make sure the camera is locked 
        if (TotalMapWidth <= ViewWorldWidth)
        {
            MinPositionX = MaxPositionX = TotalMapWidth / 2f;
        }
        if (TotalMapHeight <= ViewWorldHeight)
        {
            MinPositionY = MaxPositionY = TotalMapHeight / 2f;
        }
        
        //Clamping all the positions
        Position.X = MathHelper.Clamp(Position.X, MinPositionX, MaxPositionX);
        Position.Y = MathHelper.Clamp(Position.Y, MinPositionY, MaxPositionY);
        TargetPosition.X = MathHelper.Clamp(TargetPosition.X, MinPositionX, MaxPositionX);
        TargetPosition.Y = MathHelper.Clamp(TargetPosition.Y, MinPositionY, MaxPositionY);
    }

    private static float GetMinimumZoom()
    {
        
        float ZoomNormal = Math.Max(ScreenWidth / TotalMapWidth, ScreenHeight / TotalMapHeight); //Which is bigger when normally rotated
        float ZoomRotated = Math.Max(ScreenWidth / TotalMapHeight, ScreenHeight / TotalMapWidth); //Which is bigger when rotated once
        return Math.Max(ZoomNormal, ZoomRotated);
    }
    
    public static int GetCameraRotationDegrees()
    {
        float RotationInDegrees = MathHelper.ToDegrees(CameraRotation);
        double RoundedDegrees = Math.Round(RotationInDegrees);
        return Convert.ToInt32(RoundedDegrees);
    }
}
