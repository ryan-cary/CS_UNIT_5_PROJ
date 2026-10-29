using UnityEngine;

public class Giblet : MonoBehaviour
{
    [SerializeField] private float launchForce;
    private Rigidbody2D _rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector2 launchVector = new Vector2(Random.Range(-launchForce, launchForce), Random.Range(-launchForce, launchForce));
        _rb.AddForce(launchVector, ForceMode2D.Impulse);
    }
}
