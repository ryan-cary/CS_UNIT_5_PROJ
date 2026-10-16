using UnityEngine;

public class Bomb : MonoBehaviour, ISliceable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSliced()
    {
        // Explosion sound effect / animation
        // Destroy all fruit on screen (but not as sliced)
        // Deduct score
    }
}
