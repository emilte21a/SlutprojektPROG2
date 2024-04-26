public abstract class Tile : GameObject
{
    public Texture2D texture;
    public Vector2 position { get; set; }
    public Rectangle rectangle;
}

public sealed class GrassTile : Tile
{
    static Texture2D grassTexture;
    public GrassTile(Vector2 pos)
    {
        rectangle = new Rectangle(0, 0, 80, 80);

        this.position = pos;
        if (grassTexture.Id == 0)
            grassTexture = Raylib.LoadTexture("Images/GrassTile.png");

        rectangle.X = position.X;
        rectangle.Y = position.Y;

        texture = grassTexture;
    }
}

public sealed class StoneTile : Tile
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

public sealed class DirtTile : Tile
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

public sealed class BackgroundTile : Tile
{
    static Texture2D backgroundTexture;

    public BackgroundTile(Vector2 pos)
    {
        this.position = pos;
        if (backgroundTexture.Id == 0)
            backgroundTexture = Raylib.LoadTexture("Images/BackgroundTile.png");

        texture = backgroundTexture;
    }
}
