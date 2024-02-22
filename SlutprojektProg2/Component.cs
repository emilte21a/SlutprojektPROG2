using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Raylib_cs;


public interface IComponent
{

}

public class PhysicsBody : IComponent
{

    private float acceleration = 9.81f; //9.81 m/s^2
    private Vector2 gravVelocity = new Vector2(0, 0);
    private float terminalVelocity = 50;

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
        gravVelocity.Y += gr;
        gravVelocity.Y = Math.Clamp(gravVelocity.Y, -terminalVelocity, terminalVelocity);
        rectangle.Y += gravVelocity.Y;
    }

    public void Jump()
    {
        gravVelocity.Y -= terminalVelocity;
    }
}

public class Collider : IComponent
{
    List<Tile> collidingTiles;
    public void Update(ref Rectangle rectangle)
    {
        // var rect = rectangle;
        // collidingTiles = WorldGeneration.tilesInWorld.Where(tile => Raylib.CheckCollisionRecs(rect, tile._rectangle)).ToList();

        // foreach (Tile tile in collidingTiles)
        // {
        //     var collisionRec = Raylib.GetCollisionRec(rectangle, tile._rectangle);
        //     if ((int)collisionRec.Height < (int)collisionRec.Width && rectangle.Y > tile._rectangle.Y + tile._rectangle.Height)
        //         rectangle.Y += (int)collisionRec.Height;

        //     else if ((int)collisionRec.Height < (int)collisionRec.Width && rectangle.Y + rectangle.Height < tile._rectangle.Y)
        //         rectangle.Y -= (int)collisionRec.Height;

        //     else if ((int)collisionRec.Width < (int)collisionRec.Height && rectangle.X + rectangle.Width < tile._rectangle.X)
        //         rectangle.X -= (int)collisionRec.Width;

        //     else if ((int)collisionRec.Width < (int)collisionRec.Height && rectangle.X > tile._rectangle.X + tile._rectangle.Width)
        //         rectangle.X += (int)collisionRec.Width;
            
        // }
        // System.Console.WriteLine(collidingTiles.Count);
        //System.Console.WriteLine(WorldGeneration.tilesInWorld.Count);
    }
}

public class Renderer : IComponent
{
    public Texture2D sprite { get; set; }

}