
public abstract class Tile : GameObject
{
    public Texture2D texture;
}

public class Grass : Tile
{
    Rectangle _rectangle;

    static Texture2D grassTexture;

    public Grass()
    {
        _rectangle = new Rectangle(0, 0, 50, 50);
        if (grassTexture.Id == 0)
            grassTexture = Raylib.LoadTexture("");

        texture = grassTexture;
    }


}