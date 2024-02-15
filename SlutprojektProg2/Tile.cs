public abstract class Tile : GameObject
{
    public Texture2D texture;
    public Vector2 position;
    protected Rectangle _rectangle;
}

public class Grass : Tile
{
   


    static Texture2D grassTexture;

    public Grass()
    {
        _rectangle = new Rectangle(0, 0, 50, 50);
        if (grassTexture.Id == 0)
            grassTexture = Raylib.LoadTexture("Bilder/theo.png");

        position.X = _rectangle.X;
        position.Y = _rectangle.Y;

        texture = grassTexture;
    }
}