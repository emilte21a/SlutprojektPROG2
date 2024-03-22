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

        Raylib.InitAudioDevice();

        InitializeInstances();

        drawables = new List<IDrawable>();
        drawables.Add(worldGeneration);
        drawables.Add(player);
    }

    private void InitializeInstances()
    {
        gameSystems = new List<GameSystem>();
        gameSystems.Add(new PhysicsSystem());
        gameSystems.Add(new CollisionSystem());
        gameSystems.Add(new AudioSystem());

        camera = new()
        {
            Target = new Vector2(0, 0),
            Offset = new Vector2(ScreenWidth / 2, ScreenHeight / 2),
            Zoom = 0.9f
        };

        worldGeneration = new WorldGeneration();
        player = new Player() { camera = camera };

        worldGeneration.GenerateTiles();
        entities = new();
        entities.Add(player);
    }

    public void Run()
    {
        //SpawnManager.SpawnEntityAt(player, new Vector2(WorldGeneration.spawnPoints[2].X, WorldGeneration.spawnPoints[2].Y - player.collider.boxCollider.Height));
        while (!Raylib.WindowShouldClose())
        {
            Update();
            Draw();
        }
        Raylib.CloseWindow();
    }

    private void Update()
    {
        gameSystems.ForEach(gS => gS.Update());
        player.Update();
        camera.Target = Raymath.Vector2Lerp(camera.Target, player.position, 0.1f);
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.SkyBlue);
        Raylib.BeginMode2D(camera);

        drawables.ForEach(e => e.Draw());
        entities.ForEach(e => Raylib.DrawRectangleRec(e.GetComponent<Collider>().boxCollider, new Color(0, 255, 50, 100)));
        Raylib.EndMode2D();

        Raylib.DrawText($"Pos: {player.position}", 20, 60, 30, Color.White);
        Raylib.DrawText($"{player.healthPoints}", ScreenWidth - 100, 60, 30, Color.White);
        Raylib.DrawText($"{InputManager.GetAxisX()}", 20, 90, 30, Color.White);
        Raylib.DrawText($"Vel: {player.physicsBody.velocity}", 20, 120, 30, Color.White);
        Raylib.DrawText($"Accl: {player.physicsBody.acceleration}", 20, 150, 30, Color.White);
        Raylib.DrawText($"{player.physicsBody.airState}", 20, 180, 30, Color.White);
        Raylib.DrawFPS(20, 20);
        Raylib.EndDrawing();
    }
}
