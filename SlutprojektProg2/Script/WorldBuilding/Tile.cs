public abstract class Tile : GameObject
{
    public Texture2D texture;
    public Vector2 position { get; set; }
    public Rectangle rectangle;
}

public class GrassTile : Tile
{
    static Texture2D grassTexture;
    Collider collider;

    public GrassTile(Vector2 pos)
    {
        rectangle = new Rectangle(0, 0, 80, 80);

        // components = new();
        // collider = AddComponent<Collider>();

        // collider.boxCollider = rectangle;
        this.position = pos;
        if (grassTexture.Id == 0)
            grassTexture = Raylib.LoadTexture("Images/GrassTile.png");

        rectangle.X = position.X;
        rectangle.Y = position.Y;

        texture = grassTexture;
    }
}

public class StoneTile : Tile
{
    static Texture2D stoneTexture;

    public StoneTile(Vector2 pos)
    {
        rectangle = new Rectangle(0, 0, 80, 80);
        this.position = pos;
        if (stoneTexture.Id == 0)
        {
            stoneTexture = Raylib.LoadTexture("Images/StoneTile.png");
        }
        rectangle.X = position.X;
        rectangle.Y = position.Y;

        texture = stoneTexture;
    }
}

public class DirtTile : Tile
{
    static Texture2D dirtTexture;

    public DirtTile(Vector2 pos)
    {
        rectangle = new Rectangle(0, 0, 80, 80);
        this.position = pos;
        if (dirtTexture.Id == 0)
        {
            dirtTexture = Raylib.LoadTexture("Images/DirtTile.png");
        }
        rectangle.X = position.X;
        rectangle.Y = position.Y;

        texture = dirtTexture;
    }
}