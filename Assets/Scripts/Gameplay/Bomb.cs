using UnityEngine;

public class Bomb : SliceableObject
{
    [SerializeField] private GameObject explosionPrefab;
    
    public override void OnSliced()
    {
        // Explosion sound effect / animation
        SliceEffect();

        // Deduct score
        ScoreManager.Instance.onBombSliced?.Invoke();
    }

    public override void SliceEffect()
    {
        HapticsController.Instance.Vibrate(200);
        
        //Create visual effect
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
