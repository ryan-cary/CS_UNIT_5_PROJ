using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class SliceableObject : MonoBehaviour
{
    public abstract void OnSliced();
    public abstract void SliceEffect();
}
