using System.Globalization;
using System.Numerics;
using Raylib_cs;

public class Player : Entity
{
    public Rectangle rectangle;

    private float speed = 5;
    
    public Camera2D camera { get; init; }

    public Vector2 position
    {
        get => new Vector2(rectangle.X, rectangle.Y);
        set
        {
            rectangle.X = value.X;
            rectangle.Y = value.Y;
        }
    }



    public Player()
    {
        rectangle = new Rectangle(0, 0, 50, 50);
    }

    public override void Update()
    {
        MovePlayer();
    }

    public override void Draw()
    {
        Raylib.DrawRectangleRec(rectangle, Color.Black);
    }

    private void MovePlayer()
    {
        rectangle.X += GetAxisX() * speed;
        rectangle.Y += GetAxisY() * speed;
    }

    private float GetAxisX()
    {
        if (Raylib.IsKeyDown(KeyboardKey.A) && (!Raylib.IsKeyDown(KeyboardKey.W) || !Raylib.IsKeyDown(KeyboardKey.S)))
            return -1;

        else if (Raylib.IsKeyDown(KeyboardKey.D) && (!Raylib.IsKeyDown(KeyboardKey.W) || !Raylib.IsKeyDown(KeyboardKey.S)))
            return 1;

        return 0;
    }
    private float GetAxisY()
    {
        if (Raylib.IsKeyDown(KeyboardKey.W) && (!Raylib.IsKeyDown(KeyboardKey.A) || !Raylib.IsKeyDown(KeyboardKey.D)))
            return -1;

        else if (Raylib.IsKeyDown(KeyboardKey.S) && (!Raylib.IsKeyDown(KeyboardKey.A) || !Raylib.IsKeyDown(KeyboardKey.D)))
            return 1;

        return 0;
    }
}