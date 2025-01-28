using UnityEngine;
using System.Collections.Generic;
public class QuickTimeEventUI : MonoBehaviour
{
    public List<TMPro.TextMeshProUGUI> textlist = new List<TMPro.TextMeshProUGUI>();
    [SerializeField] private QuickTimeEvent Client; 
    [SerializeField] private GameObject Panel;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (textlist.Count == 0)
        {
            return;
        }
        for (int i = 0; i < textlist.Count; i++)
        {
            TMPro.TextMeshProUGUI text = textlist[i];
            if (!Screen.safeArea.Contains(text.gameObject.transform.position))
            {
                Destroy(text.gameObject);
                textlist.Remove(text);
                Client.sequence.Dequeue();
            }
            text.gameObject.transform.position = new Vector3(text.gameObject.transform.position.x - 2, text.gameObject.transform.position.y, text.gameObject.transform.position.z);            
        }
    }
    
    public void StartUI()
    {
        Panel.SetActive(true);
    }

    public void ButtonPressed(bool hit)
    {
        if (hit)
        {
            GetButtonOnPanel();
            Destroy(textlist[0].gameObject);
            textlist.RemoveAt(0);
        }
        else
        {
            Destroy(textlist[0].gameObject);
            
        }
    }

    public void StopUI()
    {
        foreach (TMPro.TextMeshProUGUI text in textlist)
        {
            Destroy(text.gameObject);
        }
        textlist.Clear();
        Panel.SetActive(false);
    }

    public void SendSequence(KeyCode key)
    {
        GameObject gameObject = new GameObject("Key " + key.ToString());
        gameObject.transform.SetParent(this.gameObject.transform);
        TMPro.TextMeshProUGUI keyText = gameObject.AddComponent<TMPro.TextMeshProUGUI>();
        keyText.text = key.ToString();
        gameObject.transform.position = new Vector3(Screen.width - 100, Screen.height / 2, 0);
        textlist.Add(keyText);
    }

    public TMPro.TextMeshProUGUI GetButtonOnPanel()
    {
        Debug.Log(Panel.GetComponent<RectTransform>().localPosition.x);
        foreach (TMPro.TextMeshProUGUI text in textlist)
        {
            bool blah = text.rectTransform.offsetMin.x <= 200 && text.rectTransform.offsetMin.x >= 0;
            if (blah) { return text; }
        }
        return null;
    }

    public bool IsInUI()
    {
        return Panel.activeSelf;
    }
}
