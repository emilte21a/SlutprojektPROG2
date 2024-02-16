public abstract class GameObject
{
    public T GetComponent<T>() where T : IComponent, new()
    {
        T t = new();
        components.Add(t);
        return t;
    }

    public List<IComponent> components;
}
