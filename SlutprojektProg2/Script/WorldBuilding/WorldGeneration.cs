public class WorldGeneration : IDrawable
{

    private int worldSize = 200; //Blocks
    private int tileSize = 80;

    public static List<Tile> tilesInWorld = new List<Tile>();
    public static List<Tile> tilesThatShouldRender = new List<Tile>();

    private int seed;
    private int caveThreshold = 160;
    private float surfaceThreshold = 0.5f;
    private int heightMultiplier = 1;

    public static Vector2[] spawnPoints;

    public WorldGeneration()
    {
        spawnPoints = new Vector2[worldSize];
        seed = Random.Shared.Next(-10000, 10000);
    }

    public void GenerateTiles()
    {
        Image heightImage = Raylib.GenImagePerlinNoise(worldSize, worldSize, seed, seed, 1f);
        Image noiseImage = Raylib.GenImagePerlinNoise(worldSize, worldSize, seed, seed, 10f);

        for (int x = 0; x < noiseImage.Width; x++)
        {
            int height = Raylib.GetImageColor(heightImage, (int)(x * surfaceThreshold), (int)surfaceThreshold).R * heightMultiplier;
            for (int y = -height; y < 0; y++)
            {
                if (Raylib.GetImageColor(noiseImage, x, y * -1).R < caveThreshold)
                {
                    if (y == -height)
                        SpawnTile(new GrassTile(new Vector2(x * tileSize, y * tileSize + tileSize * 120)));

                    else if (y == -height + 1)
                        SpawnTile(new DirtTile(new Vector2(x * tileSize, y * tileSize + tileSize * 120)));

                    else
                        SpawnTile(new StoneTile(new Vector2(x * tileSize, y * tileSize + tileSize * 120)));

                    if (y == -height)
                        spawnPoints[x] = new Vector2(x * tileSize, y * tileSize);
                }
            }
        }

        Raylib.UnloadImage(noiseImage);
        Raylib.UnloadImage(heightImage);
    }

    public void SpawnTile(Tile tile)
    {
        tilesInWorld.Add(tile);
    }

    public void Draw()
    {
        tilesThatShouldRender.ForEach(t => Raylib.DrawTexture(t.texture, (int)t.rectangle.X, (int)t.rectangle.Y, Color.White));
    }
}

public class SpawnManager
{
    public static void SpawnEntityAt(Entity entity, Vector2 position)
    {
        entity.position = position;
    }
}