using UnityEngine;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject InventorySlot;
    [SerializeField] private GameObject InventoryItem;
    [SerializeField] private Transform InventorySlotParent;
    [SerializeField] private Transform InventoryItemParent;
    private Inventory inventory;
    private  List<Item> items;
    private bool active = false;

    public void Start()
    {
        for (int i = 0; i < inventory.space; i++)
        {
            slots[i].index = i;
            items[i].index = i;
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < inventory.; i++)
        {
            if (i < inventory.items.Count)
            {
                inventory.AddItem();
            }
            else
            {
                inventory.RemoveItem(i);
            }
        }
    }

    public void UpdateSlot(int slot)
    {
        inventory.AddItem(items[i]);
        inventory.RemoveItem(items[i].slot);
    }
    
    public void ToggleInventory(List<Item> Inventory)
    {
        active = !active;
        InventoryPanel.SetActive(active);
        if (active)
        {
            items = Inventory;
            UpdateUI();
        }
    }
}
