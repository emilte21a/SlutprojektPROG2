public abstract class GameObject
{
    public T AddComponent<T>() where T : IComponent, new()
    {
        T t = new();
        components.Add(t);
        return t;
    }

    public List<IComponent> components;
}
