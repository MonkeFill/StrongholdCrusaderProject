using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace Stronghold_Crusader_Project.Code.Global.Other;

public static class Camera2D //Class that controls the camera for the game
{ 
    //Enumerated Variables
    public enum PossibleCameraAction
    {
        Move,
        Zoom,
        Rotate
    }
    
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
        TargetPosition = Position;
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

    public static void UpdateCamera(GameTime InputGameTime, List<PossibleCameraAction> CameraActions,Vector2 PositionChange , float RotationChange, float ZoomChange) //Updating the camera
    {
        float DeltaTime = (float)InputGameTime.ElapsedGameTime.TotalSeconds;
        if (CameraActions.Count > 0) //If there is camera action
        {
            MouseState ActiveMouseState = Mouse.GetState();
            Vector2 MouseCentre = new Vector2(ActiveMouseState.X, ActiveMouseState.Y);
            Vector2 WorldBeforeChange = CameraScreenToWorld(MouseCentre); //Getting how the world it is before 
            if (CameraActions.Contains(PossibleCameraAction.Move)) //Move the camera to a new position
            {
                if (PositionChange != Vector2.Zero) //Making sure it isn't zero
                {
                    PositionChange.Normalize(); //Move same speed horizontally, vertically and diagonally
                }
                PositionChange = Vector2.Transform(PositionChange, Matrix.CreateRotationZ(-CameraRotation)); //Create the new position change based on the rotation
                TargetPosition += PositionChange * MovementAmount * DeltaTime; //New position for where it should go
                CameraActions.Remove(PossibleCameraAction.Move);
                ClampCameraMovement();
            }
            if (CameraActions.Contains(PossibleCameraAction.Zoom)) //Zooming into where the mouse is 
            {
                Zoom += (ZoomChange * ZoomSensitivity); //Adding the zoom
                ClampCameraZoom();
                CameraActions.Remove(PossibleCameraAction.Zoom);
            }
            if (CameraActions.Contains(PossibleCameraAction.Rotate))
            {
                CameraRotation += RotationAmount * RotationChange; //Adding rotations
                CameraActions.Remove(PossibleCameraAction.Rotate);
                ClampCameraRotation();
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

    private static void ClampCamera() //Clamping all the camera movements
    {
        ClampCameraMovement();
        ClampCameraRotation();
        ClampCameraZoom();
    }
    private static void ClampCameraMovement() //A method to make sure that the camera movement
    {
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

    private static void ClampCameraRotation() //Making sure the camera has the same rotation format
    {
        CameraRotation = CameraRotation % MathHelper.TwoPi; //Making it loop through 360 degrees
        if (CameraRotation < 0) //If it goes negative whilst turning left
        {
            CameraRotation += MathHelper.TwoPi; //Adding 360 to keep it within the loop
        }
    }

    private static void ClampCameraZoom() //Making sure zom doesn't leave the bounds
    {
        Zoom = MathHelper.Clamp(Zoom, MinZoom, MaxZoom);
    }

    private static float GetMinimumZoom() //Method to calculate how much the camera can zoom out
    {
        
        float ZoomNormal = Math.Max(ScreenWidth / TotalMapWidth, ScreenHeight / TotalMapHeight); //Which is bigger when normally rotated
        float ZoomRotated = Math.Max(ScreenWidth / TotalMapHeight, ScreenHeight / TotalMapWidth); //Which is bigger when rotated once
        return Math.Max(ZoomNormal, ZoomRotated);
    }
    
    public static int GetCameraRotationDegrees() //Getting the camera rotation in degrees
    {
        float RotationInDegrees = MathHelper.ToDegrees(CameraRotation);
        double RoundedDegrees = Math.Round(RotationInDegrees);
        return Convert.ToInt32(RoundedDegrees);
    }
}
