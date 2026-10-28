using UnityEngine;

public class Bomb : SliceableObject
{
    [SerializeField] private int _scorePenalty = 10;

    public override void OnSliced()
    {
        // Explosion sound effect / animation
        SliceEffect();
        
        // Destroy all fruit on screen (but not as sliced)
        ClearBoard();

        // Deduct score
        ScoreManager.Instance.Penalize(_scorePenalty);
    }

    public override void SliceEffect()
    {
        Debug.Log("Playing bomb audio");
        _audio.Play(0);
        
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
