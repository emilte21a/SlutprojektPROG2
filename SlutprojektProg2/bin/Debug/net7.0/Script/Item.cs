
public abstract class Item : GameObject
{
    public string name;
    public int ID;
    public int stack = 1;
    public bool craftable;
    public bool stackable;
    public Texture2D texture;
    public Dictionary<Item, int> recipe;
}

public class StoneItem : Item
{
    public StoneItem()
    {
        name = "Stone";
        ID = 0;
        craftable = false;
        stackable = true;
        texture = Raylib.LoadTexture("Images/rockTexture.png");
    }
}

public class WoodItem : Item
{
    public WoodItem()
    {
        name = "Wood";
        ID = 1;
        craftable = false;
        stackable = true;
        texture = Raylib.LoadTexture("Images/woodTexture.png");
    }
}

public class StickItem : Item
{
    public StickItem()
    {
        name = "Stick";
        ID = 2;
        craftable = true;
        stackable = true;
        texture = Raylib.LoadTexture("Images/stickTexture.png");
        recipe = new() { { new WoodItem(), 1 } };
    }
}

public class WoodPickaxe : Item
{
    public WoodPickaxe()
    {
        name = "Wooden Pickaxe";
        ID = 3;
        stackable = true;
        craftable = false;
        texture = Raylib.LoadTexture("Images/woodenPickaxeTexture.png");
        recipe = new() { { new StickItem(), 1 }, { new WoodItem(), 2 } };
    }
}