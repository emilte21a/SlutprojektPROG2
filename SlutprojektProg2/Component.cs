public abstract class Component
{

}

public class PhysicsBody : Component
{
    public static void Update()
    {

    }
}

public class Collider : Component
{
    
}

public class Renderer : Component
{
    public Texture2D sprite { get; set; }

}