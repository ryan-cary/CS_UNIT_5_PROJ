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
        //TODO explosion sound effect
        //TODO explosion visual effect
        
        // Destroy all fruit on screen (but not as sliced)
        //TODO destroy all fruit
        
        // Deduct score
        //TODO deduct score
    }
}
