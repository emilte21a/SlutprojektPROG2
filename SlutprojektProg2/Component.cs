public abstract class Component
{
    public AirState airState;
}

public class PhysicsBody : Component
{

    public Vector2 acceleration = Vector2.Zero;

    public Vector2 velocity = Vector2.Zero;

    public Vector2 gravity = new Vector2(0, 9.82f);

    private float terminalVelocity = 15;

    public AirState airState;

    public enum Gravity
    {
        enabled,
        disabled
    }

    public Gravity UseGravity;

    public void Jump()
    {
        gravity.Y -= terminalVelocity * 2;
    }
}

public enum AirState
{
    inAir,
    notInAir
}

public class Collider : Component
{
    public Vector2 topRight, topLeft, bottomRight, bottomLeft;

    public Rectangle boxCollider;

    public Collider()
    {
        #region Skapa referenser till de 4 hörn på boxCollidern
        topRight = new Vector2(boxCollider.X, boxCollider.Y);
        topLeft = new Vector2(boxCollider.X + boxCollider.Width, boxCollider.Y);
        bottomRight = new Vector2(boxCollider.X, boxCollider.Y + boxCollider.Height);
        bottomLeft = new Vector2(boxCollider.X + boxCollider.Width, boxCollider.Y + boxCollider.Height);
        #endregion
    }

    // List<Tile> collidingTiles;

    // public void Update(ref Rectangle rectangle)
    // {
    //     boxCollider = rectangle;

    //     collidingTiles = WorldGeneration.tilesInWorld.Where(tile => Raylib.CheckCollisionRecs(boxCollider, tile.rectangle)).ToList();

    //     foreach (Tile tile in collidingTiles)
    //     {
    //         var collisionRec = Raylib.GetCollisionRec(boxCollider, tile.rectangle);

    //         if ((int)collisionRec.Height < (int)collisionRec.Width && (int)boxCollider.Y + boxCollider.Height > tile.rectangle.Y)//&& (bottomRight.X > tile.rectangle.X || bottomLeft.X < tile.rectangle.X))
    //             rectangle.Y -= (int)collisionRec.Height;

    //         else if ((int)collisionRec.Height < (int)collisionRec.Width && (int)boxCollider.Y < tile.rectangle.Y + tile.rectangle.Height)
    //             rectangle.Y += (int)collisionRec.Height;

    //         if ((int)collisionRec.Width < (int)collisionRec.Height && (int)boxCollider.X < tile.rectangle.X + tile.rectangle.Width && InputManager.GetAxisX() == -1)
    //             rectangle.X += (int)collisionRec.Width;

    //         else if ((int)collisionRec.Width < (int)collisionRec.Height && (int)boxCollider.X + boxCollider.Width > tile.rectangle.X && InputManager.GetAxisX() == 1)
    //             rectangle.X -= (int)collisionRec.Width;

    //         if (Raylib.CheckCollisionRecs(boxCollider, tile.rectangle) && (int)boxCollider.Y + boxCollider.Height < tile.rectangle.Y)
    //         {
    //             airState = AirState.notInAir;
    //         }
    //         else
    //             airState = AirState.inAir;

    //     }
    // }
}

public class Renderer : Component
{
    public Texture2D sprite { get; set; }
}