public class LightingSystem
{
    private int[,] lightMap;
    private Image _lightMapImage;
    public Texture2D lightMapTexture;

    private void InitializeLightmap(Tile[,] tileMap)
    {
        int width = tileMap.GetLength(0);
        int height = tileMap.GetLength(1);

        lightMap = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tileMap[x, y] == null && y < height - 1)
                {
                    lightMap[x, y] = 15;
                    lightMap[x, y + 1] = 15;
                }

                else
                {
                    if (y > 0 && y < height - 1)//&& x > 0 && x < width - 1)
                    {
                        //float newLightLevel = (lightMap[x, y - 1] + lightMap[x, y + 1] + lightMap[x - 1, y] + lightMap[x + 1, y]) / 3;
                        lightMap[x, y] = (int)Raymath.Clamp(lightMap[x, y - 1] - 1, 5, 15);
                    }
                }
            }
        }
    }

    private Image CreateLightMapImage()
    {
        int width = lightMap.GetLength(0);
        int height = lightMap.GetLength(1);

        Image img = Raylib.GenImageColor(width, height, Color.Blank);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color pixelColor = new Color(0, 0, 0, 255 - lightMap[x, y] * 17);
                Raylib.ImageDrawPixel(ref img, x, y, pixelColor);
            }
        }
        return img;
    }

    public void InstantiateLightMap(Tile[,] tileMap)
    {
        InitializeLightmap(tileMap);
        _lightMapImage = CreateLightMapImage();
        Raylib.ImageBlurGaussian(ref _lightMapImage, 5);
        lightMapTexture = Raylib.LoadTextureFromImage(_lightMapImage);
        Raylib.UnloadImage(_lightMapImage);
    }
}

public struct Light
{
    Vector2 position;
    int spread;
    Color color;
}