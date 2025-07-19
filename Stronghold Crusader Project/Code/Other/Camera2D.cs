using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Stronghold_Crusader_Project.Code.Other;

public static class Camera2D
{
    
    static readonly int MaxMapHeight = GlobalConfig.MaxMapHeight;
    static readonly int MaxMapWidth = GlobalConfig.MaxMapWidth;
    static readonly float MaxZoom = GlobalConfig.MaxZoom;
    static readonly float ZoomSensitivity = GlobalConfig.ZoomSensitivity;
    static readonly float MovementAmount = GlobalConfig.MovementAmount;
    static readonly float MovementSpeed = GlobalConfig.MovementSpeed;
    static readonly float RotationAmount = GlobalConfig.RotationAmount;
    
    static Vector2 Position = Vector2.Zero; //Position of camera
    private static Vector2 TargetPosition = Vector2.Zero;
    private static float Zoom; //Camera zoom
    public static float Rotation; //Rotation of camera
    static Viewport WindowFrame;  //Frame of the window 
    static int PreviousScrollWheelValue;
    private static float HalfScreenHeight => WindowFrame.Height / 2f / Zoom;
    private static float HalfScreenWidth => WindowFrame.Width / 2f / Zoom;
    private static float MinZoom;

    public enum CameraAction
    {
        Move,
        Zoom,
        Rotate,
        None
    }

    public static void Initialize(Viewport viewport) //Initialise of a new camera
    {
        //default values set
        Rotation = 0f;
        WindowFrame = viewport;
        PreviousScrollWheelValue = Mouse.GetState().ScrollWheelValue; //Getting the current scroll wheel value to save it
       //Make sure that zoom min is fitting the whole map on either vertical or horizontal
       float ZoomToFitVer = Math.Abs(MaxMapHeight / WindowFrame.Height);
       float ZoomToFitHoriz = Math.Abs(MaxMapWidth / WindowFrame.Width);
       MinZoom = Math.Min(ZoomToFitVer, ZoomToFitHoriz);
       MinZoom = Math.Max(MinZoom, 0.5f);
       Zoom = MinZoom;
       Position = new Vector2(MaxMapWidth / 2f, MaxMapHeight / 2f);
    }

    public static Matrix GetViewMatrix() //Get how the camera should look and be transformed onto the game
    {
        Matrix NewTranslation = Matrix.CreateTranslation(-Position.X, -Position.Y, 0f);
        Matrix NewRotation = Matrix.CreateRotationZ(Rotation);
        Matrix NewScale = Matrix.CreateScale(Zoom, Zoom, 1f);
        Matrix ScreenCentre = Matrix.CreateTranslation(WindowFrame.Width / 2f, WindowFrame.Height / 2f, 0f);
        Matrix NewViewMatrix = NewTranslation * NewRotation * NewScale * ScreenCentre;
        return NewViewMatrix;
    }

    public static void UpdateCamera(GameTime InputGameTime, CameraAction InputAction, Vector2 PositionChange , float RotationChange, float ZoomChange) //Updating the camera
    {
        float DeltaTime = (float)InputGameTime.ElapsedGameTime.TotalSeconds;
        switch (InputAction)
        {
            case  CameraAction.Move: //Move the camera to a new position
                PositionChange.Normalize(); //Move same speed horizontally, vertically and diagonally
                PositionChange = Vector2.Transform(PositionChange, Matrix.CreateRotationZ(Rotation)); //Create the new position change based on the rotation
                TargetPosition += PositionChange * MovementAmount * DeltaTime; //New position for where it should go
                break;
            case  CameraAction.Zoom: //Zooming into where the mouse is 
                MouseState ActiveMouse = Mouse.GetState(); //Getting coordinates of mouse
                Vector2 MouseScreenPreZoom = CameraScreenToWorld(new Vector2(ActiveMouse.X, ActiveMouse.Y)); //Turning it into coordinates of the world
                Zoom += (ZoomChange * ZoomSensitivity); //Adding the zoom
                Zoom = MathHelper.Clamp(Zoom, MinZoom, MaxZoom); //Making sure it isn't too high or low
                Vector2 MouseScreenAfterZoom = CameraScreenToWorld(new Vector2(ActiveMouse.X, ActiveMouse.Y)); //Getting coordinates of mouse after zoom
                Vector2 MouseZoomOffset = MouseScreenPreZoom - MouseScreenAfterZoom; //Finding difference
                Position += MouseZoomOffset; //Adding difference to camera position
                TargetPosition = Position; //Target Position to use lerp to go to it
                break;
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
        float MaxPositionX = MaxMapWidth - HalfScreenWidth; //MaxHeight Camera can go
        float MaxPositionY = MaxMapHeight - HalfScreenHeight;

        float MinPositionX = HalfScreenWidth; //Smallest height camera can go
        float MinPositionY = HalfScreenHeight;

        if (MaxMapWidth <= HalfScreenWidth * 2) //Checking if the tiles fit all on the screen
        {
            MinPositionX = MaxPositionX = MaxMapWidth / 2f;
        }
        if (MaxMapHeight <= HalfScreenHeight * 2)
        {
            MinPositionY = MaxPositionY = MaxMapHeight / 2f;
        }
        Position.X = MathHelper.Clamp(Position.X, MinPositionX, MaxPositionX);
        Position.Y = MathHelper.Clamp(Position.Y, MinPositionY, MaxPositionY);
        TargetPosition.X = MathHelper.Clamp(Position.X, MinPositionX, MaxPositionX);
        TargetPosition.Y = MathHelper.Clamp(Position.Y, MinPositionY, MaxPositionY);
    }
}
