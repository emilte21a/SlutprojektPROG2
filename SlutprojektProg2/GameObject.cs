
public abstract class GameObject
{
    public T GetComponent<T>() where T : Component, new()
    {
        T t = new();
        components.Add(t);
        return new T();
    }

    public List<Component> components;
}