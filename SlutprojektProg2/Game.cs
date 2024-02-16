global using Raylib_cs;
global using System.Numerics;
public class Game
{
    public static int ScreenWidth = 1024;
    public static int ScreenHeight = 1024;

    //Klass-instanser
    Player player;
    WorldGeneration worldGeneration;

    Camera2D camera;

    Texture2D texture;

    public Game()
    {
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "game");
        Raylib.SetTargetFPS(60);
        camera = new()
        {
            Target = new Vector2(0, 0),
            Offset = new Vector2(ScreenWidth / 2, ScreenHeight / 2),
            Zoom = 0.1f
        };
        player = new Player() { camera = camera };
        worldGeneration = new WorldGeneration();
        worldGeneration.GenerateTiles();
        //SpawnEntity.SpawnEntityAt(player, new Vector2(2500, worldGeneration.spawnPoints[500].Y));
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
        player.Update();
        camera.Target = Lerp(camera.Target, player.position, 0.1f);
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);
        Raylib.BeginMode2D(camera);
        worldGeneration.Draw();
        player.Draw();
        Raylib.EndMode2D();
        Raylib.DrawText($"{player.position}", 20, 60, 30, Color.Lime);
        Raylib.DrawFPS(20, 20);
        Raylib.EndDrawing();
    }

    Vector2 Lerp(Vector2 startPos, Vector2 targetPos, float time)
    {
        return (startPos + (targetPos - startPos) * time);
    }
}
