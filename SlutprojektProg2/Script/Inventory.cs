

public class Inventory
{
    public Dictionary<Item, int> itemsInInventory;

    private Slot[] inventoryHotbar;
    private Slot[,] inventoryBackpack;

    private Texture2D _hotbarTexture = Raylib.LoadTexture("Images/Hotbar.png");

    private Texture2D _itemChosenTexture = Raylib.LoadTexture("Images/itemChosen.png");

    bool shouldShowInventory = false;

    public Inventory()
    {
        itemsInInventory = new Dictionary<Item, int>();
        inventoryHotbar = new Slot[5];
        inventoryBackpack = new Slot[10, 10];
    }

    public void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Tab) && !shouldShowInventory)
        {
            shouldShowInventory = true;
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Tab) && shouldShowInventory)
        {
            shouldShowInventory = false;
        }


        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            if (i > inventoryHotbar.Length)
            {
                int xIndex = 0;
                int yIndex = 0;

                while (xIndex < inventoryBackpack.GetLength(0))
                {
                    inventoryBackpack[xIndex, yIndex].item = itemsInInventory.Keys.ElementAt(i);

                    xIndex++;

                    if (xIndex >= inventoryBackpack.GetLength(0))
                    {

                        xIndex = 0;
                        yIndex++;

                        if (yIndex >= inventoryBackpack.GetLength(1))
                        {
                            break;
                        }
                    }
                    continue;
                }
            }
        }

    }
    int itemPos = 0;
    public void Draw()
    {
        Raylib.DrawTexture(_hotbarTexture, Game.ScreenWidth / 2 - _hotbarTexture.Width / 2, Game.ScreenHeight - 100, Color.White);

        foreach (Slot slot in inventoryHotbar)
        {
            if (itemPos < inventoryHotbar.Length)
            {
                Raylib.DrawTexture(_itemChosenTexture, Game.ScreenWidth / 2 - _hotbarTexture.Width / 2 + 11 + CurrentActiveItem() * 58, Game.ScreenHeight - 89, Color.White);
                if (slot.item != null && itemsInInventory.ContainsKey(slot.item))
                {
                    Raylib.DrawTexture(slot.item.texture, Game.ScreenWidth / 2 - _hotbarTexture.Width / 2 + 11 + 58 * slot.index, Game.ScreenHeight - 89, Color.White);
                    Raylib.DrawText($"{itemsInInventory[slot.item]}", Game.ScreenWidth / 2 - _hotbarTexture.Width / 2 + 54 + 58 * slot.index, Game.ScreenHeight - 64, 10, Color.White);
                }

                itemPos++;
            }
            if (itemPos >= inventoryHotbar.Length)
            {
                itemPos = FindFirstEmptySlot();
            }
        }
        if (shouldShowInventory)
        {
            Raylib.DrawRectangle(300, 300, 500, 500, Color.Orange);
            for (int x = 0; x < inventoryBackpack.GetLength(0); x++)
            {
                for (int y = 0; y < inventoryBackpack.GetLength(1); y++)
                {
                    if (inventoryBackpack[x, y].item != null)
                    {
                        Raylib.DrawTexture(inventoryBackpack[x, y].item.texture, x * 50 + 300, y * 50 + 300, Color.White);
                    }
                }
            }
        }
    }

    int activeitem;
    public int CurrentActiveItem()
    {
        KeyboardKey keyPressed = (KeyboardKey)Raylib.GetKeyPressed();

        switch (keyPressed)
        {
            case KeyboardKey.One:
                activeitem = 0;
                break;
            case KeyboardKey.Two:
                activeitem = 1;
                break;
            case KeyboardKey.Three:
                activeitem = 2;
                break;
            case KeyboardKey.Four:
                activeitem = 3;
                break;
            case KeyboardKey.Five:
                activeitem = 4;
                break;
        }
        return activeitem;
    }

    public void AddToInventory(Item item)
    {
        if (item.stackable && InventoryContains(item))
        {
            for (int i = 0; i < itemsInInventory.Count; i++)
            {
                if (itemsInInventory.Keys.Equals(item))
                {
                    itemsInInventory[item]++;
                }
            }
        }

        else if (!InventoryContains(item) || !item.stackable)
            itemsInInventory.Add(item, 1);

        if (itemsInInventory.Count <= inventoryHotbar.Length)
            inventoryHotbar[FindFirstEmptySlot()] = new Slot(item, FindFirstEmptySlot());
    }

    public void RemoveFromInventory(Item item)
    {
        if (item.GetType() == typeof(Item) && itemsInInventory.ContainsKey(item))
            itemsInInventory.Remove(item);
    }

    private bool InventoryContains(Item item)
    {
        if (itemsInInventory.Any(existingItem => existingItem.Equals(item)))
            return true;

        return false;
    }

    public int FindFirstEmptySlot()
    {
        for (int i = 0; i < inventoryHotbar.Length; i++)
        {
            if (inventoryHotbar[i].item == null)
            {
                return i;
            }
            continue;
        }
        return -1;
    }

    public bool CanCraft(Item item)
    {
        for (int i = 0; i < itemsInInventory.Count; i++)
        {
            foreach (KeyValuePair<Item, int> ingredient in item.recipe)
            {
                if (!itemsInInventory.ContainsKey(ingredient.Key) || itemsInInventory[item] < ingredient.Value)
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
                    itemsInInventory[item] -= ingredient.Value;
                    if (itemsInInventory[item] <= 0 && inventoryHotbar[i].item != null)
                    {
                        inventoryHotbar[i].item = null;
                        itemsInInventory.Remove(ingredient.Key);
                    }

                }
            }
            AddToInventory(item);
        }
    }
}

public struct Slot
{
    public Vector2 position;
    public int index;
    public Item item;
    public Slot(Item item1, int slotIndex)
    {
        this.item = item1;
        this.index = slotIndex;
    }
}