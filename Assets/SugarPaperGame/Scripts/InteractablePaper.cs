using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePaper : MonoBehaviour
{
    private bool isInteractable = false;

    private void Start()
    {
        if (TryGetComponent<Collider>(out var collider))
        {
            isInteractable = true;
        }
    }

    private void OnMouseDown()
    {
        if (isInteractable)
        {

        }
    }

    private void OnMouseDrag()
    {
        
    }
}
