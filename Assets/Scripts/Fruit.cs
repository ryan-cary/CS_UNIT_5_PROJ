using UnityEngine;

public class Fruit : SliceableObject
{
    [SerializeField] private Color _gibletColor;
    [SerializeField] private int _numGiblets = 4;

    public override void OnSliced()
    {
        //Destroy
        Destroy(this.gameObject);
        // Add score
        ScoreManager.Instance.onFruitSliced?.Invoke();
        //Play effects
        SliceEffect();
    }

    public override void SliceEffect()
    {
        throw new System.NotImplementedException();
        
        // Play Audio
        _audio.Play(0);
        //TODO vibrate
        // Make Giblets
    }
}
