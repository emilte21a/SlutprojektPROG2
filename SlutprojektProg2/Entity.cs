

public abstract class Entity : GameObject
{
    protected Rectangle _rectangle;
    public Vector2 position
    {
        get => new Vector2(_rectangle.X, _rectangle.Y);
        set
        {
            _rectangle.X = value.X;
            _rectangle.Y = value.Y;
        }

    }
    public virtual void Update() { }
    public virtual void Draw() { }

}
