using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private Inventory inventory; 
    [SerializeField] private List<Item> items;
    [SerializeField] private List<GameObject> slots;
    [SerializeField] private bool active = false;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private TMPro.TextMeshProUGUI tooltipText;

    public void BuildUI()
    {
        int columns = 5;
        float slotSize = 64f;
        float spacing = 5f;
        int totalSlots = inventory.GetMaxSpace();
    
        int rows = Mathf.CeilToInt((float)totalSlots / columns);
        float totalWidth = (columns * slotSize) + ((columns - 1) * spacing);
        float totalHeight = (rows * slotSize) + ((rows - 1) * spacing);
    
        RectTransform panelRect = InventoryPanel.GetComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(totalWidth, totalHeight);
        panelRect.anchoredPosition = Vector2.zero;
    
        foreach (GameObject slot in slots)
        {
            Destroy(slot);
        }
        slots.Clear();

        for (int i = 0; i < totalSlots; i++)
        {
            int row = i / columns;
            int col = i % columns;
    
            float x = (col * (slotSize + spacing)) - (totalWidth / 2) + (slotSize / 2);
            float y = -(row * (slotSize + spacing)) + (totalHeight / 2) - (slotSize / 2);
    
            GameObject slot = new GameObject("Slot");
            slot.transform.SetParent(InventoryPanel.transform);
            RectTransform rect = slot.AddComponent<RectTransform>();
            slot.AddComponent<CanvasRenderer>();
            UnityEngine.UI.Image image = slot.AddComponent<UnityEngine.UI.Image>();
            image.color = new Color(1, 1, 1, 1.0f);
            rect.sizeDelta = new Vector2(slotSize, slotSize);
            rect.anchoredPosition = new Vector2(x, y);
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 0.5f);
    
            slots.Add(slot);
    
            EventTrigger trigger = slot.AddComponent<EventTrigger>();
            int slotIndex = i;  
            AddEvent(trigger, EventTriggerType.PointerEnter, () => ShowTooltip(slotIndex));
            AddEvent(trigger, EventTriggerType.PointerExit, () => HideTooltip());
        }
    
        for (int i = 0; i < items.Count; i++)
        {
            UpdateSlot(i, items[i]);
        }
    }

    public void AddEvent(EventTrigger trigger, EventTriggerType eventType, UnityAction action)
    {
        if (trigger == null)
        {
            Debug.LogError("EventTrigger is null!");
            return;
        }
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((data) => { action(); });
        trigger.triggers.Add(entry);
    }

    public void ShowTooltip(int slotIndex)
    {
        if (tooltip.activeSelf)
        {
            return;
        }

        if (items == null || items.Count == 0 || slotIndex >= items.Count)
        {
            return;
        }

        tooltip.SetActive(true);
        tooltipText.gameObject.SetActive(true);
        tooltipText.text = items[slotIndex].GetName() +"x"+items[slotIndex].GetAmount();
        Vector3 mousePosition = Input.mousePosition;
        tooltip.transform.position = new Vector3(mousePosition.x + 10, mousePosition.y - 10, mousePosition.z);
        tooltipText.rectTransform.position = new Vector3(tooltip.transform.position.x, tooltip.transform.position.y, tooltip.transform.position.z);

        CanvasGroup canvasGrouptip = tooltip.GetComponent<CanvasGroup>();
        CanvasGroup canvasGrouptext = tooltip.GetComponent<CanvasGroup>();
        if (canvasGrouptip == null || canvasGrouptext == null)
        {
            canvasGrouptip = tooltip.AddComponent<CanvasGroup>();
            canvasGrouptext = tooltipText.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGrouptip.blocksRaycasts = false;
        canvasGrouptext.blocksRaycasts = false;
    }

    public void HideTooltip()
    {
        if (!tooltip.activeSelf)
        {
            return;
        }
        tooltip.SetActive(false);
        tooltipText.gameObject.SetActive(false);

        CanvasGroup canvasGrouptip = tooltip.GetComponent<CanvasGroup>();
        CanvasGroup canvasGrouptext = tooltip.GetComponent<CanvasGroup>();
        if (canvasGrouptip != null || canvasGrouptext != null)
        {
            canvasGrouptip.blocksRaycasts = false;
            canvasGrouptext.blocksRaycasts = false;
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
        HideTooltip();
    }

    public void UpdateInventory()
    {
        foreach (GameObject slot in slots)
        {
            Destroy(slot);
        }
        slots.Clear();

        BuildUI();
    }

    Sprite Load( string imageName, string spriteName)
    {
        Sprite[] all = Resources.LoadAll<Sprite>( imageName);

        foreach( var s in all)
        {
            if (s.name == spriteName)
            {
                return s;
            }
        }
        return null;
    }

    public void UpdateSlot(int slotIndex, Item newItem)
    {
        if (slotIndex < 0 || slotIndex >= slots.Count)
        {
            Debug.Log("Invalid slot index!");
            return;
        }

        GameObject slot = slots[slotIndex];

        foreach (Transform child in slot.transform)
        {
            Destroy(child.gameObject);
        }

        if (!newItem.Equals(default(Item)))
        {
            GameObject item = new GameObject("Item");
            item.transform.SetParent(slot.transform);
            RectTransform rect = item.AddComponent<RectTransform>();
            item.AddComponent<CanvasRenderer>();
            UnityEngine.UI.Image image = item.AddComponent<UnityEngine.UI.Image>();

            image.sprite = Load("fish_all", "fish_all_"+newItem.GetItemIndex());

            rect.sizeDelta = new Vector2(32, 32);
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 0.5f);
            rect.localPosition = Vector3.zero; 
        }
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
