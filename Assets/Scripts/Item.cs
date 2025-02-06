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