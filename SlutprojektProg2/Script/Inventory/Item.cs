
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
    public ItemType itemType;

    // //Hitta items av samma typ
    // public override bool Equals(object obj)
    // {
    //     if (obj == null || GetType() != obj.GetType())
    //         return false;

    //     Item otherItem = (Item)obj;

    //     return name == otherItem.name && ID == otherItem.ID;
    // }
}

public sealed class StoneItem : Item, IPlaceable
{
    static Texture2D tex;
    public StoneItem()
    {
        name = "Stone";
        ID = 0;
        craftable = false;
        stackable = true;
        usable = false;
        itemType = ItemType.stone;
        if (tex.Id == 0)
            tex = Raylib.LoadTexture("Images/rockTexture.png");

        texture = tex;
    }
    public Tile TileToPlace(Vector2 pos)
    {
        return new StoneTile(pos);
    }
}

public sealed class GrassItem : Item, IPlaceable
{
    static Texture2D tex;
    public GrassItem()
    {
        name = "Grass";
        ID = 0;
        craftable = false;
        stackable = true;
        usable = false;
        itemType = ItemType.grass;
        if (tex.Id == 0)
            tex = Raylib.LoadTexture("Images/grassTexture.png");

        texture = tex;
    }
    public Tile TileToPlace(Vector2 pos)
    {
        return new GrassTile(pos);
    }
}

public sealed class DirtItem : Item, IPlaceable
{
    static Texture2D tex;
    public DirtItem()
    {
        name = "Dirt";
        ID = 1;
        craftable = false;
        stackable = true;
        usable = false;
        itemType = ItemType.dirt;
        if (tex.Id == 0)
            tex = Raylib.LoadTexture("Images/dirtTexture.png");

        texture = tex;
    }
    public Tile TileToPlace(Vector2 pos)
    {
        return new DirtTile(pos);
    }
}

public sealed class WoodItem : Item
{
    static Texture2D tex;
    public WoodItem()
    {
        name = "Wood";
        ID = 2;
        craftable = false;
        stackable = true;
        usable = false;
        itemType = ItemType.wood;

        if (tex.Id == 0)
            tex = Raylib.LoadTexture("Images/woodTexture.png");

        texture = tex;
    }

}

public sealed class StickItem : Item
{
    static Texture2D tex;
    public StickItem()
    {
        name = "Stick";
        ID = 3;
        craftable = true;
        stackable = true;
        usable = false;
        itemType = ItemType.stick;

        if (tex.Id == 0)
            tex = Raylib.LoadTexture("Images/stickTexture.png");
        texture = tex;


        recipe = new() { { new WoodItem(), 1 } };
    }
}

public sealed class WoodPickaxe : Item
{
    static Texture2D tex;
    public WoodPickaxe()
    {
        name = "Wooden Pickaxe";
        ID = 4;
        stackable = false;
        craftable = false;
        usable = true;
        itemDamage = 10;
        itemType = ItemType.woodenPickaxe;

        if (tex.Id == 0)
            tex = Raylib.LoadTexture("Images/woodenPickaxeTexture.png");
        texture = tex;

        recipe = new() { { new StickItem(), 1 }, { new WoodItem(), 2 } };
    }
}
public sealed class StoneAxe : Item
{
    static Texture2D tex;
    public StoneAxe()
    {
        name = "Stone Axe";
        ID = 5;
        stackable = false;
        craftable = false;
        usable = true;
        itemDamage = 15;
        itemType = ItemType.stoneAxe;

        if (tex.Id == 0)
            tex = Raylib.LoadTexture("Images/stoneAxeTexture.png");
        texture = tex;

        recipe = new() { { new StickItem(), 1 }, { new StoneItem(), 2 } };
    }
}
