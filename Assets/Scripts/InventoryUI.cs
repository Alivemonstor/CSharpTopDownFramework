using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;


public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private Inventory inventory; 
    [SerializeField] private List<Item> items;
    [SerializeField] private List<GameObject> slots;
    [SerializeField] private bool active = false;

 
    public void BuildUI()
    {
        int offsetX = 0;
        int offsetY = 0;

        for (int i = 0; i < inventory.GetMaxSpace(); i++)
        {
            if (i % 5 == 0)
            {
                offsetX = 0;
                offsetY += 60;
            }
            else
            {
                offsetX += 60;
            }     
            GameObject slot = new GameObject("Slot");
            slot.transform.SetParent(InventoryPanel.transform);
            slot.AddComponent<RectTransform>();
            slot.AddComponent<CanvasRenderer>();
            slot.AddComponent<UnityEngine.UI.Image>();
            slot.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1.0f);
            slot.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
            slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetX, offsetY);
            slots.Add(slot);
        }


        for (int i = 0; i < items.Count; i++)
        {
            GameObject item = new GameObject("Item");
            item.transform.SetParent(slots[i].transform);
            item.AddComponent<RectTransform>();
            item.AddComponent<CanvasRenderer>();
            item.AddComponent<UnityEngine.UI.Image>();
            item.GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(new Texture2D(64, 64), new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
            item.GetComponent<RectTransform>().sizeDelta = new Vector2(32, 32);
            item.GetComponent<RectTransform>().anchoredPosition = slots[i].GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void DestroyUI()
    {
        for (int i = 0; i < inventory.GetMaxSpace(); i++)
        {
            Debug.Log("Destroying Slot" + i);
            Destroy(slots[i]);
        }    
        slots.Clear();
    }

    public void UpdateInventory()
    {
        //Update inventory
    }

    public void UpdateSlot(int slot)
    {
        // Update slot
    }
    
    public void ToggleInventory(List<Item> Inventory)
    {
        active = !active;
        InventoryPanel.SetActive(active);
        if (active)
        {
            items = Inventory;
            BuildUI();
            return;
        }
        DestroyUI();
    }
}
