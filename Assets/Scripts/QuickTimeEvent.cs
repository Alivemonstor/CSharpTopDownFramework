using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuickTimeEvent : MonoBehaviour
{
    public int timeToWait = 1;
    private Queue<KeyCode> sequence = new Queue<KeyCode>();
    public List<KeyCode> keys = new List<KeyCode>{KeyCode.A, KeyCode.S, KeyCode.D};
    private bool isRunning = false;
    public void StartQuickTimeEvent()
    {
        isRunning = true;
        StartCoroutine(WaitAndQueue());
    }

    IEnumerator WaitAndQueue()
    {
        while (isRunning)
        {
            sequence.Enqueue(keys[Random.Range(0, keys.Count)]);
            Debug.Log("New sequence: " + string.Join(", ", sequence));
            yield return new WaitForSeconds(timeToWait);
        }
    }

    private void Update()
    {
        if (isRunning && Input.anyKeyDown)
        {
            if (Input.GetKeyDown(sequence.Peek()))
            {
                sequence.Dequeue();
                Debug.Log("Correct key pressed!");
            }
            else
            {
                Debug.Log("Wrong key pressed!");
            }
        }
    }
}
