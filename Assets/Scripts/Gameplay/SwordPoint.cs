using System;
using UnityEngine;

public class SwordPoint : MonoBehaviour
{
    private void Slice(SliceableObject sliceable)
    {
        if (sliceable != null)
        {
            sliceable.OnSliced();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fruit") || other.gameObject.CompareTag("Bomb"))
        {
            SliceableObject sliceable = other.GetComponent<SliceableObject>();
            Slice(sliceable);
        }
    }
}
