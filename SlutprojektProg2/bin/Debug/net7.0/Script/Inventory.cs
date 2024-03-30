

public class Inventory
{
    public List<Item> itemsInInventory;

    private Item[] inventorySlots;

    private Texture2D _inventoryTexture = Raylib.LoadTexture("Images/inventoryspot.png");

    private Texture2D _slotTexture = Raylib.LoadTexture("Images/itemChosen.png");

    public Inventory()
    {
        itemsInInventory = new List<Item>();
        inventorySlots = new Item[5];

        for (int i = 0; i < inventorySlots.Length; i++)
            inventorySlots[i] = null;
    }


    public void Update()
    {
        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            inventorySlots[i] = itemsInInventory[i];
        }
    }

    int itemPos = 0;
    public void Draw()
    {
        Raylib.DrawTexture(_inventoryTexture, 30, 30, Color.White);

        foreach (Item item in inventorySlots)
        {
            if (itemPos < inventorySlots.Length)
            {
                if (item != null && itemsInInventory.Contains(item))
                {
                    Raylib.DrawTexture(item.texture, 50 + 120 * itemPos, 70, Color.White);
                }

                Raylib.DrawTexture(_slotTexture, 40 + CurrentActiveItem() * 120, 60, Color.White);

                if (itemPos >= inventorySlots.Length)
                {
                    itemPos = FindFirstEmptySlot();
                }
            }
            itemPos++;
        }
    }
    public int CurrentActiveItem()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.One))
            return 0;

        else if (Raylib.IsKeyPressed(KeyboardKey.Two))
            return 1;

        else if (Raylib.IsKeyPressed(KeyboardKey.Three))
            return 2;

        else if (Raylib.IsKeyPressed(KeyboardKey.Four))
            return 3;

        else if (Raylib.IsKeyPressed(KeyboardKey.Five))
            return 4;

        return 0;
    }

    public void AddToInventory(Item item)
    {
        if (itemsInInventory.Contains(item) && item.stackable)
            item.stack++;

        itemsInInventory.Add(item);
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