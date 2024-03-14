public class PhysicsSystem : GameSystem
{

    private float _speed = 500;

    public override void Update()
    {
        foreach (Entity e in Game.entities)
        {
            PhysicsBody? physicsBody = new PhysicsBody();

            if (physicsBody != null || physicsBody.UseGravity == PhysicsBody.Gravity.enabled)
            {
                physicsBody.acceleration += physicsBody.gravity * Raylib.GetFrameTime() * 60;
                physicsBody.velocity += physicsBody.acceleration * Raylib.GetFrameTime();
                e.position += physicsBody.velocity * Raylib.GetFrameTime();
                physicsBody.acceleration = Vector2.Zero;

                if (physicsBody.velocity.Y != 0)
                    physicsBody.airState = AirState.inAir;

                else
                    physicsBody.airState = AirState.notInAir;

                MovePlayer(physicsBody);
            }
        }
    }

    private void MovePlayer(PhysicsBody physicsBody)
    {
        physicsBody.velocity.X += InputManager.GetAxisX() * _speed * Raylib.GetFrameTime();
    }
}