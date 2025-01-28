using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuickTimeEvent : MonoBehaviour
{
    public int timeToWait = 3;
    [SerializeField] private QuickTimeEventUI UI; 
    public Queue<KeyCode> sequence = new Queue<KeyCode>();
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
            sequence.Enqueue(keytopress);
            UI.SendSequence(keytopress);
            yield return new WaitForSeconds(timeToWait);
        }
    }

    private void Update()
    {
        if (isRunning && Input.anyKeyDown)
        {
            if (sequence.Count > 0)
            {
                if (Input.GetKeyDown(sequence.Peek()))
                {
                    sequence.Dequeue();
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
