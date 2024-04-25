
using System.Runtime.CompilerServices;

public class PlayerAction
{
    public Rectangle handRectangle;
    Vector2 position;
    float timer = 2;
    public float rotation = 0;
    public Vector2 origin;

    public RotationDirection rotationDirection = RotationDirection.down;

    public int attackCount = 0;

    public PlayerAction()
    {
        position = new Vector2(0, 0);
        handRectangle = new Rectangle(0, 0, 20, 20);
    }

    public void Update()
    {
        if (isRotating)
        {
            // Increment rotation towards the target rotation
            if (rotation != targetRotation)
            {
                rotation = Raymath.Lerp(rotation, targetRotation * xScale, 0.3f);
            }
            else
            {
                isRotating = false;
            }
        }

        if (attackCount % 2 == 1)
        {
            rotationDirection = RotationDirection.down;
        }
        else
            rotationDirection = RotationDirection.up;
    }

    private bool isRotating = false;
    private float targetRotation = -90f;
    private const float rotationSpeed = 90f; // Degrees per second

    public int yScale = 1;
    public int xScale = 1;

    public void OnClick(Vector2 playerPosition, int direction, Inventory inventory)
    {
        attackCount++;
        origin = playerPosition + new Vector2(40, 40);
        position = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Game.camera);

        handRectangle.X = position.X;
        handRectangle.Y = position.Y;
        xScale = direction;
        isRotating = true;
        targetRotation *= -1;
        yScale *= -1;
        if (Vector2.Distance(origin, position) <= 240)
        {
            foreach (var pF in WorldGeneration.prefabs)
            {
                if (Raylib.CheckCollisionPointRec(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Game.camera), pF.rectangle))
                {
                    Item item = pF.dropType;
                    inventory.AddToInventory(item, 1);
                }
            }
            // om Checkcollisionpointrec(musposition, valid colliders i världen typ)
            // minska hp på den colliderns ägare??
            // Om HP på den colliderns ägare == 0
            //Add to inventory colliderns ägares itemtype
        }
    }


    public enum RotationDirection
    {
        up, down
    }
}