
public abstract class Prefab : GameObject
{
    public Vector2 position;
    public Rectangle rectangle;
    public Collider collider;
    public Renderer renderer;
    protected static List<Texture2D> textures;
    public Item dropType;
}

public sealed class Tree : Prefab
{
    private static Texture2D oakTexture;
    private static Texture2D birchTexture;
    public Tree(Vector2 pos)
    {
        textures = new();
        textures.Add(oakTexture);
        textures.Add(birchTexture);
        this.position = pos;
        components = new();
        collider = AddComponent<Collider>();
        renderer = AddComponent<Renderer>();

        if (oakTexture.Id == 0)
            oakTexture = Raylib.LoadTexture("Images/oakTree.png");

        if (birchTexture.Id == 0)
            birchTexture = Raylib.LoadTexture("Images/birchTree.png");

        renderer.sprite = textures[Random.Shared.Next(0, textures.Count)];
        rectangle = new Rectangle(0, 0, renderer.sprite.Width, renderer.sprite.Height);

        position.Y -= renderer.sprite.Height;
        rectangle.X = position.X;
        rectangle.Y = position.Y;
        collider.boxCollider = rectangle;

        dropType = new WoodItem();
    }
}

public sealed class Rock : Prefab
{
    private static Texture2D rockTexture;
    public Rock(Vector2 pos)
    {
        this.position = pos;
        components = new();
        collider = AddComponent<Collider>();
        renderer = AddComponent<Renderer>();

        if (rockTexture.Id == 0)
            rockTexture = Raylib.LoadTexture("Images/rock.png");

        renderer.sprite = rockTexture;
        rectangle = new Rectangle(0, 0, renderer.sprite.Width, renderer.sprite.Height);

        position.Y -= renderer.sprite.Height;
        rectangle.X = position.X;
        rectangle.Y = position.Y;
        collider.boxCollider = rectangle;
        dropType = new StoneItem();
    }
}