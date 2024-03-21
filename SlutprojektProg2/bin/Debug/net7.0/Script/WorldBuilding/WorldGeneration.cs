public class WorldGeneration : IDrawable
{

    private int worldSize = 200; //Blocks
    private int tileSize = 80;

    public static List<Tile> tilesInWorld = new List<Tile>();

    private List<Tile> tilesThatShouldRender = new List<Tile>();

    Rectangle screenRect = new Rectangle(0, 0, Game.ScreenWidth, Game.ScreenHeight);

    private int seed;
    private int caveThreshold = 160;
    private float surfaceThreshold = 0.5f;
    private int heightMultiplier = 1;

    public static Tile[,] grid;

    public Vector2[] spawnPoints;

    public WorldGeneration()
    {
        spawnPoints = new Vector2[worldSize];
        seed = Random.Shared.Next(-10000, 10000);
        grid = new Tile[worldSize, worldSize];
        Console.WriteLine("Width: " + grid.GetLength(0) + " Height: " + grid.GetLength(1));
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
                        SpawnTile(new Grass(new Vector2(x * tileSize, y * tileSize + tileSize * 120)));

                    else if (y == -height + 1)
                        SpawnTile(new Dirt(new Vector2(x * tileSize, y * tileSize + tileSize * 120)));

                    else
                        SpawnTile(new Stone(new Vector2(x * tileSize, y * tileSize + tileSize * 120)));
                }
            }
            spawnPoints[x] = new Vector2(x * tileSize, -height * tileSize);
        }

        Raylib.UnloadImage(noiseImage);
        Raylib.UnloadImage(heightImage);
    }

    public void SpawnTile(Tile tile)
    {
        tilesInWorld.Add(tile);
        //grid[(int)position.X / tileSize, (int)position.Y * -1 / tileSize] = tile;
    }

    public void Draw()
    {
        tilesInWorld.ForEach(t => Raylib.DrawTexture(t.texture, (int)t.rectangle.X, (int)t.rectangle.Y, Color.White));
    }
}

public class SpawnManager
{
    public static void SpawnEntityAt(Entity entity, Vector2 position)
    {
        entity.position = position;
    }
}