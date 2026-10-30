using UnityEngine;

public class Bomb : SliceableObject
{

    public override void OnSliced()
    {
        // Explosion sound effect / animation
        SliceEffect();
        
        // Destroy all fruit on screen (but not as sliced)
        ClearBoard();

        // Deduct score
        ScoreManager.Instance.onBombSliced?.Invoke();
    }

    public override void SliceEffect()
    {
        Debug.Log("Playing bomb audio");
        _audio.PlayOneShot(_audio.clip);
        
        //TODO play visual effect
        //TODO vibrate
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
