

public class CollisionSystem : GameSystem
{
    public override void Update()
    {
        foreach (Entity e in Game.entities)
        {
            PhysicsBody? physicsBody = e.GetComponent<PhysicsBody>();
            Collider? collider = e.GetComponent<Collider>();
            Renderer? renderer = e.GetComponent<Renderer>();
            List<Tile> tilesCollidingWithEntity;
            List<Tile> floorCol;

            Rectangle floorCollider = new Rectangle(e.position.X + 5, e.position.Y + renderer.sprite.Height, renderer.sprite.Width - 10, 2);

            if (collider != null && physicsBody != null)
            {
                tilesCollidingWithEntity = WorldGeneration.tilesInWorld.Where(tile => Raylib.CheckCollisionRecs(collider.boxCollider, tile.rectangle)).ToList();
                floorCol = WorldGeneration.tilesInWorld.Where(tile => Raylib.CheckCollisionRecs(floorCollider, tile.rectangle)).ToList();

                //System.Console.WriteLine(tilesCollidingWithEntity.Count);
                System.Console.WriteLine(floorCol.Count);

                if (floorCol.Count > 0)
                    physicsBody.airState = AirState.grounded;

                else
                    physicsBody.airState = AirState.inAir;

                foreach (Tile tile in tilesCollidingWithEntity)
                {
                    if (LargestCollisionRectangle(tilesCollidingWithEntity, collider.boxCollider).Height < LargestCollisionRectangle(tilesCollidingWithEntity, collider.boxCollider).Width)
                    {
                        if (physicsBody.velocity.Y > 0)
                        {
                            // Adjust Y velocity if falling
                            e.position = new Vector2(e.position.X, tile.rectangle.Y - collider.boxCollider.Height);
                            physicsBody.velocity.Y = 0;
                        }
                        else if (physicsBody.velocity.Y < 0)
                        {
                            // Adjust Y velocity if jumping
                            e.position = new Vector2(e.position.X, tile.rectangle.Y + tile.rectangle.Height);
                            physicsBody.velocity.Y = 0;
                        }
                    }

                    if (LargestCollisionRectangle(tilesCollidingWithEntity, collider.boxCollider).Width < LargestCollisionRectangle(tilesCollidingWithEntity, collider.boxCollider).Height)
                    {
                        if (physicsBody.velocity.X > 0 || InputManager.GetLastDirectionDelta() == 1)
                        {
                            physicsBody.velocity.X = 0;
                            e.position = new Vector2(tile.rectangle.X - collider.boxCollider.Width, e.position.Y);
                        }

                        else if (physicsBody.velocity.X < 0 || InputManager.GetLastDirectionDelta() == -1)
                        {
                            physicsBody.velocity.X = 0;
                            e.position = new Vector2(tile.rectangle.X + tile.rectangle.Width, e.position.Y);
                        }
                    }
                }
            }
        }
    }

    private Rectangle LargestCollisionRectangle(List<Tile> tiles, Rectangle entityRect)
    {
        List<Tile> order = tiles.OrderByDescending(t => t.rectangle.Width).ThenBy(t => t.rectangle.Height).ToList();

        return Raylib.GetCollisionRec(entityRect, order[0].rectangle);
    }
}