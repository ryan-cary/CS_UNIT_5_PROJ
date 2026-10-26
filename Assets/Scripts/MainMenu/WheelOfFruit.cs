using UnityEngine;

public class WheelOfFruit : MonoBehaviour
{
    [SerializeField] private GameObject[] fruits;
    
    [Header("Wheel Parameters")]
    [SerializeField] private float radius = 1;
    [SerializeField] private int numFruit = 5;
    [SerializeField][Range(0,100)] private float rotationSpeed = 1;
    
    private GameObject[] _elements;
    private float _activeRotation;

    void Start()
    {
        _elements = new GameObject[numFruit];
        
        for (int i = 0; i < numFruit; i++)
        {
            int fruitIndex = Random.Range(0, fruits.Length);
            _elements[i] = Instantiate(fruits[fruitIndex], Vector3.zero, Quaternion.identity);
        }
    }

    private void Update()
    {
        int count = _elements.Length;
        for (int i = 0; i < count; i++)
        {
            float angle = 360f / count * i;

            Quaternion rotation = Quaternion.Euler(0, 0, angle + _activeRotation);
            Vector3 target = rotation * Vector2.up * radius;
            _elements[i].transform.position = target + transform.position;
        }

        _activeRotation += rotationSpeed * Time.deltaTime;
    }
}
