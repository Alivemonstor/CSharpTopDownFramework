using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private List<ShopItem> shopItems;

    public void ShowUI(List<ShopItem> shopItems)
    {
        this.shopItems = shopItems;
    }
}
