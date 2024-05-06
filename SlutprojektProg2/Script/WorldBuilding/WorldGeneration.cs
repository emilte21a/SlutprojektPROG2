using System.Security.Cryptography.X509Certificates;

public class WorldGeneration : IDrawable
{
    private int _worldSize = 350; //Världsstorlek
    private int _tileSize = 80; //Varje tiles storlek

    #region  Listor för alla tiles och prefabs i världen
    public static List<TilePref> gameObjectsInWorld = new List<TilePref>();
    public static List<TilePref> gameObjectsThatShouldRender = new List<TilePref>();

    #endregion

    #region Variabler som bestämmer hur världen ser ut
    private int _seed;
    private int _caveThreshold = 160;
    private float _surfaceThreshold = 0.5f;
    private int _heightMultiplier = 1;

    #endregion

    public static Vector2[] spawnPoints; //Varje möjliga plats att spawna en entity på
    public static string[] tileOccupation; //En array där man kan se ifall en position på ytan ockuperas av en prefab
    public Tile[,] tilemap;

    public WorldGeneration()
    {
        spawnPoints = new Vector2[_worldSize];
        tileOccupation = new string[spawnPoints.Length];
        tilemap = new Tile[_worldSize, _worldSize];
        _seed = Random.Shared.Next(-10000, 10000); //Skapa nytt seed varje gång man startar spelet
    }

    //Metod som procedurellt genererar en ny värld varje gång man startar spelet.
    public void GenerateTiles()
    {
        Image heightImage = Raylib.GenImagePerlinNoise(_worldSize, _worldSize, _seed, _seed, 1f); //En perlinnoise bild som bestämmer hur "len" ytan ska vara. Desto större scale, desto "galnare"
        Image noiseImage = Raylib.GenImagePerlinNoise(_worldSize, _worldSize, _seed, _seed, 10f); //Bestämmer hur grottorna ska se ut.

        for (int x = 0; x < noiseImage.Width; x++)
        {
            int height = Raylib.GetImageColor(heightImage, (int)(x * _surfaceThreshold), (int)_surfaceThreshold).R * _heightMultiplier;
            //Hämta en höjd för varje x värde i världen genom att ta det röda värdet på varje x pixel.

            for (int y = height; y < _worldSize; y++)
            {
                SpawnTilePrefab(new BackgroundTile(new Vector2(x * _tileSize, y * _tileSize))); //Background tile på varje plats i världen

                if (Raylib.GetImageColor(noiseImage, x, y).R < _caveThreshold) //Placera endast ut ett block om det röda värdet på varje pixel på noiseimage är mindre än en threshold
                {
                    if (y == height) //Spawna gräs
                    {
                        GrassTile grassTile = new GrassTile(new Vector2(x * _tileSize, y * _tileSize));
                        SpawnTilePrefab(grassTile);
                        spawnPoints[x] = new Vector2(x * _tileSize, y * _tileSize);
                        tilemap[x, y] = grassTile;
                    }

                    else if (y == height + 1) //Spawna jord under gräs
                    {
                        DirtTile dirtTile = new DirtTile(new Vector2(x * _tileSize, y * _tileSize));
                        SpawnTilePrefab(dirtTile);
                        tilemap[x, y] = dirtTile;
                    }

                    else //Annars spawna sten
                    {
                        StoneTile stoneTile = new StoneTile(new Vector2(x * _tileSize, y * _tileSize));
                        SpawnTilePrefab(stoneTile);
                        tilemap[x, y] = stoneTile;
                    }
                }
                else //Gör varje plats där det inte finns en tile till tom på tilemap. Detta används i lightingsystem
                    tilemap[x, y] = null;
            }
        }

        Raylib.UnloadImage(noiseImage);
        Raylib.UnloadImage(heightImage);
        //Ladda ur båda bilderna från CPU för att frigöra minne
    }
    public void SpawnTilePrefab(TilePref tilePref)
    {
        gameObjectsInWorld.Add(tilePref);
    }

    //Metod som ritar ut varje tile och prefab som finns i världen
    public void Draw()
    {
        gameObjectsThatShouldRender.ForEach(TP => Raylib.DrawTexture(TP.renderer.sprite, (int)TP.position.X, (int)TP.position.Y, Color.White));
    }

    public Tile GetTileAt(Vector2 position)
    {
        if (position.X / _tileSize >= 0 && position.X / _tileSize <= tilemap.GetLength(0) && position.Y / _tileSize >= 0 && position.Y / _tileSize <= tilemap.GetLength(1))
        {
            return tilemap[(int)position.X / _tileSize, (int)position.Y / _tileSize];
        }
        return null;
    }

    //Metod som genererar prefabs
    public void GeneratePrefabs()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            tileOccupation[i] = "Empty";
            if (Random.Shared.Next(0, 10) == 1 && tileOccupation[i] == "Empty" && spawnPoints[i].Y != 0)
            {
                SpawnTilePrefab(new Tree(new Vector2(i * _tileSize - _tileSize, spawnPoints[i].Y)));
                tileOccupation[i] = "Tree";
            }

            else if (Random.Shared.Next(0, 10) == 1 && tileOccupation[i] == "Empty" && spawnPoints[i].Y != 0)
            {
                SpawnTilePrefab(new Rock(new Vector2(i * _tileSize, spawnPoints[i].Y)));
                tileOccupation[i] = "Rock";
            }
        }
    }

    /*
        För varje position som är giltid på världen
            Gör alla till tomma

            10% att spawna ett träd på varje tile om den är tom 
                Spawna ett träd på den positionen och gör att den platsen ockuperas av ett träd

            10% att spawna en sten på varje tile om den är tom 
                spawna en sten på den postitionen och gör att den platsen ockuperas av en sten
    */
}

public class SpawnManager
{
    public static void SpawnEntityAt(Entity entity, Vector2 position)
    {
        entity.position = position;
    }
}