public abstract class GameObject
{
    public string tag = "";

    public Transform transform = new Transform();

    public T AddComponent<T>() where T : Component, new()
    {
        T t = new();
        components.Add(t);
        return t;
    }

    public T? GetComponent<T>() where T : Component
    {
        foreach (var c in components)
        {
            if (c.GetType() == typeof(T))
                return (T)c;
        }
        return null;
    }

    public List<Component> components;
}
