using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float detectionRadius = 0.2f;
    [SerializeField] private TMPro.TextMeshProUGUI interactionText;

    void Update()
    {
        if (TryGetOptimalInteractionPoint() is InteractionPoint interactionPoint)
        {
            interactionText.gameObject.SetActive(true);
            interactionText.text = interactionPoint.GetText();
            interactionText.rectTransform.position = Camera.main.WorldToScreenPoint(interactionPoint.transform.position); 

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactionPoint.Interact();
            }
        }
        else
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    InteractionPoint TryGetOptimalInteractionPoint()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayer);

        InteractionPoint optimal = null;
        float minDistance = float.MaxValue;

        // check that I can interact with them 
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out InteractionPoint interactionPoint))
            {
                if (!interactionPoint.CanInteract())
                {
                    continue;
                }

                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    optimal = interactionPoint;
                }
            }
        }
        return optimal;
    }
}
