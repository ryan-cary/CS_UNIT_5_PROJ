using UnityEngine;

public class Bomb : MonoBehaviour, ISliceable
{
    private AudioSource _audio;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void OnSliced()
    {
        // Explosion sound effect / animation
        Kaboom();
        
        // Destroy all fruit on screen (but not as sliced)
        ClearBoard();

        // Deduct score
        //TODO deduct score
    }

    private void Kaboom()
    {
        _audio.Play();
        
        //TODO play visual effect
    }

    private void ClearBoard()
    {
        
        Fruit[] fruitToBeDestroyed = FindObjectsByType<Fruit>(FindObjectsSortMode.None);
        Bomb[] bombsToBeDestroyed = FindObjectsByType<Bomb>(FindObjectsSortMode.None);

        foreach (Fruit f in (fruitToBeDestroyed))
        {
            Destroy(f.gameObject);
        }

        foreach (Bomb b in bombsToBeDestroyed)
        {
            Destroy(b.gameObject);
        }
    }
}
