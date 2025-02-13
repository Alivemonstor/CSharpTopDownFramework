using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuickTimeEvent : MonoBehaviour
{
    public int timeToWait = 3;
    [SerializeField] private QuickTimeEventUI UI; 
    [SerializeField] private float duration = 3;
    public Queue<QuickTimeEventObject> sequence = new Queue<QuickTimeEventObject>();
    public List<KeyCode> keys = new List<KeyCode>{KeyCode.A, KeyCode.S, KeyCode.D};
    private bool isRunning = false;
    public void StartQuickTimeEvent()
    {
        isRunning = true;
        UI.StartUI();
        StartCoroutine(WaitAndQueue());
    }

    IEnumerator WaitAndQueue()
    {
        while (isRunning)
        {
            KeyCode keytopress = keys[Random.Range(0, keys.Count)];
            QuickTimeEventObject quickTimeEventObject = new QuickTimeEventObject();
            
            quickTimeEventObject.key = keytopress;
            quickTimeEventObject.duration = duration;
            quickTimeEventObject.time = Time.time;

            sequence.Enqueue(quickTimeEventObject);
            UI.SendSequence(quickTimeEventObject);
            yield return new WaitForSeconds(timeToWait);
        }
    }

    private void Update()
    {
        if (isRunning && Input.anyKeyDown)
        {
            if (sequence.Count > 0)
            {
                if (Input.GetKeyDown(sequence.Peek().key))
                {
                    QuickTimeEventObject quickTimeEventObject = sequence.Dequeue();
                    UI.ButtonPressed(true);
                }
                else
                {
                    UI.ButtonPressed(false);
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UI.StopUI();
                    isRunning = false;
                    sequence.Clear();
                }
            }
        }
    }
}
