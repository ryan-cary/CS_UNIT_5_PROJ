using UnityEngine;

[System.Serializable] public struct Launchable
{
    public GameObject prefab;
    public float spawnWeight;
}

public class Launcher : MonoBehaviour
{
    [SerializeField] private Launchable[] launchableObjects;

    [Header("Bomb")] 
    [SerializeField] private GameObject bombPrefab;
    [SerializeField][Range(0,0.5f)] private float bombChance;

    [Header("Spawn Parameters")] 
    [SerializeField] private float maxSpawnFrequency = 1.0f;
    [SerializeField] private float verticalOffset = -1.0f;
    [SerializeField] private float horizontalPadding = 1.0f;
    private float _spawnFrequency = 1.0f;

    [Header("Launch Parameters")]
    [SerializeField] private float maxForce = 15.0f;
    [SerializeField] private float minForce = 8.0f;
    [SerializeField] private float maxAngle = 10.0f;
    [SerializeField] private float minRotationSpeed = 10f;
    [SerializeField] private float maxRotationSpeed = 180f;

    private float _timer;

    private void Update()
    {
        if (GameManager.Instance.State == GameState.Play)
        {
            _timer += Time.deltaTime;

            if (_timer > _spawnFrequency)
            {
                SpawnFruit(GetFruitToSpawn());
                _spawnFrequency = Random.Range(0, maxSpawnFrequency);
            }
        }
    }

    private GameObject GetFruitToSpawn()
    {
        float f = Random.Range(0f, 1f);
        if (f < bombChance)
        {
            return bombPrefab;
        }
        
        int index = Random.Range(0, launchableObjects.Length);
        return launchableObjects[index].prefab;
    }

    private Vector2 GetSpawnLocation()
    {
        Camera cam = FindFirstObjectByType<Camera>();
        
        float randomWidth = Random.Range(0 + horizontalPadding, Screen.width - horizontalPadding);
        
        return cam.ScreenToWorldPoint(new Vector2(randomWidth, verticalOffset));
    }

    private void SpawnFruit(GameObject prefab)
    {
        // Instantiate Prefab
        GameObject fruit = Instantiate(prefab, GetSpawnLocation(), Quaternion.identity);
        
        //Get random rotation
        float angle = Random.Range(0, maxAngle);
        if (IsOnLeftSide(fruit))
            angle = -angle;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        
        //Build vector
        Vector3 launchAngle = rotation * Vector2.up;
        
        //Get random launch force
        float force = Random.Range(minForce, maxForce);
        
        // Launch prefab
        Rigidbody2D fruitRb = fruit.GetComponent<Rigidbody2D>();
        fruitRb.AddForce(launchAngle * force, ForceMode2D.Impulse);
        fruitRb.angularVelocity = GetRandomRotationSpeed();

        _timer = 0;
    }

    private float GetRandomRotationSpeed()
    {
        float speed = Random.Range(minRotationSpeed, maxRotationSpeed);
        int direction = Random.Range(-1, 2);

        return speed * direction;
    }

    private bool IsOnLeftSide(GameObject fruit)
    {
        if (fruit.transform.position.x < 0)
            return true;

        return false;
    }
}
