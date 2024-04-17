
public abstract class Item : GameObject
{
    public string name;
    public int ID;
    public bool craftable;
    public bool stackable;
    public Texture2D texture;
    public Dictionary<Item, int> recipe;

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false; // Return false if obj is null or not an Item instance
        }

        Item otherItem = (Item)obj; // Cast obj to Item type

        // Compare items based on specific properties (e.g., Name and Id)
        return name == otherItem.name && ID == otherItem.ID;
    }
}

public class StoneItem : Item
{
    static Texture2D stoneTexture;
    public StoneItem()
    {
        name = "Stone";
        ID = 0;
        craftable = false;
        stackable = true;
        if (stoneTexture.Id == 0)
            stoneTexture = Raylib.LoadTexture("Images/rockTexture.png");

        texture = stoneTexture;
    }
}

public class WoodItem : Item
{
    static Texture2D woodTexture;
    public WoodItem()
    {
        name = "Wood";
        ID = 1;
        craftable = false;
        stackable = true;

        if (woodTexture.Id == 0)
            woodTexture = Raylib.LoadTexture("Images/woodTexture.png");

        texture = woodTexture;
    }
}

public class StickItem : Item
{
    static Texture2D stickTexture;
    public StickItem()
    {
        name = "Stick";
        ID = 2;
        craftable = true;
        stackable = true;

        if (stickTexture.Id == 0)
            stickTexture = Raylib.LoadTexture("Images/stickTexture.png");
        texture = stickTexture;


        recipe = new() { { new WoodItem(), 1 } };
    }
}

public class WoodPickaxe : Item
{
    static Texture2D woodPickaxeTexture;
    public WoodPickaxe()
    {
        name = "Wooden Pickaxe";
        ID = 3;
        stackable = true;
        craftable = false;

        if (woodPickaxeTexture.Id == 0)
            woodPickaxeTexture = Raylib.LoadTexture("Images/woodenPickaxeTexture.png");
        texture = woodPickaxeTexture;

        recipe = new() { { new StickItem(), 1 }, { new WoodItem(), 2 } };
    }
}