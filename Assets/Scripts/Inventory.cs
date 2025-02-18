using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] private string name;
    [SerializeField] private int amount;
    [SerializeField] private bool canStack;
    [SerializeField] private int slot;
    [SerializeField] private int itemIndex;


    // Constructor

    public Item(string name, int amount, bool canStack, int slot, int itemIndex)
    {
        this.name = name;
        this.amount = amount;
        this.canStack = canStack;
        this.slot = slot;
        this.itemIndex = itemIndex;
    }

    // Setters

    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetAmount(int amount)
    {
        Debug.Log("Setting Amount: " + amount);
        this.amount = amount;
        Debug.Log("Amount Set: " + this.amount);
    }
    public void SetCanStack(bool canStack)
    {
        this.canStack = canStack;
    }

    public void SetSlot(int slot)
    {
        if (slot < 0 || slot > 20)
        {
            Debug.Log("Invalid slot");
            return;
        }
        this.slot = slot;
    }

    public void SetItemIndex(int itemIndex)
    {
        this.itemIndex = itemIndex;
    }

    // Getters

    public string GetName()
    {
        return this.name;
    }
    public int GetAmount()
    {
        return this.amount;
    }
    public bool GetCanStack()
    {
        return this.canStack;
    }

    public int GetSlot()
    {
        return this.slot;
    }

    public int GetItemIndex()
    {
        return this.itemIndex;
    }

}

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryUI UI; 
    [SerializeField] private List<Item> items;
    [SerializeField] private int maxSpace = 20;
    [SerializeField] private PlayerData playerData;

    public int GetMaxSpace()
    {
        return this.maxSpace;
    }

    public List<Item> CreateInventory()
    {
        return new List<Item>();
    }

    public void Start()
    {
        items = playerData.GetInventory();
        if (items.Count == 0)
        {
            Debug.Log("No Inventory Found, Creating");
            items = CreateInventory();
        }
    }

    public void AddTestItem()
    {
        for (int i = 0; i < 7; i++)
        {
            int index = playerData.GetRandomItemIndex();
            string name = playerData.GetNameFromIndex(index);
            Item item = new Item(name, 7, true, -1, index);
            AddItem(item);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UI.ToggleInventory(items);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            playerData.SaveIntoJson(items,  playerData.GetCoins(),  playerData.GetDays(),  playerData.GetFinishedGames());
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            int index = playerData.GetRandomItemIndex();
            string name = playerData.GetNameFromIndex(index);
            Item item = new Item(name, 7, true, -1, index);
            AddItem(item);
        }
    }

    public int FindNextSlot()
    {
        int toSend = -1;
        for (int i = 0; i <= maxSpace; i++)
        {
            Debug.Log("Checking Slot: " + i);
            if (items.Count < i)
            {
            Debug.Log("Slot: " + i + " is empty");
                toSend = i;
                break;
            }
        }
        return toSend;
    }

    public Item HasItem(string name)
    {
        Debug.Log("Checking for item: " + name);
        foreach (Item item in items)
        {
            if (item.GetName() == name && item.GetCanStack() == true)
            {
                Debug.Log("Found Item: " + item.GetName());
                return item;
            }
        }
        return new Item("none", 1, false, -1, -1);
    }

    public Item GetItemBySlot(int slot)
    {
        if (slot < 0 || slot >= items.Count)
        {
            Debug.Log("Invalid slot");
            return new Item("none", 1, false, -1, -1);
        }

        #nullable enable
        Item? item = items[slot];
        return item ?? new Item("none", 1, false, -1, -1);

    }

    public void AddItem(Item item)
    {
        if (items.Count <= maxSpace && item.GetCanStack() == false)
        {
            
            if (item.GetSlot() == -1)
            {
                int slot = FindNextSlot();
                if (slot == -1)
                {
                    Debug.Log("Inventory is full canstack == false");
                    return;
                }
                item.SetSlot(slot);
            }
            items.Add(item);
            Debug.Log("Added Item: " + item.GetName() + " Amount: " + item.GetAmount() + " canStack: " + item.GetCanStack() + " Slot: " + item.GetSlot());
            UI.UpdateSlot(item.GetSlot()-1, item);
            playerData.SetInventory(items);
            return;
        } 
        else if (items.Count <= maxSpace && item.GetCanStack() == true)
        {
            Item itemInInventory = HasItem(item.GetName());
            Debug.Log("Item in Inventory: " + itemInInventory.GetName());
            if (itemInInventory.GetName() == "none")
            {
                if (item.GetSlot() == -1)
                {
                    int slot = FindNextSlot();
                    if (slot == -1)
                    {
                        Debug.Log("Inventory is full canstack == true");
                        return;
                    }
                    item.SetSlot(slot);
                }
                Debug.Log("Added Item: " + item.GetName() + " Amount: " + item.GetAmount() + " canStack: " + item.GetCanStack() + " Slot: " + item.GetSlot());
                items.Add(item);
                playerData.SetInventory(items);
                UI.UpdateSlot(item.GetSlot()-1, item);
                return;
            }

            if (itemInInventory.GetAmount() >= 30)
            {
                itemInInventory.SetCanStack(false);
                items[itemInInventory.GetSlot()-1] = itemInInventory;
                playerData.SetInventory(items);
                Debug.Log("Cant Stack Anymore");
                return;
            }

            if (itemInInventory.GetAmount() + item.GetAmount() > 30)
            {
                int amount = 30 - itemInInventory.GetAmount();
                item.SetAmount(item.GetAmount() - amount);
                itemInInventory.SetAmount(30);
                itemInInventory.SetCanStack(false);
                items[itemInInventory.GetSlot()-1] = itemInInventory;
                playerData.SetInventory(items);
                UI.UpdateSlot(itemInInventory.GetSlot()-1, itemInInventory);
                AddItem(item);
                return;
            }


            Debug.Log("Stacking Item: " + item.GetName() + " Amount: " + item.GetAmount() + " canStack: " + item.GetCanStack() + " Slot: " + item.GetSlot() + " with Item: " + itemInInventory.GetName() + " Amount: " + itemInInventory.GetAmount() + " canStack: " + itemInInventory.GetCanStack() + " Slot: " + itemInInventory.GetSlot());
            itemInInventory.SetAmount(itemInInventory.GetAmount() + item.GetAmount());    
            items[itemInInventory.GetSlot()-1] = itemInInventory;
            playerData.SetInventory(items);
            UI.UpdateSlot(itemInInventory.GetSlot()-1, itemInInventory);
            return;

        } 
        else
        {
            Debug.Log("Inventory is full");
            return;
        }
    }

    public void RemoveItem(int slot)
    {
        Item item = GetItemBySlot(slot);
        if (slot < 0 || slot >= items.Count || item.GetName() == "none")
        {
            Debug.Log("Invalid slot");
            return;
        }   

        if (items[slot].GetAmount() > 1)
        {
            items[slot].SetAmount(items[slot].GetAmount() - 1);
            playerData.SetInventory(items);
            return;
        }

        items.RemoveAt(slot);
        playerData.SetInventory(items);
    }
}
