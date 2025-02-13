using UnityEngine;
using System.Collections.Generic;


public struct ShopItem 
{
    [SerializeField] private string name;
    [SerializeField] private int price;
    [SerializeField] private string description;

    public ShopItem(string name, int price, string description)
    {
        this.name = name;
        this.price = price;
        this.description = description;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetPrice(int price)
    {
        this.price = price;
    }

    public void SetDescription(string description)
    {
        this.description = description;
    }

    public string GetName()
    {
        return this.name;
    }

    public int GetPrice()
    {
        return this.price;
    }

    public string GetDescription()
    {
        return this.description;
    }

}



public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItem> ShopItems;

    public List<ShopItem> GetShopItems()
    {
        return this.ShopItems;
    }

    public void SetShopItems(List<ShopItem> shopItems)
    {
        this.ShopItems = shopItems;
    }

    public void AddShopItem(ShopItem shopItem)
    {
        this.ShopItems.Add(shopItem);
    }

    public void RemoveShopItem(ShopItem shopItem)
    {
        this.ShopItems.Remove(shopItem);
    }

    public void RemoveShopItem(int index)
    {
        this.ShopItems.RemoveAt(index);
    }

    public void ClearShopItems()
    {
        this.ShopItems.Clear();
    }

    public void BuyItem(ShopItem shopItem, PlayerData playerData)
    {
        if (playerData.GetCoins() >= shopItem.GetPrice())
        {
            playerData.SetCoins(playerData.GetCoins() - shopItem.GetPrice());
            playerData.GetInventory().Add(new Item(shopItem.GetName(), 1, true, -1));
        }
    }
}
