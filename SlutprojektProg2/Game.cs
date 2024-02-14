global using Raylib_cs;
global using System.Numerics;
public class Game
{
    public static int ScreenWidth = 1024;
    public static int ScreenHeight = 1024;

    Player player;

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
            Zoom = 0.8f
        };
        player = new Player() { camera = camera };
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
        Raylib.ClearBackground(Color.Black);
        Raylib.BeginMode2D(camera);
        player.Draw();
        Raylib.EndMode2D();
        Raylib.DrawText($"{player.position}", 20, 20, 30, Color.Lime);
        Raylib.EndDrawing();
    }

    Vector2 Lerp(Vector2 startPos, Vector2 targetPos, float time)
    {
        return (startPos + (targetPos - startPos) * time);
    }
}