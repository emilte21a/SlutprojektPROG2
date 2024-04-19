
public abstract class Item : GameObject
{
    public string name;
    public int ID;
    public bool craftable;
    public bool stackable;
    public Texture2D texture;
    public int itemDamage;
    public bool usable;
    public Dictionary<Item, int> recipe;

    //Hitta items av samma typ
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Item otherItem = (Item)obj;

        return name == otherItem.name && ID == otherItem.ID;
    }
}

public sealed class StoneItem : Item
{
    static Texture2D stoneTexture;
    public StoneItem()
    {
        name = "Stone";
        ID = 0;
        craftable = false;
        stackable = true;
        usable = false;
        if (stoneTexture.Id == 0)
            stoneTexture = Raylib.LoadTexture("Images/rockTexture.png");

        texture = stoneTexture;
    }
}

public sealed class WoodItem : Item
{
    static Texture2D woodTexture;
    public WoodItem()
    {
        name = "Wood";
        ID = 1;
        craftable = false;
        stackable = true;
        usable = false;

        if (woodTexture.Id == 0)
            woodTexture = Raylib.LoadTexture("Images/woodTexture.png");

        texture = woodTexture;
    }
}

public sealed class StickItem : Item
{
    static Texture2D stickTexture;
    public StickItem()
    {
        name = "Stick";
        ID = 2;
        craftable = true;
        stackable = true;
        usable = false;

        if (stickTexture.Id == 0)
            stickTexture = Raylib.LoadTexture("Images/stickTexture.png");
        texture = stickTexture;


        recipe = new() { { new WoodItem(), 1 } };
    }
}

public sealed class WoodPickaxe : Item
{
    static Texture2D woodPickaxeTexture;
    public WoodPickaxe()
    {
        name = "Wooden Pickaxe";
        ID = 3;
        stackable = true;
        craftable = false;
        usable = false;
        itemDamage = 10;

        if (woodPickaxeTexture.Id == 0)
            woodPickaxeTexture = Raylib.LoadTexture("Images/woodenPickaxeTexture.png");
        texture = woodPickaxeTexture;

        recipe = new() { { new StickItem(), 1 }, { new WoodItem(), 2 } };
    }
}