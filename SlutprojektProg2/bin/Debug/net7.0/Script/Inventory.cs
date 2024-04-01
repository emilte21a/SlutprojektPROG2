

public class Inventory
{
    public List<Item> itemsInInventory;

    private Item[] inventorySlots;

    private Texture2D _hotbarTexture = Raylib.LoadTexture("Images/Hotbar.png");

    private Texture2D _itemChosenTexture = Raylib.LoadTexture("Images/itemChosen.png");

    public Inventory()
    {
        itemsInInventory = new List<Item>();
        inventorySlots = new Item[5];

        for (int i = 0; i < inventorySlots.Length; i++)
            inventorySlots[i] = null;
    }

    public void Update()
    {

    }
    int itemPos = 0;

    public void Draw()
    {
        Raylib.DrawTexture(_hotbarTexture, Game.ScreenWidth / 2 - _hotbarTexture.Width / 2, Game.ScreenHeight - 100, Color.White);

        foreach (Item item in inventorySlots)
        {
            if (itemPos < inventorySlots.Length)
            {
                if (item != null && itemsInInventory.Contains(item))
                {
                    Raylib.DrawTexture(item.texture, 50 + 120 * itemPos, 70, Color.White);
                }

                Raylib.DrawTexture(_itemChosenTexture, Game.ScreenWidth / 2 - _hotbarTexture.Width / 2 + 11 + CurrentActiveItem() * 58, Game.ScreenHeight - 89, Color.White);
            }
            itemPos++;
        }
        if (itemPos >= inventorySlots.Length)
        {
            itemPos = FindFirstEmptySlot();
        }
    }

    int activeitem;
    public int CurrentActiveItem()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.One))
            activeitem = 0;

        else if (Raylib.IsKeyPressed(KeyboardKey.Two))
            activeitem = 1;

        else if (Raylib.IsKeyPressed(KeyboardKey.Three))
            activeitem = 2;

        else if (Raylib.IsKeyPressed(KeyboardKey.Four))
            activeitem = 3;

        else if (Raylib.IsKeyPressed(KeyboardKey.Five))
            activeitem = 4;

        return activeitem;
    }

    public void AddToInventory(Item item)
    {
        if (itemsInInventory.Contains(item) && item.stackable)
            item.stack++;

        itemsInInventory.Add(item);
        if (itemsInInventory.Count <= 5)
        {
            for(int i= 0; i < itemsInInventory.Count; i++)
            {
                inventorySlots[i] = item;
            }
        }
    }
    public void RemoveFromInventory(Item item)
    {
        if (item.GetType() == typeof(Item) && itemsInInventory.Contains(item))
            itemsInInventory.Remove(item);
    }

    public int FindFirstEmptySlot()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null)
            {
                return i;
            }
            continue;
        }
        return 5;
    }

    public bool CanCraft(Item item)
    {
        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            foreach (KeyValuePair<Item, int> ingredient in item.recipe)
            {
                if (!itemsInInventory.Contains(ingredient.Key) || itemsInInventory[i].stack < ingredient.Value)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void CraftItem(Item item)
    {
        if (CanCraft(item))
        {
            for (int i = 0; i < itemsInInventory.Count; i++)
            {
                foreach (KeyValuePair<Item, int> ingredient in item.recipe)
                {
                    itemsInInventory[i].stack -= ingredient.Value;
                    if (itemsInInventory[i].stack <= 0 && inventorySlots[i] != null)
                    {
                        inventorySlots[i] = null;
                        itemsInInventory.Remove(ingredient.Key);
                    }

                }
            }
            AddToInventory(item);
        }
    }
}