public abstract class GameObject
{
    public T AddComponent<T>() where T : Component, new()
    {
        T t = new();
        components.Add(t);
        return t;
    }

    public List<Component> components;
}
