using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Item
{
    [SerializeField] private string name;
    [SerializeField] private int amount;
    [SerializeField] private bool canStack;
    [SerializeField] private int slot;

    // Constructor

    public Item(string name, int amount, bool canStack, int slot)
    {
        this.name = name;
        this.amount = amount;
        this.canStack = canStack;
        this.slot = slot;
    }

    // Setters

    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetAmount(int amount)
    {
        this.amount = amount;
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

}

public class Inventory : MonoBehaviour
{

    public List<Item> items;
    public int maxSpace = 20;
    [SerializeField] private PlayerData playerData;

    public List<Item> CreateInventory()
    {
        return new List<Item>();
    }

    public void AddTestItems()
    {
        AddItem(new Item("Sword", 1, false, FindNextSlot()));
        AddItem(new Item("Shield", 1, false, FindNextSlot()));
        AddItem(new Item("Potion", 5, true, FindNextSlot()));
    }

    public void Start()
    {
        items = playerData.GetInventory();
        if (items.Count == 0)
        {
            Debug.Log("No Inventory Found, Creating");
            items = CreateInventory();
        }
        Debug.Log("AddingItems");

        AddTestItems();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Inventory: ");
            foreach (Item item in items)
            {
                Debug.Log(item.GetName() + " " + item.GetAmount());
            }
        }
    }

    public int FindNextSlot()
    {
        int toSend = -1;
        for (int i = 0; i < maxSpace; i++)
        {

            if (items.Count < i)
            {
                toSend = i;
            }
        }
        return toSend;
    }

    public Item HasItem(string name)
    {
        Debug.Log("Checking for item: " + name);
        #nullable enable
        foreach (Item item in items)
        {
            if (item.GetName() == name)
            {
                return item;
            }
        }
        return new Item("none", 1, false, -1);
    }

    public Item GetItemBySlot(int slot)
    {
        if (slot < 0 || slot >= items.Count)
        {
            Debug.Log("Invalid slot");
            return new Item("none", 1, false, -1);
        }

        #nullable enable
        Item? item = items[slot];
        return item ?? new Item("none", 1, false, -1);

    }

    public void AddItem(Item item)
    {
        Debug.Log("Adding Item: " + item.GetName() + " Amount: " + item.GetAmount() + " canStack: " + item.GetCanStack() + " Slot: " + item.GetSlot());
        if (items.Count <= maxSpace && item.GetCanStack() == false)
        {
            int slot = FindNextSlot();
            if (slot == -1)
            {
                Debug.Log("Inventory is full canstack == false");
                return;
            }
            item.SetSlot(slot);
            items.Add(item);
            playerData.SetInventory(items);
            return;
        } 
        else if (items.Count <= maxSpace && item.GetCanStack() == true)
        {
            Item itemInInventory = HasItem(item.GetName());
            Debug.Log("Item in Inventory: " + itemInInventory.GetName());
            if (itemInInventory.GetName() == "none")
            {
                int slot = FindNextSlot();
                if (slot == -1)
                {
                    Debug.Log("Inventory is full canstack == true");
                    return;
                }
                item.SetSlot(slot);
                items.Add(item);
                playerData.SetInventory(items);
                return;
            }

            if (itemInInventory.GetAmount() > 30)
            {
                Debug.Log("Cant Stack Anymore");
                return;
            }

            itemInInventory.SetAmount(item.GetAmount() + 1);    
            playerData.SetInventory(items);
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
            items[slot].SetAmount(items[slot].GetAmount() + 1);
            playerData.SetInventory(items);
            return;
        }

        items.RemoveAt(slot);
        playerData.SetInventory(items);
    }
}
