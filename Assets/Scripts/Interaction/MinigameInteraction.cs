using UnityEngine;

public class MinigameInteraction : InteractionPoint
{
    public override bool CanInteract()
    {
        return true;
    }

    public override void Interact()
    {
        GetComponent<QuickTimeEvent>().StartQuickTimeEvent();
    }

}
