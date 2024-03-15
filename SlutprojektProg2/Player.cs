public class Player : Entity, IDrawable, IGravityAffected
{
    public Camera2D camera { get; init; }

    //Skapa spelarens egna physicsbody
    public PhysicsBody physicsBody;

    //Referera att spelarens physicsbody är påverkad av physicssystem
    PhysicsBody IGravityAffected.PhysicsBody => physicsBody;

    //Skapa en renderer för att rita ut spelaren
    public Renderer renderer;

    //Skapa spelarens collider
    public Collider collider;

    public Player()
    {
        rectangle = new Rectangle(0, 0, 50, 50);
        position = new Vector2(0, 0);

        components = new List<Component>();
        physicsBody = AddComponent<PhysicsBody>();
        collider = AddComponent<Collider>();
        renderer = AddComponent<Renderer>();
        renderer.sprite = Raylib.LoadTexture("Bilder/CharacterSprite.png");

        physicsBody.UseGravity = PhysicsBody.Gravity.enabled;
        rectangle.X = position.X;
        rectangle.Y = position.Y;

        collider.boxCollider = rectangle;
    }



    public override void Update()
    {
        MovePlayer(physicsBody);

        if (Raylib.IsKeyPressed(KeyboardKey.Space))
            physicsBody.Jump(physicsBody, 30);

        // if (IsInsideGrid((int)position.X / 100, (int)position.Y / 100) || IsInsideSurroundingArea(position))
        // {
        //     WorldGeneration.tilesThatShouldRender.Clear();
        //     AddTilesWithinAreaToRenderList(position);
        // }
        // else
        //     WorldGeneration.tilesThatShouldRender.Clear();
    }

    public void MovePlayer(PhysicsBody physicsBody)
    {
        physicsBody.velocity.X += InputManager.GetAxisX() * 10 * Raylib.GetFrameTime();
        System.Console.WriteLine("acceleration: " + physicsBody.acceleration + " Velocity: " + physicsBody.velocity);
    }

    // // Check if a specific position (x, y) is inside the grid
    // bool IsInsideGrid(int x, int y)
    // {
    //     return x >= 0 && x < WorldGeneration.grid.GetLength(0) &&
    //            -y >= 0 && -y < WorldGeneration.grid.GetLength(1);
    // }

    // // Check if the surrounding 5x5 area around the player's position is inside the grid
    // bool IsInsideSurroundingArea(Vector2 position)
    // {
    //     int startX = (int)position.X / 100 - 500;
    //     int endX = (int)position.X / 100 + 500;
    //     int startY = (int)position.Y / 100 - 500;
    //     int endY = (int)position.Y / 100 + 500;

    //     for (int x = startX; x <= endX; x++)
    //     {
    //         for (int y = startY; y <= endY; y++)
    //         {
    //             if (!IsInsideGrid(x, y))
    //             {
    //                 System.Console.WriteLine(position / 100);
    //                 return false;
    //             }
    //         }
    //     }
    //     System.Console.WriteLine("INSIDE");
    //     return true;
    // }

    // void AddTilesWithinAreaToRenderList(Vector2 position)
    // {
    //     int startX = (int)position.X / 100 - 5;
    //     int endX = (int)position.X / 100 + 5;
    //     int startY = (int)position.Y / 100 - 5;
    //     int endY = (int)position.Y / 100 + 5;

    //     for (int x = startX; x <= endX; x++)
    //     {
    //         for (int y = startY; y <= endY; y++)
    //         {
    //             if (IsInsideGrid(x, y))
    //             {
    //                 WorldGeneration.tilesThatShouldRender.Add(WorldGeneration.grid[x, -y]);
    //             }

    //         }
    //     }
    // }

    public override void Draw()
    {
        Raylib.DrawRectangleRec(rectangle, Color.Black);
        // Raylib.DrawTextureRec(renderer.sprite, new Rectangle(0, 0, renderer.sprite.Width * InputManager.GetLastDirection(), renderer.sprite.Height), position, Color.White);
        //Raylib.DrawTexture(sprite, (int)rectangle.X, (int)rectangle.Y, Color.White);

    }
}
