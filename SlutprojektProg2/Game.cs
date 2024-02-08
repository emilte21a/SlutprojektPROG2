using Raylib_cs;
using System.Numerics;
public class Game
{
    public static int ScreenWidth = 1024;
    public static int ScreenHeight = 1024;

    Player player;

    Camera2D camera;

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
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);
        player.Draw();
        Raylib.EndDrawing();
    }

    Vector2 Lerp(Vector2 startPos, Vector2 targetPos, float time)
    {
        return (startPos + (targetPos - startPos) * time);
    }
}