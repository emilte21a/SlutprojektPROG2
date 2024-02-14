public class Player : Entity
{
    private Rectangle _rectangle;

    private float _speed = 5;

    public Camera2D camera { get; init; }

    PhysicsBody physicsBody;
    Renderer renderer;
    Collider collider;

    public Vector2 position
    {
        get => new Vector2(_rectangle.X, _rectangle.Y);
        set
        {
            _rectangle.X = value.X;
            _rectangle.Y = value.Y;
        }

    }

    public Player()
    {
        _rectangle = new Rectangle(0, 0, 50, 50);
        components = new List<Component>();
        physicsBody = GetComponent<PhysicsBody>();
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
        renderer.sprite = Raylib.LoadTexture("Bilder/CharacterSprite.png");
    }



    public override void Update()
    {
        MovePlayer();
        System.Console.WriteLine("Players comp: " + components.Count);
    }

    public override void Draw()
    {
        Raylib.DrawTexture(renderer.sprite, (int)_rectangle.X, (int)_rectangle.Y, Color.White);
    }

    private void MovePlayer()
    {
        _rectangle.X += GetAxisX() * _speed;
        _rectangle.Y += GetAxisY() * _speed;
    }

    public static float GetAxisX()
    {
        if (Raylib.IsKeyDown(KeyboardKey.A) && (!Raylib.IsKeyDown(KeyboardKey.W) || !Raylib.IsKeyDown(KeyboardKey.S)))
            return -1;

        else if (Raylib.IsKeyDown(KeyboardKey.D) && (!Raylib.IsKeyDown(KeyboardKey.W) || !Raylib.IsKeyDown(KeyboardKey.S)))
            return 1;

        return 0;
    }
    public static float GetAxisY()
    {
        if (Raylib.IsKeyDown(KeyboardKey.W) && (!Raylib.IsKeyDown(KeyboardKey.A) || !Raylib.IsKeyDown(KeyboardKey.D)))
            return -1;

        else if (Raylib.IsKeyDown(KeyboardKey.S) && (!Raylib.IsKeyDown(KeyboardKey.A) || !Raylib.IsKeyDown(KeyboardKey.D)))
            return 1;

        return 0;
    }
}