public class Player : Entity, IDrawable
{
    public Camera2D camera { get; init; }

    //Skapa spelarens egna physicsbody
    public PhysicsBody physicsBody;

    //Skapa en renderer för att rita ut spelaren
    public Renderer renderer;

    //Skapa spelarens collider
    public Collider collider;

    public AudioPlayer audioPlayer;

    private float _playerSpeed = 100;

    public Player()
    {

        components = new List<Component>();
        physicsBody = AddComponent<PhysicsBody>();
        collider = AddComponent<Collider>();
        renderer = AddComponent<Renderer>();
        audioPlayer = AddComponent<AudioPlayer>();
        renderer.sprite = Raylib.LoadTexture("Images/CharacterSprite.png");
        audioPlayer.audioClip = Raylib.LoadSound("");
        rectangle = new Rectangle(0, 0, renderer.sprite.Width, renderer.sprite.Height);
        position = new Vector2(0, 0);
        physicsBody.UseGravity = PhysicsBody.Gravity.enabled;
        rectangle.X = position.X;
        rectangle.Y = position.Y;

        collider.boxCollider = rectangle;
    }



    public override void Update()
    {
        collider.boxCollider.X = position.X;
        collider.boxCollider.Y = position.Y;
        physicsBody.MovePlayer(physicsBody, _playerSpeed);

        if (Raylib.IsKeyPressed(KeyboardKey.Space))
            physicsBody.Jump(physicsBody, 300);

        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            position += new Vector2(0, 200);
        }

        // if (IsInsideGrid((int)position.X / 100, (int)position.Y / 100) || IsInsideSurroundingArea(position))
        // {
        //     WorldGeneration.tilesThatShouldRender.Clear();
        //     AddTilesWithinAreaToRenderList(position);
        // }
        // else
        //     WorldGeneration.tilesThatShouldRender.Clear();
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
        //Raylib.DrawRectangleRec(rectangle, Color.Black);
        Raylib.DrawTextureRec(renderer.sprite, new Rectangle(0, 0, renderer.sprite.Width * InputManager.GetLastDirectionDelta(), renderer.sprite.Height), position, Color.White);
        Raylib.DrawRectangle((int)position.X, (int)position.Y, 10, 10, Color.Orange);
        //Raylib.DrawTexture(sprite, (int)rectangle.X, (int)rectangle.Y, Color.White);

    }
}
