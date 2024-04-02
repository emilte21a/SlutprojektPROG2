public class ParallaxLayer
{
    public Texture2D texture;
    public Vector2 position;
    public Rectangle rectangle;
    public float parallaxFactor;
    public ParallaxLayer(Texture2D layerTexture)
    {
        this.position = position;
        this.texture = layerTexture;
        rectangle = new Rectangle(0, 0, layerTexture.Width * 5, layerTexture.Height);
    }
}

public class ParallaxManager : IDrawable
{
    List<ParallaxLayer> parallaxLayers;
    Texture2D layer1 = Raylib.LoadTexture("Images/mountains.png");
    Texture2D layer2 = Raylib.LoadTexture("Images/parallaxbackgound.png");

    public ParallaxManager()
    {
        parallaxLayers = new();
        parallaxLayers.Add(new ParallaxLayer(layer1) { parallaxFactor = 5 });
        parallaxLayers.Add(new ParallaxLayer(layer2) { parallaxFactor = 10 });
    }
    public void Update(Vector2 position)
    {
        foreach (var p in parallaxLayers)
        {
            p.position.X = position.X - InputManager.GetAxisX() * p.parallaxFactor;
            p.position.Y = position.Y;
        }

    }

    public void Draw()
    {
        parallaxLayers.ForEach(p => Raylib.DrawTextureRec(p.texture, p.rectangle, p.position, Color.White));
    }

}