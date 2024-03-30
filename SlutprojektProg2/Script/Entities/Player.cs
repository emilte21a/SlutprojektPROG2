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

    public Animator animator;

    public Inventory inventory;

    private float _playerSpeed = 150;

    public int healthPoints;

    private Texture2D spriteSheet = Raylib.LoadTexture("Images/SpriteSheet.png");

    public static PlayerState playerState;

    public Player()
    {
        #region Lägg till spelarens komponenter
        components = new List<Component>();
        physicsBody = AddComponent<PhysicsBody>();
        collider = AddComponent<Collider>();
        renderer = AddComponent<Renderer>();
        audioPlayer = AddComponent<AudioPlayer>();
        animator = AddComponent<Animator>();
        #endregion

        healthPoints = 100;
        tag = "Player";
        playerState = PlayerState.inGame;

        renderer.sprite = Raylib.LoadTexture("Images/CharacterSprite.png");
        audioPlayer.audioClip = Raylib.LoadSound("");

        rectangle = new Rectangle(0, 0, renderer.sprite.Width, renderer.sprite.Height);

        position = new Vector2(0, 0);
        System.Console.WriteLine(WorldGeneration.spawnPoints[100]);
        physicsBody.UseGravity = PhysicsBody.Gravity.enabled;
        rectangle.X = position.X;
        rectangle.Y = position.Y;
        collider.boxCollider = rectangle;
    }


    public override void Update()
    {
        collider.boxCollider.X = rectangle.X;
        collider.boxCollider.Y = rectangle.Y;

        MovePlayer(physicsBody, _playerSpeed);

        if (Raylib.IsKeyPressed(KeyboardKey.Space))//&& physicsBody.airState == AirState.grounded)
            physicsBody.Jump(physicsBody, 300);

        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            position += new Vector2(0, 200);
        }
    }

    public void MovePlayer(PhysicsBody physicsBody, float speed)
    {
        physicsBody.velocity.X = Raymath.Clamp(InputManager.GetAxisX() * speed * Raylib.GetFrameTime(), -2f, 2f);
    }

    private int FallDamage()
    {
        return (int)physicsBody.velocity.Y > 6 ? (int)physicsBody.velocity.Y : 0;
    }

    public override void Draw()
    {
        if (physicsBody.velocity.X != 0)
            animator.PlayAnimation(spriteSheet, (int)InputManager.GetLastDirectionDelta(), 4, position);

        else
            Raylib.DrawTextureRec(renderer.sprite, new Rectangle(0, 0, renderer.sprite.Width * InputManager.GetLastDirectionDelta(), renderer.sprite.Height), position, Color.White);

        Raylib.DrawRectangle((int)position.X, (int)position.Y, 10, 10, Color.Orange);
        Raylib.DrawRectangleRec(new Rectangle(position.X, position.Y + renderer.sprite.Height, renderer.sprite.Width, 2), Color.DarkBlue);
    }
}

public enum PlayerState
{
    inInventory,
    inGame,
    inMenu
}