using System.Numerics;
using Raylib_cs;


public abstract class Component
{

}

public class PhysicsBody : Component
{

    private float acceleration = 9.81f; //9.81 m/s^2
    private Vector2 gravVelocity = new Vector2(0, 0);
    float gr = 0;
    private float terminalVelocity = 15;

    public enum Gravity
    {
        enabled,
        disabled
    }

    public Gravity gravity;

    public void Update(Rectangle rectangle)
    {
        if (gravity == Gravity.enabled)
        {
            gr += 0.01f * (float)Raylib.GetTime() * acceleration;
            gravVelocity.Y = Math.Clamp(gr, -terminalVelocity, terminalVelocity);

            rectangle.Y = gravVelocity.Y;

        }
        System.Console.WriteLine(gr);
    }

}

public class Collider : Component
{

}

public class Renderer : Component
{
    public Texture2D sprite { get; set; }

}