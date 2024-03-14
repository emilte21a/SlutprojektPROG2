

public class CollisionSystem : GameSystem
{

    public override void Update()
    {
        foreach (Entity entity in Game.entities)
        {
            PhysicsBody? physicsBody = entity.GetComponent<PhysicsBody>();
            Collider? collider = entity.GetComponent<Collider>();

            if (collider != null && physicsBody != null)
            {

            }
        }
    }

    // List<Collider> DetectCollision(Collider collider, PhysicsBody physicsBody)
    // {

    // }
}