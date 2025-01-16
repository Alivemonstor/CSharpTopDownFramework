using UnityEngine;

public class InteractionPoint : MonoBehaviour
{

    public Transform detectionPoint;

    [SerializeField] private const float detectionRadius = 0.2f;

    public LayerMask detectionLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DetectInteraction())
        {
            if(InteractInput())
            {
                Debug.Log("Yeah");
            }
        }
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectInteraction()
    {
        return Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
    }
}
