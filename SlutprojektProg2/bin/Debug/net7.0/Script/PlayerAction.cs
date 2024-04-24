
using System.Runtime.CompilerServices;

public class PlayerAction
{
    public Rectangle handRectangle;
    Vector2 position;
    float timer = 2;
    public float rotation = 0;
    public Vector2 origin;

    public PlayerAction()
    {
        position = new Vector2(0, 0);
        handRectangle = new Rectangle(0, 0, 20, 20);
    }

    public void Update()
    {

    }

    private bool isRotating = false;
    private float targetRotation = 0f;
    private const float rotationSpeed = 90f; // Degrees per second

    public int yScale = 1;
    public int xScale = 1;

    public void OnClick(Vector2 playerPosition, Item currentItem, int direction)
    {
        xScale = direction;
        origin = playerPosition + new Vector2(40, 40);
        position = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Game.camera);
        handRectangle.X = position.X;
        handRectangle.Y = position.Y;

        if (Raylib.IsMouseButtonPressed(0) && currentItem.usable)
        {
            yScale *= -1;
        }
        // // Check if left mouse button is pressed and rotation is not already in progress
        // if (Raylib.IsMouseButtonPressed(0) && !isRotating && Vector2.Distance(origin, position) <= 240 && currentItem.usable && timer <= 0)
        // {
        //     // Set up the rotation process
        //     isRotating = true;
        //     targetRotation = 90f; // Rotate to 90 degrees (clockwise)

        //     // Reset rotation state to start from 0
        //     rotation = 0f;
        // }

        // // Handle rotation process if it's in progress
        // if (isRotating)
        // {

        //     // Increment rotation towards the target rotation
        //     if (rotation < targetRotation)
        //     {
        //         rotation = Raymath.Lerp(rotation, targetRotation * direction, 0.1f);
        //     }

        // }
    }
}