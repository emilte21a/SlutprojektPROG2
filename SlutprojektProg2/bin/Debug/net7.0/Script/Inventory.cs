

public class Inventory
{
    public List<Item> itemsInInventory;

    private Slot[] inventorySlots;

    private Texture2D _hotbarTexture = Raylib.LoadTexture("Images/Hotbar.png");

    private Texture2D _itemChosenTexture = Raylib.LoadTexture("Images/itemChosen.png");

    public Inventory()
    {
        itemsInInventory = new List<Item>();
        inventorySlots = new Slot[5];
    }

    public void Update()
    {

    }
    int itemPos = 0;
    public void Draw()
    {
        Raylib.DrawTexture(_hotbarTexture, Game.ScreenWidth / 2 - _hotbarTexture.Width / 2, Game.ScreenHeight - 100, Color.White);

        foreach (Slot slot in inventorySlots)
        {
            if (itemPos < inventorySlots.Length)
            {
                Raylib.DrawTexture(_itemChosenTexture, Game.ScreenWidth / 2 - _hotbarTexture.Width / 2 + 11 + CurrentActiveItem() * 58, Game.ScreenHeight - 89, Color.White);
                if (slot.item != null && itemsInInventory.Contains(slot.item))
                {
                    Raylib.DrawTexture(slot.item.texture, Game.ScreenWidth / 2 - _hotbarTexture.Width / 2 + 11 + 58 * slot.index, Game.ScreenHeight - 89, Color.White);
                    Raylib.DrawText($"{slot.item.stack}", Game.ScreenWidth / 2 - _hotbarTexture.Width / 2 + 54 + 58 * slot.index, Game.ScreenHeight - 64, 10, Color.White);
                }

                itemPos++;
            }
            if (itemPos >= inventorySlots.Length)
            {
                itemPos = FindFirstEmptySlot();
            }
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
        if (itemsInInventory.Contains(item) && item.stackable && item.GetType() == typeof(Item))
            item.stack++;

        itemsInInventory.Add(item);
        if (itemsInInventory.Count <= 5)
        {
            inventorySlots[FindFirstEmptySlot()] = new Slot(item, FindFirstEmptySlot());
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
            if (inventorySlots[i].item == null)
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
                    if (itemsInInventory[i].stack <= 0 && inventorySlots[i].item != null)
                    {
                        inventorySlots[i].item = null;
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