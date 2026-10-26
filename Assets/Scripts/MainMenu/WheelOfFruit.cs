using UnityEngine;

public class WheelOfFruit : MonoBehaviour
{
    [SerializeField] private GameObject[] fruits;
    
    [Header("Wheel Parameters")]
    [SerializeField] private float radius = 1;
    [SerializeField] private int numFruit = 5;
    [SerializeField] private float rotationSpeed = 1;
    
    private GameObject[] elements;

    void Start()
    {
        elements = new GameObject[numFruit];
        
        for (int i = 0; i < numFruit; i++)
        {
            int fruitIndex = Random.Range(0, fruits.Length);
            elements[i] = Instantiate(fruits[fruitIndex], Vector3.zero, Quaternion.identity);
        }
    }

    private void Update()
    {
        int count = elements.Length;
        for (int i = 0; i < count; i++)
        {
            float angle = 360f / count * i;

            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 target = rotation * Vector2.up * radius;
            elements[i].transform.position = target;
        }
    }
}
