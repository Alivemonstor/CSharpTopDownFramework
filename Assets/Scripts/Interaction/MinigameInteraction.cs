using UnityEngine;

public class MinigameInteraction : InteractionPoint
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerData playerData;
    public override bool CanInteract()
    {
        return true;
    }

    public override void Interact()
    {
        // GetComponent<QuickTimeEvent>().StartQuickTimeEvent();

        // give player a random item
        int index = playerData.GetRandomItemIndex();
        string name = playerData.GetNameFromIndex(index);
        Item item = new Item(name, 1, true, -1, index);
        inventory.AddItem(item);
        
    }

}
