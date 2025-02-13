using UnityEngine;

public abstract class InteractionPoint : MonoBehaviour
{
    public virtual bool CanInteract()
    {
        return Random.Range(0, 2) == 0;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }

    public virtual string GetText()
    {
        return "Press E to interact with " + gameObject.name;
    }
}

