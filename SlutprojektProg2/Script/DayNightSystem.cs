

using System.Globalization;

public class DayNightSystem
{
    public float currentTime;

    public int dayDuration = 24;

    Rectangle skyRectangle = new Rectangle(0, 0, Game.ScreenWidth, Game.ScreenHeight);

    Color backgroundColor = new Color(0, 0, 0, 0);
    Color dayColor = new Color(135, 206, 235, 255);
    Color nightColor = new Color(0, 17, 41, 255);

    Texture2D sunSprite = Raylib.LoadTexture("Images/sunSprite.png");

    private float rotation = 0f;


    int a;

    public DayNightSystem()
    {
        backgroundColor = nightColor;
    }

    public void Update()
    {
        currentTime += Raylib.GetFrameTime() / 10; //Varje "timme" är 10 sekunder

        if (currentTime >= dayDuration)
        {
            currentTime = 0;
        }

        rotation = currentTime * 15 - 90;

        nightColor.A = (byte)Raymath.Clamp((float)Calculate(currentTime), 0, 255);
        a = (int)Raymath.Clamp((float)Calculate(currentTime), 0, 180);
    }

    public void Draw()
    {
        Raylib.DrawRectangle(0, 0, Game.ScreenWidth, Game.ScreenHeight, dayColor);
        Raylib.DrawRectangleRec(skyRectangle, nightColor);
        DrawCircle();
    }

    private void DrawCircle()
    {
        Raylib.DrawTexturePro(sunSprite,
            new Rectangle(0, 0, sunSprite.Width, sunSprite.Height), // Source rectangle (whole texture)
            new Rectangle(Game.ScreenWidth / 2, Game.ScreenHeight / 2, sunSprite.Width, sunSprite.Height), // Destination rectangle
            new Vector2(600, 250 / 2), // Origin (center of texture)
            rotation,
            Color.White);
    }

    public static double Calculate(float x)
    {
        // if (x <= 12)
        // {
        // Approach 0 as x approaches 12
        return -180 * Math.Cos((x - 12) * Math.PI / 12);
        // }
        // else
        // {
        //     // Return to 100 as x approaches 24
        //     return 100 * Math.Cos((x - 24) * Math.PI / 12);
        // }
    }

    public void DrawR()
    {
        Raylib.DrawRectangle(0, 0, Game.ScreenWidth, Game.ScreenHeight, new Color(nightColor.R, nightColor.G, nightColor.B, a));
    }
}