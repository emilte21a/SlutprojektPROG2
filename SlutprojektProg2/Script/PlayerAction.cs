
using System.Runtime.CompilerServices;

public class PlayerAction
{
    public Rectangle handRectangle;
    Vector2 position;
    float timer = 2;
    public float rotation = 0;
    public Vector2 origin;

    RotationDirection rotationDirection = RotationDirection.down;

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
    public int xScale;

    public void OnClick(Vector2 playerPosition, Item currentItem, int direction)
    {

        origin = playerPosition + new Vector2(40, 40);
        position = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Game.camera);

        handRectangle.X = position.X;
        handRectangle.Y = position.Y;

        if (Raylib.IsMouseButtonPressed(0) && !isRotating && Vector2.Distance(origin, position) <= 240 && currentItem.usable)
        {
            xScale = direction;
            yScale *= -1;
            isRotating = true;

            if (rotationDirection == RotationDirection.down)
                targetRotation = 90f;

            else if (rotationDirection == RotationDirection.up)
                targetRotation = -90f;

        }

        if (isRotating)
        {
            // Increment rotation towards the target rotation
            if (rotation != targetRotation)
            {
                rotation = Raymath.Lerp(rotation, targetRotation * direction, 0.3f);
            }
            else
            {
                isRotating = false;
            }
        }
    }

    enum RotationDirection
    {
        up, down
    }
}