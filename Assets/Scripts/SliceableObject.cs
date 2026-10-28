using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class SliceableObject : MonoBehaviour
{
    protected AudioSource _audio;
    
    protected void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public abstract void OnSliced();
    public abstract void SliceEffect();
}
