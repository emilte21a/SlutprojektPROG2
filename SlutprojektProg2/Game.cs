global using Raylib_cs;
global using System.Numerics;
global using System.Collections.Generic;
public class Game
{
    public static int ScreenWidth = 1024;
    public static int ScreenHeight = 1024;

    //Klass-instanser
    Player player;
    WorldGeneration worldGeneration;

    Camera2D camera;

    List<IDrawable> drawables;

    public static List<Entity> entities;

    List<GameSystem> gameSystems;

    public Game()
    {
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "game");
        Raylib.SetTargetFPS(60);

        InitializeInstances();

        drawables = new List<IDrawable>();
        drawables.Add(worldGeneration);
        drawables.Add(player);



        SpawnManager.SpawnEntityAt(player, worldGeneration.spawnPoints[20]);
    }

    private void InitializeInstances()
    {
        gameSystems = new List<GameSystem>();
        gameSystems.Add(new PhysicsSystem());
        gameSystems.Add(new CollisionSystem());

        camera = new()
        {
            Target = new Vector2(0, 0),
            Offset = new Vector2(ScreenWidth / 2, ScreenHeight / 2),
            Zoom = 0.5f
        };
        player = new Player() { camera = camera };
        worldGeneration = new WorldGeneration();
        worldGeneration.GenerateTiles();
        entities = new();
        entities.Add(player);
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            Update();
            Draw();
        }
        Raylib.CloseWindow();
    }

    private void Update()
    {
        gameSystems.ForEach(system => system.Update());
        player.Update();
        camera.Target = Raymath.Vector2Lerp(camera.Target, player.position, 0.1f);
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.SkyBlue);
        Raylib.BeginMode2D(camera);

        drawables.ForEach(e => e.Draw());

        Raylib.EndMode2D();
        
        Raylib.DrawText($"{player.position}", 20, 60, 30, Color.White);
        Raylib.DrawFPS(20, 20);
        Raylib.EndDrawing();
    }
}
