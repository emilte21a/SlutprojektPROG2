

public class CollisionSystem : GameSystem
{
    public override void Update()
    {

        foreach (Entity e in Game.entities)
        {
            #region Hämta potentiella komponenter hos entitites
            PhysicsBody? physicsBody = e.GetComponent<PhysicsBody>();
            Collider? collider = e.GetComponent<Collider>();
            #endregion

            List<Tile> tilesCloseToEntity; //Lista med alla rektanglar som screenRectangle kolliderar med
            List<Tile> tilesCollidingWithFeet; //Lista med alla rektanglar som endast fötterna hos entityn kolliderar med

            #region Rektanglar som kollar spelarens kollisioner
            Rectangle ScreenRectangle = new Rectangle(e.position.X - Game.ScreenWidth / 2 - 100, e.position.Y - Game.ScreenHeight / 2 - 100, Game.ScreenWidth + 200, Game.ScreenWidth + 200);
            Rectangle floorCollider = new Rectangle(e.position.X, e.position.Y + collider.boxCollider.Height, collider.boxCollider.Width, 2);
            #endregion

            if (collider != null && physicsBody != null) //Kolla så att entityns physicsbody och collider inte är null
            {
                if (e.tag == "Player")
                    WorldGeneration.tilesThatShouldRender = WorldGeneration.tilesInWorld.Where(tile => Raylib.CheckCollisionRecs(ScreenRectangle, tile.rectangle)).ToList();

                tilesCloseToEntity = WorldGeneration.tilesInWorld.Where(tile => Raylib.CheckCollisionRecs(ScreenRectangle, tile.rectangle)).ToList();
                tilesCollidingWithFeet = WorldGeneration.tilesInWorld.Where(tile => Raylib.CheckCollisionRecs(floorCollider, tile.rectangle)).ToList();

                physicsBody.airState = tilesCollidingWithFeet.Count > 0 ? AirState.grounded : AirState.inAir;

                e.lastDirection.X = physicsBody.velocity.X > 0 ? 1 : -1;
                e.lastDirection.Y = physicsBody.velocity.Y > 0 ? 1 : -1;

                #region Korrigera y positionen 
                foreach (Tile tile in tilesCollidingWithFeet)
                {
                    //Räkna ut spelarens y-position i nästa frame med en rektangel
                    Rectangle nextBounds = new Rectangle(e.position.X, e.position.Y + physicsBody.velocity.Y, collider.boxCollider.Width, collider.boxCollider.Height);
                    if (LargestCollisionRectangle(tilesCollidingWithFeet, nextBounds).Height < LargestCollisionRectangle(tilesCollidingWithFeet, nextBounds).Width) //Spelaren är över tilen
                    {
                        if (physicsBody.velocity.Y > 0 || e.lastDirection.Y == 1)
                        {
                            //Korrigera Y hastigheten ifall spelaren faller
                            e.position = new Vector2(e.position.X, tile.rectangle.Y - collider.boxCollider.Height);
                            physicsBody.velocity.Y = 0;
                        }
                        else if (physicsBody.velocity.Y < 0 || e.lastDirection.Y == -1)
                        {
                            //Korrigera Y hastigheten ifall spelaren hoppar
                            e.position = new Vector2(e.position.X, tile.rectangle.Y + tile.rectangle.Height);
                            physicsBody.velocity.Y = 0;
                        }
                    }
                }
                #endregion

                #region Korrigera x positionen
                foreach (Tile tile in tilesCloseToEntity)
                {
                    //Räkna ut spelarens x-position i nästa frame med en rektangel
                    Rectangle nextBounds = new Rectangle(e.position.X + physicsBody.velocity.X, e.position.Y, collider.boxCollider.Width, collider.boxCollider.Height);
                    if (Raylib.CheckCollisionRecs(tile.rectangle, nextBounds)) //Kolla kollisioner mellan alla tiles som är inom ett visst område av spelaren och spelarens rektangel i nästa frame
                    {
                        if (physicsBody.velocity.X > 0 || e.lastDirection.X == 1)
                        {
                            //Korrigera spelarens hastighet åt höger när den kolliderar
                            e.position = new Vector2(tile.rectangle.X - collider.boxCollider.Width, e.position.Y);
                            physicsBody.velocity.X = 0;
                        }

                        else if (physicsBody.velocity.X < 0 || e.lastDirection.X == -1)
                        {
                            //Korrigera spelarens hastighet åt vänster när den kolliderar
                            e.position = new Vector2(tile.rectangle.X + tile.rectangle.Width, e.position.Y);
                            physicsBody.velocity.X = 0;
                        }
                    }
                }
                #endregion
            }
        }
    }

    //Metod som returnerar den största rektangeln ur listan med de rektanglar som en entity kolliderar med 
    private Rectangle LargestCollisionRectangle(List<Tile> tiles, Rectangle entityRect)
    {
        List<Tile> order = tiles.OrderByDescending(t => t.rectangle.Width).ThenBy(t => t.rectangle.Height).ToList();

        return Raylib.GetCollisionRec(entityRect, order[0].rectangle);
    }


}