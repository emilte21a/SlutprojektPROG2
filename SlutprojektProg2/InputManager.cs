public class InputManager
{
    public static float GetAxisX()
    {
        if (Raylib.IsKeyDown(KeyboardKey.A) && (!Raylib.IsKeyDown(KeyboardKey.W) || !Raylib.IsKeyDown(KeyboardKey.S)))
            return -1;

        else if (Raylib.IsKeyDown(KeyboardKey.D) && (!Raylib.IsKeyDown(KeyboardKey.W) || !Raylib.IsKeyDown(KeyboardKey.S)))
            return 1;

        return 0;
    }
    public static float GetAxisY()
    {
        if (Raylib.IsKeyDown(KeyboardKey.W) && (!Raylib.IsKeyDown(KeyboardKey.A) || !Raylib.IsKeyDown(KeyboardKey.D)))
            return -1;

        else if (Raylib.IsKeyDown(KeyboardKey.S) && (!Raylib.IsKeyDown(KeyboardKey.A) || !Raylib.IsKeyDown(KeyboardKey.D)))
            return 1;

        return 0;
    }

    private static int _directionDelta = 1;
    private static string _directionName = "Down";
    public static float GetLastDirectionDelta()
    {

        if (Raylib.IsKeyDown(KeyboardKey.A))
            _directionDelta = -1;

        else if (Raylib.IsKeyDown(KeyboardKey.D))
            _directionDelta = 1;

        return _directionDelta;
    }

}

