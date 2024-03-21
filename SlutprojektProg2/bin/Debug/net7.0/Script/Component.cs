public abstract class Component
{
    public AirState airState;
}

public class PhysicsBody : Component
{

    public Vector2 acceleration = Vector2.Zero;

    public Vector2 velocity = Vector2.Zero;

    public Vector2 gravity = new Vector2(0, 50f);

    public AirState airState;

    public PhysicsBody()
    {
        acceleration = Vector2.Zero;
        velocity = Vector2.Zero;
    }

    public enum Gravity
    {
        enabled,
        disabled
    }

    public Gravity UseGravity;

    public void Jump(PhysicsBody physicsBody, float jumpForce)
    {
        physicsBody.velocity.Y = -jumpForce * Raylib.GetFrameTime();
    }

    public void MovePlayer(PhysicsBody physicsBody, float speed)
    {
        physicsBody.velocity.X = InputManager.GetAxisX() * speed * Raylib.GetFrameTime();
        //System.Console.WriteLine("acceleration: " + physicsBody.acceleration + " Velocity: " + physicsBody.velocity);
    }
}

public enum AirState
{
    inAir,
    grounded
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
}

public class Renderer : Component
{
    public Texture2D sprite { get; set; }
}

public class AudioPlayer : Component
{
    public Sound audioClip { get; set; }

    float audioVolume { get; set; }
}