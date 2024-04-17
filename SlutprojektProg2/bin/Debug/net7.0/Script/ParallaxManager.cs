public class ParallaxLayer
{
    public Texture2D texture;
    public Vector2 position;
    public Rectangle rectangle;
    public float parallaxFactor;

    public Color color;
    public ParallaxLayer(Texture2D layerTexture)
    {
        this.texture = layerTexture;
        rectangle = new Rectangle(0, 0, Game.ScreenWidth * 1.5f, layerTexture.Height);
    }
}

public class ParallaxManager
{
    List<ParallaxLayer> parallaxLayers;
    Texture2D layer1 = Raylib.LoadTexture("Images/mountains.png");
    Texture2D layer2 = Raylib.LoadTexture("Images/parallaxbackground.png");

    public ParallaxManager()
    {
        parallaxLayers = new();
        parallaxLayers.Add(new ParallaxLayer(layer1) { parallaxFactor = 35, color = Color.White });
        parallaxLayers.Add(new ParallaxLayer(layer2) { parallaxFactor = 50, color = Color.White });
    }
    public void Update(Player player)
    {
        foreach (var p in parallaxLayers)
        {
            p.position.X = 0;
            p.rectangle.X = player.position.X * p.parallaxFactor / 500;
            p.position.Y = Raymath.Lerp(p.position.Y, p.parallaxFactor * 10, 0.5f) + 2 * p.parallaxFactor - 200;
        }

    }

    public void Draw()
    {
        parallaxLayers.ForEach(p => Raylib.DrawTextureRec(p.texture, p.rectangle, p.position, p.color));
    }

}