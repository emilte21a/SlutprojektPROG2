public abstract class Tile : GameObject
{
    protected Texture2D texture;
    public Vector2 position;
    public Rectangle rectangle;
}

public class Grass : Tile
{
    static Texture2D grassTexture;
    Collider collider;

    public Grass(Vector2 pos)
    {
        rectangle = new Rectangle(0, 0, 100, 100);

        components = new();
        collider = AddComponent<Collider>();

        collider.boxCollider = rectangle;
        position = pos;
        if (grassTexture.Id == 0)
            // grassTexture = Raylib.LoadTexture("Bilder/theo.png");

            rectangle.X = position.X;
        rectangle.Y = position.Y;

        texture = grassTexture;
    }
}