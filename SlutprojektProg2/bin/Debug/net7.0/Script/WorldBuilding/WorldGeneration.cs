public class WorldGeneration : IDrawable
{

    private int worldSize = 600; //Blocks
    private int tileSize = 80;

    public static List<Tile> tilesInWorld = new List<Tile>();
    public static List<Tile> backgroundTiles = new List<Tile>();
    public static List<Tile> tilesThatShouldRender = new List<Tile>();
    public static List<Prefab> prefabs = new List<Prefab>();

    private int seed;
    private int caveThreshold = 160;
    private float surfaceThreshold = 0.5f;
    private int heightMultiplier = 1;

    public static Vector2[] spawnPoints;
    public static string[] tileOccupation;

    public WorldGeneration()
    {
        spawnPoints = new Vector2[worldSize];
        tileOccupation = new string[spawnPoints.Length];
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
                SpawnTile(new BackgroundTile(new Vector2(x * tileSize, y * tileSize)));

                if (Raylib.GetImageColor(noiseImage, x, y * -1).R < caveThreshold)
                {
                    if (y == -height)
                    {
                        SpawnTile(new GrassTile(new Vector2(x * tileSize, y * tileSize)));
                        spawnPoints[x] = new Vector2(x * tileSize, y * tileSize);
                    }

                    else if (y == -height + 1)
                        SpawnTile(new DirtTile(new Vector2(x * tileSize, y * tileSize)));

                    else
                        SpawnTile(new StoneTile(new Vector2(x * tileSize, y * tileSize)));
                }
            }
        }

        Raylib.UnloadImage(noiseImage);
        Raylib.UnloadImage(heightImage);
    }

    public void SpawnTile(Tile tile)
    {
        if (tile.GetType() != typeof(BackgroundTile))
            tilesInWorld.Add(tile);

        else
            backgroundTiles.Add(tile);
    }

    public void SpawnGameObject(Prefab prefab)
    {
        prefabs.Add(prefab);
    }

    public void Draw()
    {
        backgroundTiles.ForEach(bg => Raylib.DrawTexture(bg.texture, (int)bg.position.X, (int)bg.position.Y, Color.White));
        tilesThatShouldRender.ForEach(t => Raylib.DrawTexture(t.texture, (int)t.rectangle.X, (int)t.rectangle.Y, Color.White));
        prefabs.ForEach(p => Raylib.DrawTexture(p.renderer.sprite, (int)p.position.X, (int)p.position.Y, Color.White));
    }

    public void GeneratePrefabs()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            tileOccupation[i] = "Empty";
            if (Random.Shared.Next(0, 10) == 1 && tileOccupation[i] == "Empty")
            {
                SpawnGameObject(new Tree(new Vector2(i * tileSize - tileSize, spawnPoints[i].Y)));
                tileOccupation[i] = "Tree";
            }

            else if (Random.Shared.Next(0, 13) == 1 && tileOccupation[i] == "Empty")
            {
                SpawnGameObject(new Rock(new Vector2(i * tileSize - tileSize, spawnPoints[i].Y)));
                tileOccupation[i] = "Rock";
            }
            
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