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
    [SerializeField] private float spawnFrequency = 1.0f;
    [SerializeField] private float verticalOffset = -1.0f;
    [SerializeField] private float horizontalPadding = 1.0f;

    [Header("Launch Parameters")]
    [SerializeField] private float maxForce = 15.0f;
    [SerializeField] private float minForce = 8.0f;
    [SerializeField] private float maxAngle = 10.0f;

    private float _timer;

    private void Update()
    {
        if (GameManager.Instance.State == GameState.Play)
        {
            _timer += Time.deltaTime;

            if (_timer > spawnFrequency)
                SpawnFruit(GetFruitToSpawn());
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
        fruit.GetComponent<Rigidbody2D>().AddForce(launchAngle * force, ForceMode2D.Impulse);

        _timer = 0;
    }

    private bool IsOnLeftSide(GameObject fruit)
    {
        if (fruit.transform.position.x < 0)
            return true;

        return false;
    }
}
