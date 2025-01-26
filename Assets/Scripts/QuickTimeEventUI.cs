using UnityEngine;
using System.Collections.Generic;

public class QuickTimeEventUI : MonoBehaviour
{

    public List<TMPro.TextMeshProUGUI> textlist = new List<TMPro.TextMeshProUGUI>();
    [SerializeField] private QuickTimeEvent Client; 


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
            Debug.Log(text.gameObject.transform.position);
            if (!Screen.safeArea.Contains(text.gameObject.transform.position))
            {
                Destroy(text.gameObject);
                textlist.Remove(text);
                Client.sequence.Dequeue();
            }
            text.gameObject.transform.position = new Vector3(text.gameObject.transform.position.x - 1, text.gameObject.transform.position.y, text.gameObject.transform.position.z);            
        }
    }
    

    public void ButtonPressed()
    {
        Destroy(textlist[0]);
        textlist.RemoveAt(0);
    }

    public void StopUI()
    {
        foreach (TMPro.TextMeshProUGUI text in textlist)
        {
            Destroy(text.gameObject);
        }
        textlist.Clear();
    }

    public void SendSequence(KeyCode key)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(this.gameObject.transform);
        TMPro.TextMeshProUGUI keyText = gameObject.AddComponent<TMPro.TextMeshProUGUI>();
        keyText.text = key.ToString();
        gameObject.transform.position = new Vector3(Screen.width - 100, Screen.height / 2, 0);
        textlist.Add(keyText);
    }
}
