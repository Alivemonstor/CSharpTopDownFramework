
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private List<ShopItem> shopItems;
    [SerializeField] private PlayerData PlayerData;

    public void ShowUI(List<ShopItem> shopItems)
    {
        this.gameObject.SetActive(true);
        this.shopItems = shopItems;

        // create background panel that covers the whole screen and has an image
        GameObject backgroundPanel = new GameObject("BackgroundPanel");
        backgroundPanel.transform.SetParent(this.transform);
        RectTransform backgroundPanelRectTransform = backgroundPanel.AddComponent<RectTransform>();
        backgroundPanelRectTransform.anchorMin = new Vector2(0, 0);
        backgroundPanelRectTransform.anchorMax = new Vector2(1, 1);
        backgroundPanelRectTransform.pivot = new Vector2(0.5f, 0.5f);
        backgroundPanelRectTransform.localScale = new Vector3(99, 99, 1);
        backgroundPanelRectTransform.anchoredPosition = Vector2.zero;

        backgroundPanel.AddComponent<CanvasRenderer>();
        Image backgroundPanelImage = backgroundPanel.AddComponent<Image>();
        backgroundPanelImage.color = new Color(0.0f, 0.0f, 0.0f, 0.75f);



        GameObject shopPanel = new GameObject("ShopPanelItemContainer");
        shopPanel.transform.SetParent(this.transform);
        
        RectTransform shopPanelRectTransform = shopPanel.AddComponent<RectTransform>();
        shopPanelRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        shopPanelRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        shopPanelRectTransform.pivot = new Vector2(0.5f, 0.5f);
        shopPanelRectTransform.anchoredPosition = Vector2.zero;

        // add a scroll panel to the shop panel
        ScrollRect scrollRect = shopPanel.AddComponent<ScrollRect>();
        scrollRect.horizontal = true;
        scrollRect.vertical = false;
        scrollRect.movementType = ScrollRect.MovementType.Elastic;
        scrollRect.viewport = shopPanel.GetComponent<RectTransform>();
        scrollRect.content = shopPanel.GetComponent<RectTransform>();



        // put a grid layout group on the panel
        GridLayoutGroup gridLayoutGroup = shopPanel.AddComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize = new Vector2(300, 100);
        gridLayoutGroup.spacing = new Vector2(10, 10);
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayoutGroup.constraintCount = 1;

        foreach (ShopItem shopItem in shopItems)
        {
            GameObject shopItemPanel = new GameObject("ShopItemPanel");
            shopItemPanel.transform.SetParent(shopPanel.transform);
            RectTransform rectTransform = shopItemPanel.AddComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;

            shopItemPanel.AddComponent<CanvasRenderer>();
            Image panelImage = shopItemPanel.AddComponent<Image>();
            panelImage.color = new Color(0.8f, 0.8f, 0.8f, 1.0f);

            GameObject nameText = new GameObject("NameText");
            nameText.transform.SetParent(shopItemPanel.transform);
            RectTransform nameTextRectTransform = nameText.AddComponent<RectTransform>();
            nameTextRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            nameTextRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            nameTextRectTransform.pivot = new Vector2(0.5f, 0.5f);
            nameTextRectTransform.anchoredPosition = new Vector2(-50, 0);

            nameText.AddComponent<CanvasRenderer>();
            Text nameTextComponent = nameText.AddComponent<Text>();
            nameTextComponent.text = shopItem.GetName();
            nameTextComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            nameTextComponent.fontSize = 24;
            nameTextComponent.color = Color.black;
            nameTextComponent.alignment = TextAnchor.MiddleCenter;

            GameObject buyButtonObject = new GameObject("BuyButton");
            buyButtonObject.transform.SetParent(shopItemPanel.transform);
            RectTransform buyButtonRectTransform = buyButtonObject.AddComponent<RectTransform>();
            buyButtonRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            buyButtonRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            buyButtonRectTransform.pivot = new Vector2(0.5f, 0.5f);
            buyButtonRectTransform.sizeDelta = new Vector2(100, 50);
            buyButtonRectTransform.anchoredPosition = new Vector2(100, 0);

            buyButtonObject.AddComponent<CanvasRenderer>();
            Image buyButtonImage = buyButtonObject.AddComponent<Image>();
            buyButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            Button buyButton = buyButtonObject.AddComponent<Button>();
            buyButton.onClick.AddListener(() => { shop.SellItem(shopItem, PlayerData); HideUI();});

            GameObject buyButtonText = new GameObject("BuyButtonText");
            buyButtonText.transform.SetParent(buyButtonObject.transform);
            RectTransform buyButtonTextRectTransform = buyButtonText.AddComponent<RectTransform>();
            buyButtonTextRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            buyButtonTextRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            buyButtonTextRectTransform.pivot = new Vector2(0.5f, 0.5f);
            buyButtonTextRectTransform.sizeDelta = new Vector2(100, 50);
            buyButtonTextRectTransform.anchoredPosition = Vector2.zero;

            buyButtonText.AddComponent<CanvasRenderer>();
            Text buyButtonTextComponent = buyButtonText.AddComponent<Text>();
            buyButtonTextComponent.text = "Sell";
            buyButtonTextComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            buyButtonTextComponent.fontSize = 24;
            buyButtonTextComponent.color = Color.black;
            buyButtonTextComponent.alignment = TextAnchor.MiddleCenter;
        }

        GameObject exitButtonObject = new GameObject("ExitButton");
        exitButtonObject.transform.SetParent(shopPanel.transform);
        RectTransform exitButtonRectTransform = exitButtonObject.AddComponent<RectTransform>();
        exitButtonRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        exitButtonRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        exitButtonRectTransform.pivot = new Vector2(0.5f, 0.5f);
        exitButtonRectTransform.sizeDelta = new Vector2(300, 50);
        exitButtonRectTransform.anchoredPosition = new Vector2(0, -60);

        exitButtonObject.AddComponent<CanvasRenderer>();
        Image exitButtonImage = exitButtonObject.AddComponent<Image>();
        exitButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        Button exitButton = exitButtonObject.AddComponent<Button>();
        exitButton.onClick.AddListener(() => HideUI());

        GameObject exitButtonText = new GameObject("ExitButtonText");
        exitButtonText.transform.SetParent(exitButtonObject.transform);
        RectTransform exitButtonTextRectTransform = exitButtonText.AddComponent<RectTransform>();
        exitButtonTextRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        exitButtonTextRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        exitButtonTextRectTransform.pivot = new Vector2(0.5f, 0.5f);
        exitButtonTextRectTransform.sizeDelta = new Vector2(300, 50);
        exitButtonTextRectTransform.anchoredPosition = Vector2.zero;

        exitButtonText.AddComponent<CanvasRenderer>();
        Text exitButtonTextComponent = exitButtonText.AddComponent<Text>();
        exitButtonTextComponent.text = "Exit";
        exitButtonTextComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        exitButtonTextComponent.fontSize = 24;
        exitButtonTextComponent.color = Color.black;
        exitButtonTextComponent.alignment = TextAnchor.MiddleCenter;
    }

    public void HideUI()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        this.gameObject.SetActive(false);
    }
}
