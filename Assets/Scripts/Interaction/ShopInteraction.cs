using UnityEngine;
using System.Collections.Generic;
public class ShopInteraction : InteractionPoint
{
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private PlayerData PlayerData;

    public override bool CanInteract()
    {
        return true;
    }

    public override void Interact()
    {
        // create shopitem list with all items from players inventory
        List<ShopItem> shopItems = new List<ShopItem>();

        foreach (Item item in PlayerData.GetInventory())
        {
            ShopItem shopItem = new ShopItem(item.GetName(), 10, "This is a shop item", item.GetSlot());
            shopItems.Add(shopItem);
        }

        shopUI.ShowUI(shopItems);
    }

}
