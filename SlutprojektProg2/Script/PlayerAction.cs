


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
            Prefab GO = WorldGeneration.prefabs.Find(g => Raylib.CheckCollisionPointRec(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Game.camera), g.rectangle));

            if (GO != null)
            {

                Item item = GO.dropType;

                GO.HP -= inventory.currentActiveItem.itemDamage;

                if (GO.HP <= 0)
                {
                    inventory.AddToInventory(item, 1);
                    Game.gameObjectsToDestroy.Add(GO);

                    WorldGeneration.prefabs.Remove(GO);
                }
            }
        }
    }
}


public enum RotationDirection
{
    up, down
}
