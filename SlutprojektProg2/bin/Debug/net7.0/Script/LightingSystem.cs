public class LightingSystem
{
    private int[,] lightMap;
    Image lightMapImage;

    public Texture2D lightMapTexture;

    private void InitializeLightmap(Tile[,] tileMp)
    {
        int width = tileMp.GetLength(0);
        int height = tileMp.GetLength(1);

        lightMap = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tileMp[x, y] == null)
                    lightMap[x, y] = 15;

                else
                {
                    lightMap[x, y] = (int)Raymath.Clamp(lightMap[x, y - 1] - 1, 0, 15);
                }
            }
        }
    }

    private void CreateLightMapImage()
    {
        int width = lightMap.GetLength(0);
        int height = lightMap.GetLength(1);

        lightMapImage = new Image
        {
            Width = width,
            Height = height
        };
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                Raylib.ImageDrawPixel(ref lightMapImage, x, y, new Color(0, 0, 0, lightMap[x, y] * 17));
            }
        }
    }

    public void InstantiateLightMap(Tile[,] tileMp)
    {
        InitializeLightmap(tileMp);
        CreateLightMapImage();

        lightMapTexture = Raylib.LoadTextureFromImage(lightMapImage);

        Raylib.UnloadImage(lightMapImage);
    }
}