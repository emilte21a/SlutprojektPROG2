using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Raylib_cs;


public partial class Component
{
    public AirState airState;
}

public class PhysicsBody : Component
{

    private float acceleration = 9.81f;
    public Vector2 gravVelocity = new Vector2(0, 0);
    private float terminalVelocity = 15;

    private float gr;

    public enum Gravity
    {
        enabled,
        disabled
    }

    public Gravity gravity;

    public void Update(ref Rectangle rectangle)
    {
        gr = 0.005f * acceleration * 1000 * Raylib.GetFrameTime();
        if (airState == AirState.notInAir)
            gravVelocity.Y = 0;

        else
            gravVelocity.Y += gr;

        gravVelocity.Y = Math.Clamp(gravVelocity.Y, -terminalVelocity, terminalVelocity);
        rectangle.Y += gravVelocity.Y;

        System.Console.WriteLine(gravVelocity.Y);
    }

    public void Jump()
    {
        gravVelocity.Y -= terminalVelocity * 2;
    }
}

public enum AirState
{
    inAir,
    notInAir
}

public class Collider : Component
{
    List<Tile> collidingTiles;

    public void Update(ref Rectangle rectangle)
    {
        var rect = rectangle;
        collidingTiles = WorldGeneration.tilesInWorld.Where(tile => Raylib.CheckCollisionRecs(rect, tile._rectangle)).ToList();

        foreach (Tile tile in collidingTiles)
        {
            var collisionRec = Raylib.GetCollisionRec(rectangle, tile._rectangle);

            if ((int)collisionRec.Height < (int)collisionRec.Width && (int)rectangle.Y + rectangle.Height > tile._rectangle.Y)
                rectangle.Y -= (int)collisionRec.Height;

            else if ((int)collisionRec.Height < (int)collisionRec.Width && (int)rectangle.Y < tile._rectangle.Y + tile._rectangle.Height)
                rectangle.Y += (int)collisionRec.Height;

            if ((int)collisionRec.Width < (int)collisionRec.Height && (int)rectangle.X < tile._rectangle.X + tile._rectangle.Width && InputManager.GetAxisX() == -1)
                rectangle.X += (int)collisionRec.Width;

            else if ((int)collisionRec.Width < (int)collisionRec.Height && (int)rectangle.X + rectangle.Width > tile._rectangle.X && InputManager.GetAxisX() == 1)
                rectangle.X -= (int)collisionRec.Width;

            if (Raylib.CheckCollisionRecs(rectangle, tile._rectangle) && (int)rectangle.Y + rectangle.Height < tile._rectangle.Y)
            {
                airState = AirState.notInAir;
            }
            else
                airState = AirState.inAir;

        }
    }
}

public class Renderer : Component
{
    public Texture2D sprite { get; set; }


}