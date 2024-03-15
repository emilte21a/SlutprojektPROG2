public class PhysicsSystem : GameSystem
{

    private float terminalVelocity = 150;
    public override void Update()
    {
        foreach (Entity e in Game.entities)
        {

            if (e is IGravityAffected gravityAffected && gravityAffected.PhysicsBody != null && gravityAffected.PhysicsBody.UseGravity == PhysicsBody.Gravity.enabled)
            {
                PhysicsBody? physicsBody = gravityAffected.PhysicsBody;

                //Lägg till acceleration
                physicsBody.acceleration += physicsBody.gravity * Raylib.GetFrameTime();

                //Updatera velociteten
                physicsBody.velocity += physicsBody.acceleration;//* Raylib.GetFrameTime();

                //Clampa maxhastigheten 
                Raymath.Clamp(physicsBody.velocity.Y, -terminalVelocity, terminalVelocity);

                //Uppdatera positionen
                e.position += physicsBody.velocity;//* Raylib.GetFrameTime() * 20000;

                //Återställ accelerationen
                physicsBody.acceleration = Vector2.Zero;

                // if (physicsBody.velocity.Y != 0)
                //     physicsBody.airState = AirState.inAir;

                // else
                //     physicsBody.airState = AirState.notInAir;
            }
        }
    }
}