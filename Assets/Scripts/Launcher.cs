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
    //[SerializeField] private float maxangle;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > spawnFrequency)
            SpawnFruit(GetFruitToSpawn());
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
        //TODO get rotation vector
        
        //Get random launch force
        float force = Random.Range(minForce, maxForce);
        
        // Launch prefab
        fruit.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);

        _timer = 0;
    }
}
