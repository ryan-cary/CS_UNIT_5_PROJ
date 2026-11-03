using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private float delay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, delay);
    }
}
