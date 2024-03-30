

public class Inventory : IDrawable
{
    private List<Item> itemsInInventory;

    private Item[] inventorySlots;


    public Inventory()
    {
        itemsInInventory = new List<Item>();
        inventorySlots = new Item[5];

        for (int i = 0; i < inventorySlots.Length; i++)
            inventorySlots[i] = null;
    }

    public void Draw()
    {
        foreach (Item item in inventorySlots)
        {
            int itemPos = 0;

            if (itemPos < inventorySlots.Length)
            {
                if (item != null && itemsInInventory.Contains(item))
                {
                    Raylib.DrawTexture(item.texture, 50 + 120 * CurrentActiveItem(), 70, Color.White);
                }

                Raylib.DrawTexture(Raylib.LoadTexture("Images/itemChosen.png"), 40 + itemPos * 120, 60, Color.White);

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