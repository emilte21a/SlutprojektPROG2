
public abstract class Entity
{
    public T GetComponent<T>() where T : Component, new()
    {
        T t = new();
        return t;
    }
    public virtual void Update() { }
    public virtual void Draw() { }

}