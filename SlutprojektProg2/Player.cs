public class Player : Entity, IDrawable
{
    private float _speed = 1000;

    public Camera2D camera { get; init; }

    PhysicsBody physicsBody;
    Renderer renderer;
    Collider collider;

    public Player()
    {
        _rectangle = new Rectangle(0, 0, 50, 50);
        components = new List<IComponent>();
        physicsBody = AddComponent<PhysicsBody>();
        collider = AddComponent<Collider>();
        renderer = AddComponent<Renderer>();
        renderer.sprite = Raylib.LoadTexture("Bilder/CharacterSprite.png");

        physicsBody.gravity = PhysicsBody.Gravity.enabled;
        _rectangle.X = position.X;
        _rectangle.Y = position.Y;
    }



    public override void Update()
    {
        MovePlayer();
        physicsBody.Update(ref _rectangle);
        collider.Update(ref _rectangle);
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
            physicsBody.Jump();

        if (IsInsideGrid())
        {
            System.Console.WriteLine("pos: " + WorldGeneration.grid[(int)position.X / 100, (int)position.Y - WorldGeneration.grid.GetLength(1) / 100]);
        }
    }

    bool IsInsideGrid()
    {
        if (position.X / 100 < 0 || position.X / 100 > WorldGeneration.grid.GetLength(0) || position.Y - WorldGeneration.grid.GetLength(1) / 100 < 0 || position.Y - WorldGeneration.grid.GetLength(1) / 100 > WorldGeneration.grid.GetLength(1))
            return false;

        return true;
    }

    public override void Draw()
    {
        //Raylib.DrawTexture(renderer.sprite, (int)_rectangle.X, (int)_rectangle.Y, Color.White);
        Raylib.DrawRectangleRec(_rectangle, Color.Black);
    }

    private void MovePlayer()
    {
        _rectangle.X += InputManager.GetAxisX() * _speed * Raylib.GetFrameTime();
        //_rectangle.Y += GetAxisY() * _speed;
    }
}
