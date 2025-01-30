using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
public class QuickTimeEventUI : MonoBehaviour
{
    public Dictionary<TMPro.TextMeshProUGUI, QuickTimeEventObject> textlist = new Dictionary<TMPro.TextMeshProUGUI, QuickTimeEventObject>();
    [SerializeField] private QuickTimeEvent Client; 
    [SerializeField] private GameObject Panel;


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
            Debug.Log(textlist[text].time + textlist[text].duration);
            Debug.Log(text);
            if (textlist[text].time + textlist[text].duration < Time.time)
            {
                Destroy(text.gameObject);
                textlist.Remove(text);
                i-=1;
                continue;
            }
            text.rectTransform.position = new Vector3(text.rectTransform.position.x - (Screen.width / textlist[text].duration) * Time.fixedDeltaTime, text.rectTransform.position.y, text.rectTransform.position.z);
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
            if (textObject != null)
            {
                Destroy(textObject.gameObject);
                textlist.Remove(textObject);
            }
        }
        else
        {
            // Destroy(textlist[0].gameObject);
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
            if (textlist[text].time + textlist[text].duration < closestTime)
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
