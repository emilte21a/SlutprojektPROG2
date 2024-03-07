public abstract class Tile : GameObject
{
    protected Texture2D texture;
    public Vector2 position;
    public Rectangle _rectangle;
}

public class Grass : Tile
{
    static Texture2D grassTexture;

    public Grass(Vector2 pos)
    {
        _rectangle = new Rectangle(0, 0, 100, 100);
        position = pos;
        if (grassTexture.Id == 0)
            // grassTexture = Raylib.LoadTexture("Bilder/theo.png");

            _rectangle.X = position.X;
        _rectangle.Y = position.Y;

        texture = grassTexture;
    }
}