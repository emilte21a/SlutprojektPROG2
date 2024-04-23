public sealed class Player : Entity, IDrawable
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

    public PlayerAction playerAction;

    private float _playerSpeed = 2.5f;

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
        inventory = new Inventory();
        playerAction = new PlayerAction();
        #endregion

        inventory.AddToInventory(new WoodItem(), 2);
        inventory.AddToInventory(new WoodItem(), 2);
        inventory.AddToInventory(new StoneItem(), 9);
        inventory.AddToInventory(new StickItem(), 4);
        inventory.AddToInventory(new WoodPickaxe(), 1);

        healthPoints = 100;
        tag = "Player";
        playerState = PlayerState.inGame;

        renderer.sprite = Raylib.LoadTexture("Images/CharacterSprite.png");
        audioPlayer.audioClip = Raylib.LoadSound("");

        rectangle = new Rectangle(0, 0, renderer.sprite.Width, renderer.sprite.Height);

        position = new Vector2(0, 0);
        physicsBody.UseGravity = PhysicsBody.Gravity.enabled;
        collider.boxCollider = rectangle;
    }


    public override void Update()
    {
        collider.boxCollider.X = rectangle.X;
        collider.boxCollider.Y = rectangle.Y;

        MovePlayer(physicsBody, _playerSpeed);

        playerAction.origin = position;

        // if (Raylib.IsKeyDown(KeyboardKey.F))
        //     playerAction.OnClick(position, inventory.currentActiveItem);
        

        if (Raylib.IsKeyPressed(KeyboardKey.Space)) //&& physicsBody.airState == AirState.grounded)
            physicsBody.Jump(physicsBody, 7);

        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            position += new Vector2(0, 200);
        }

        if (Raylib.IsKeyPressed(KeyboardKey.H))
            healthPoints = 100;

        inventory.Update();
        playerAction.Update();
    }

    public void MovePlayer(PhysicsBody physicsBody, float speed)
    {
        physicsBody.velocity.X = Raymath.Clamp(InputManager.GetAxisX() * speed, -5f, 5f);
    }

    private int FallDamage()
    {
        return (int)physicsBody.velocity.Y > 10 ? (int)physicsBody.velocity.Y : 0;
    }

    public override void Draw()
    {
        if (physicsBody.velocity.X != 0)
            animator.PlayAnimation(spriteSheet, (int)InputManager.GetLastDirectionDelta(), 4, position);

        else
            Raylib.DrawTextureRec(renderer.sprite, new Rectangle(0, 0, renderer.sprite.Width * InputManager.GetLastDirectionDelta(), renderer.sprite.Height), position, Color.White);

        Raylib.DrawRectangleRec(playerAction.handRectangle, Color.Red);
        //Raylib.DrawTexture(inventory.currentActiveItem.texture, position.X, position.Y, Color.White);
        if (inventory.currentActiveItem.usable)
        {
            Raylib.DrawTexturePro(inventory.currentActiveItem.texture, new Rectangle(0, 0, inventory.currentActiveItem.texture.Width, inventory.currentActiveItem.texture.Height), new Rectangle(40, 40, inventory.currentActiveItem.texture.Width, inventory.currentActiveItem.texture.Height), new Vector2(position.X + rectangle.Width / 2 + lastDirection.X * 5, position.Y + rectangle.Height / 2), playerAction.rotation, Color.White);
         //  Raylib.DrawTexture(inventory.currentActiveItem.texture, (int)position.X, (int)position.Y, Color.White);//, playerAction.handRectangle, position, playerAction.rotation, Color.White);
        }
    }
}

public enum PlayerState
{
    inInventory,
    inGame,
    inMenu
}