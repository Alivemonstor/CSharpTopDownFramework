using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
public class QuickTimeEventUI : MonoBehaviour
{
    public Dictionary<TMPro.TextMeshProUGUI, QuickTimeEventObject> textlist = new Dictionary<TMPro.TextMeshProUGUI, QuickTimeEventObject>();
    [SerializeField] private QuickTimeEvent Client; 
    [SerializeField] private GameObject Panel;
    public List<string> MissedStrings = new List<string>{"You Missed!", "Wrong Key!", "Try Again"};



    // Update is called once per frame
    void FixedUpdate()
    {
        if (textlist.Count == 0)
        {
            return;
        }

        for (int i = 0, textlistCount = textlist.Keys.Count; i < textlistCount; i++)
        {
            TMPro.TextMeshProUGUI text = textlist.Keys.ElementAt(i);
            text.rectTransform.position = new Vector3(text.rectTransform.position.x - (Screen.width / textlist[text].duration) * Time.fixedDeltaTime, text.rectTransform.position.y, text.rectTransform.position.z);
        }
    }

    void Update()
    {
        if (textlist.Count == 0)
        {
            return;
        }
        
        for (int i = 0, textlistCount = textlist.Keys.Count; i < textlistCount; i++)
        {
            TMPro.TextMeshProUGUI text = textlist.Keys.ElementAt(i);
            if (textlist[text].time + textlist[text].duration < Time.time)
            {
                Destroy(text.gameObject);
                textlist.Remove(text);
                i-=1;
                continue;
            }
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
            TMPro.TextMeshProUGUI textObject = GetButtonClosestToEndTime();
            Debug.Log(textObject);
            if (textObject != null)
            {
                Destroy(textObject.gameObject);
                textlist.Remove(textObject);
            }
        }
        else
        {
            GameObject gameObject = new GameObject("MissedKey");
            gameObject.transform.SetParent(this.gameObject.transform);
            TMPro.TextMeshProUGUI keyText = gameObject.AddComponent<TMPro.TextMeshProUGUI>();
            System.Random rnd = new System.Random();
            int index = rnd.Next(MissedStrings.Count);
            keyText.text = MissedStrings[index];
            gameObject.transform.position = new Vector3(Screen.width - 100, Screen.height / 2, 0);

        }
    }

    public void StopUI()
    {
        foreach (TMPro.TextMeshProUGUI text in textlist.Keys)
        {
            Destroy(text.gameObject);
        }
        textlist.Clear();
        Panel.SetActive(false);
    }

    public void SendSequence(QuickTimeEventObject KeyObject)
    {
        
        GameObject gameObject = new GameObject("Key " + KeyObject.key.ToString());
        gameObject.transform.SetParent(this.gameObject.transform);
        TMPro.TextMeshProUGUI keyText = gameObject.AddComponent<TMPro.TextMeshProUGUI>();
        keyText.text = KeyObject.key.ToString();
        gameObject.transform.position = new Vector3(Screen.width - 100, Screen.height / 2, 0);
        textlist.Add(keyText, KeyObject);

    }

    public TMPro.TextMeshProUGUI GetButtonClosestToEndTime()
    {
        TMPro.TextMeshProUGUI closest = null;
        float closestTime = 0;
        foreach (TMPro.TextMeshProUGUI text in textlist.Keys)
        {
            if (textlist[text].time + textlist[text].duration > closestTime)
            {
                closest = text;
                closestTime = textlist[text].time + textlist[text].duration;
            }
        }
        return closest;
    }

    public bool IsInUI()
    {
        return Panel.activeSelf;
    }
}
