

using System.Globalization;

public class DayNightSystem
{
    public float currentTime;

    public int dayDuration = 24;

    Rectangle skyRectangle = new Rectangle(0, 0, Game.ScreenWidth, Game.ScreenHeight);

    Color backgroundColor = new Color(0, 0, 0, 0);
    Color dayColor = new Color(135, 206, 235, 255); //Sky blue
    Color nightColor = new Color(0, 17, 41, 255); //Midnight blue


    public void Update()
    {
        currentTime += Raylib.GetFrameTime();

        if (currentTime >= dayDuration)
        {
            currentTime = 0;
        }

        float lerpValue = currentTime / dayDuration;

        backgroundColor = LerpColor(dayColor, nightColor, 1);
    }

    public void Draw()
    {
        Raylib.DrawRectangleRec(skyRectangle, backgroundColor);
    }


    public Color LerpColor(Color color1, Color color2, float t)
    {
        t = Math.Clamp(t, 0.0f, 1.0f); // Clamp t between 0 and 1
        int r = 0;
        int g = 0;
        int b = 0;
        if (currentTime <= 7)
        {

            r = (int)Math.Round(Raymath.Lerp(color2.R, color1.R, t));
            g = (int)Math.Round(Raymath.Lerp(color2.G, color1.G, t));
            b = (int)Math.Round(Raymath.Lerp(color2.B, color1.B, t));
        }
        else if (currentTime >= 18)
        {

            r = (int)Math.Round(Raymath.Lerp(color1.R, color2.R, t));
            g = (int)Math.Round(Raymath.Lerp(color1.G, color2.G, t));
            b = (int)Math.Round(Raymath.Lerp(color1.B, color2.B, t));
        }
        else
        {
            r = color1.R;
            g = color1.G;
            b = color1.B;
        }
        return new Color(r, g, b, 255);
    }
}