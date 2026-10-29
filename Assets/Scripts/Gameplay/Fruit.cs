using System;
using UnityEngine;

public class Fruit : SliceableObject
{
    [SerializeField] private float lifetime = 5;
    private float _timer = 0;
    
    
    [SerializeField] private Color _gibletColor;
    [SerializeField] private int _numGiblets = 4;
    [SerializeField] private GameObject gibletPrefab;

    private void Update()
    {
        if (GameManager.Instance.State == GameState.Play)
        {
            _timer += Time.deltaTime;
            if (_timer >= lifetime)
            {
                ScoreManager.Instance.onFruitMissed?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }

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
        // Play Audio
        //_audio.Play(0);
        //TODO vibrate
        // Make Giblets
        Splat();
    }
    
    protected void Splat()
    {
        for(int i=0;i<_numGiblets;i++)
        {
            GameObject giblet = Instantiate(gibletPrefab, transform.position, Quaternion.identity);
            giblet.GetComponent<SpriteRenderer>().color = _gibletColor;
        }
    }
}
