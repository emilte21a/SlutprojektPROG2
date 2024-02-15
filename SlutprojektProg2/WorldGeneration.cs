

public class WorldGeneration
{

    private int worldSize = 100;
    private int tileSize = 50;

    public List<Tile> tilesInWorld = new List<Tile>();

    private int seed;
    private int caveThreshold = 125;
    private float surfaceThreshold = 0.5f;
    private int heightMultiplier = 1;
    private int heightAddition = 40;

    public Vector2[] spawnPoints;

    public WorldGeneration()
    {
        spawnPoints = new Vector2[worldSize * 10];
        seed = Random.Shared.Next(-10000, 10000);
    }

    public void GenerateTiles()
    {
        Image heightImage = Raylib.GenImagePerlinNoise(worldSize * 10, worldSize * 10, seed, seed, 1f);
        Image noiseImage = Raylib.GenImagePerlinNoise(worldSize * 10, worldSize * 10, seed, seed, 4f);

        for (int x = 0; x < noiseImage.Width; x++)
        {
            int height = Raylib.GetImageColor(heightImage, (int)(x * surfaceThreshold), (int)surfaceThreshold).R * heightMultiplier + heightAddition;
            for (int y = -height; y < 0; y++)
            {
                if (Raylib.GetImageColor(noiseImage, x, y * -1).R < caveThreshold)
                    SpawnTile(new Grass(), new Vector2(x * tileSize, y * tileSize));
            }
            spawnPoints[x] = new Vector2(x, height);
        }
        Raylib.UnloadImage(noiseImage);
        Raylib.UnloadImage(heightImage);
    }

    private void SpawnTile(Tile tile, Vector2 position)
    {
        tile.position = position;
        tilesInWorld.Add(tile);
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

public static class SpawnEntity
{
    public static void SpawnEntityAt(Entity entity, Vector2 position)
    {
        entity.position = position;
    }
}