using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Item
{
    public string name;
    public int amount;
    public bool canStack;

    public Item(string name, int amount, bool canStack)
    {
        this.name = name;
        this.amount = amount;
        this.canStack = canStack;
    }

}

public class Inventory : MonoBehaviour
{

    public List<Item> items;
    public int maxSpace = 20;

    public List<Item> CreateInventory()
    {
        return new List<Item>();
    }

    public void Start()
    {
        PlayerData playerData = new PlayerData();
        items = playerData.GetInventory();
        if (items == null)
        {
            items = CreateInventory();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Inventory: ");
            foreach (Item item in items)
            {
                Debug.Log(item.name + " " + item.amount);
            }
        }
    }


    public void AddItem(Item item)
    {
        if (items.Count <= maxSpace)
        {
            items.Add(item);
        } else
        {
            Debug.Log("Inventory is full");
        }
    }

    public void RemoveItem(int slot)
    {
        if (slot < 0 || slot >= items.Count)
        {
            Debug.Log("Invalid slot");
            return;
        }   

        items.RemoveAt(slot);
    }
}
