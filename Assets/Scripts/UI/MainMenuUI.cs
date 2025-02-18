using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private List<PastPlayerData> pastPlayerData;
    [SerializeField] private List<Button> Buttons;
    [SerializeField] private PlayerData playerData;
    
    [SerializeField] private List<GameObject> panels;

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

public void LoadPastGames()
{
    pastPlayerData = playerData.GetFinishedGames();
    foreach (Button button in Buttons)
    {
        button.gameObject.SetActive(false);
    }

    float panelHeight = 100f; 
    float spacing = 10f;

    Vector3 startPosition = Buttons[0].transform.position;


    foreach (PastPlayerData pastData in pastPlayerData)
    {
        GameObject panel = new GameObject("PastGamePanel");
        panel.transform.SetParent(this.transform);
        RectTransform rectTransform = panel.AddComponent<RectTransform>();
        panel.AddComponent<CanvasRenderer>();
        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = new Color(0.8f, 0.8f, 0.8f, 1.0f); 

        Outline panelOutline = panel.AddComponent<Outline>();
        panelOutline.effectColor = Color.black;
        panelOutline.effectDistance = new Vector2(2, 2);

        rectTransform.sizeDelta = new Vector2(300, panelHeight); 
        rectTransform.position = new Vector3(startPosition.x, startPosition.y - (pastPlayerData.IndexOf(pastData) * (panelHeight + spacing)), startPosition.z);

        GameObject text = new GameObject("PastGameText");
        text.transform.SetParent(panel.transform);
        RectTransform textRectTransform = text.AddComponent<RectTransform>();
        text.AddComponent<CanvasRenderer>();
        Text textComponent = text.AddComponent<Text>();
        textComponent.text = "Coins: " + pastData.Coins + " Days: " + pastData.Days;
        textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textComponent.fontSize = 24;
        textComponent.color = Color.black;
        textComponent.alignment = TextAnchor.MiddleCenter;

        textRectTransform.sizeDelta = rectTransform.sizeDelta;
        textRectTransform.anchoredPosition = Vector2.zero;

        panels.Add(panel);
    }

    GameObject backButton = new GameObject("BackButton");
    backButton.transform.SetParent(this.transform);
    RectTransform backButtonRectTransform = backButton.AddComponent<RectTransform>();
    backButton.AddComponent<CanvasRenderer>();
    Image backButtonImage = backButton.AddComponent<Image>();
    backButtonImage.color = new Color(0.5f, 0.5f, 0.5f, 1.0f); 
    Button backButtonComponent = backButton.AddComponent<Button>();

    Outline backButtonOutline = backButton.AddComponent<Outline>();
    backButtonOutline.effectColor = Color.black;
    backButtonOutline.effectDistance = new Vector2(2, 2);

    backButtonRectTransform.sizeDelta = new Vector2(300, panelHeight);
    backButtonRectTransform.position = new Vector3(startPosition.x, startPosition.y - (pastPlayerData.Count * (panelHeight + spacing)), startPosition.z);

    GameObject backButtonText = new GameObject("BackButtonText");
    backButtonText.transform.SetParent(backButton.transform);
    RectTransform backButtonTextRectTransform = backButtonText.AddComponent<RectTransform>();
    backButtonText.AddComponent<CanvasRenderer>();
    Text backButtonTextComponent = backButtonText.AddComponent<Text>();
    backButtonTextComponent.text = "Back";
    backButtonTextComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
    backButtonTextComponent.fontSize = 24;
    backButtonTextComponent.color = Color.black;
    backButtonTextComponent.alignment = TextAnchor.MiddleCenter;

    backButtonTextRectTransform.sizeDelta = backButtonRectTransform.sizeDelta;
    backButtonTextRectTransform.anchoredPosition = Vector2.zero;

    backButtonComponent.onClick.AddListener(() =>
    {
        foreach (Button button in Buttons)
        {
            button.gameObject.SetActive(true);
        }

        foreach (GameObject gameObject in panels)
        {
            Destroy(gameObject);
        }

        Destroy(backButton);
    });
}
}