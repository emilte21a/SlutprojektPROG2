public class WorldGeneration : IDrawable
{

    private int worldSize = 200; //Blocks
    private int tileSize = 100;

    public List<Tile> tilesInWorld = new List<Tile>();

    private int seed;
    private int caveThreshold = 160;
    private float surfaceThreshold = 0.5f;
    private int heightMultiplier = 1;

    public static int[,] grid;

    public Vector2[] spawnPoints;

    public WorldGeneration()
    {
        spawnPoints = new Vector2[worldSize];
        seed = Random.Shared.Next(-10000, 10000);
        grid = new int[worldSize, worldSize];
        System.Console.WriteLine("Width: " + grid.GetLength(0) + " Height: " + grid.GetLength(1));
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
                    SpawnTile(new Grass(), new Vector2(x * tileSize, y * tileSize));
            }
            spawnPoints[x] = new Vector2(x, -height);
        }

        Raylib.UnloadImage(noiseImage);
        Raylib.UnloadImage(heightImage);
    }

    public void SpawnTile(Tile tile, Vector2 position)
    {
        tile.position = position;
        tilesInWorld.Add(tile);
        grid[(int)position.X / 100, (int)position.Y * -1 / 100] = 1;
        
    }

    public void Draw()
    {
        foreach (Tile tile in tilesInWorld)
        {
            Raylib.DrawRectangle((int)tile.position.X, (int)tile.position.Y, tileSize, tileSize, Color.Red);
            //   RaylibDrawTexture(tile.texture, (int)tile.position.X, (int)tile.position.Y, Color.White);
        }
    }
}

public class SpawnManager
{
    public static void SpawnEntityAt(Entity entity, Vector2 position)
    {
        entity.position = position;
    }
}