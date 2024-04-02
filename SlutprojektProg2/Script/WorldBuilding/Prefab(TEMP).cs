
public abstract class Prefab : GameObject
{
    public Vector2 position;
    public Rectangle rectangle;

    public Collider collider;
    public Renderer renderer;
}

public class Tree : Prefab
{
    private static Texture2D treeTexture;
    public Tree(Vector2 pos)
    {
        this.position = pos;
        components = new();
        collider = AddComponent<Collider>();
        renderer = AddComponent<Renderer>();

        if (treeTexture.Id == 0)
            treeTexture = Raylib.LoadTexture("Images/tree.png");

        renderer.sprite = treeTexture;
        rectangle = new Rectangle(0, 0, renderer.sprite.Width, renderer.sprite.Height);

        rectangle.X = position.X;
        rectangle.Y = position.Y;
        collider.boxCollider = rectangle;

    }
}