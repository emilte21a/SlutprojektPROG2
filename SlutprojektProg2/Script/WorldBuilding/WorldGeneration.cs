using System.Security.Cryptography.X509Certificates;

public class WorldGeneration : IDrawable
{

    private int _worldSize = 300;
    private int _tileSize = 80;

    public static List<Tile> tilesInWorld = new List<Tile>();
    public static List<Tile> backgroundTiles = new List<Tile>();
    public static List<Tile> tilesThatShouldRender = new List<Tile>();
    public static List<Prefab> prefabs = new List<Prefab>();

    private int _seed;
    private int _caveThreshold = 160;
    private float _surfaceThreshold = 0.5f;
    private int _heightMultiplier = 1;

    public static Vector2[] spawnPoints;
    public static string[] tileOccupation;
    public Tile[,] tilemap;

    public WorldGeneration()
    {
        spawnPoints = new Vector2[_worldSize];
        tileOccupation = new string[spawnPoints.Length];
        tilemap = new Tile[_worldSize, _worldSize];
        _seed = Random.Shared.Next(-10000, 10000);
    }

    public void GenerateTiles()
    {
        Image heightImage = Raylib.GenImagePerlinNoise(_worldSize, _worldSize, _seed, _seed, 1f);
        Image noiseImage = Raylib.GenImagePerlinNoise(_worldSize, _worldSize, _seed, _seed, 10f);

        for (int x = 0; x < noiseImage.Width; x++)
        {
            int height = Raylib.GetImageColor(heightImage, (int)(x * _surfaceThreshold), (int)_surfaceThreshold).R * _heightMultiplier;
            for (int y = -height; y < 0; y++)
            {
                SpawnTile(new BackgroundTile(new Vector2(x * _tileSize, y * _tileSize)));

                if (Raylib.GetImageColor(noiseImage, x, y * -1).R < _caveThreshold)
                {
                    if (y == -height)
                    {
                        GrassTile grassTile = new GrassTile(new Vector2(x * _tileSize, y * _tileSize));
                        SpawnTile(grassTile);
                        spawnPoints[x] = new Vector2(x * _tileSize, y * _tileSize);
                        tilemap[x, -y] = grassTile;
                        grassTile.lightLevel = 0;
                    }

                    else if (y == -height + 1)
                    {
                        DirtTile dirtTile = new DirtTile(new Vector2(x * _tileSize, y * _tileSize));
                        SpawnTile(dirtTile);
                        tilemap[x, -y] = dirtTile;
                        dirtTile.lightLevel = -y / 3;
                    }

                    else
                    {
                        StoneTile stoneTile = new StoneTile(new Vector2(x * _tileSize, y * _tileSize));
                        SpawnTile(stoneTile);
                        tilemap[x, -y] = stoneTile;
                        stoneTile.lightLevel = -y;
                    }
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

    public Tile GetTileAt(Vector2 position)
    {
        if (position.X / _tileSize >= 0 && position.X / _tileSize <= tilemap.GetLength(0) && position.Y / _tileSize >= 0 && position.Y / _tileSize <= tilemap.GetLength(1))
        {
            return tilemap[(int)position.X / _tileSize, (int)position.Y / _tileSize];
        }
        return null;
    }

    public void GeneratePrefabs()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            tileOccupation[i] = "Empty";
            if (Random.Shared.Next(0, 10) == 1 && tileOccupation[i] == "Empty")
            {
                SpawnGameObject(new Tree(new Vector2(i * _tileSize - _tileSize, spawnPoints[i].Y)));
                tileOccupation[i] = "Tree";
            }

            else if (Random.Shared.Next(0, 10) == 1 && tileOccupation[i] == "Empty")
            {
                SpawnGameObject(new Rock(new Vector2(i * _tileSize, spawnPoints[i].Y)));
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