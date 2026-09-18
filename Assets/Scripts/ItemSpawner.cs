using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    private float spawnInterval = 2f;

    private float timer;

    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnItem();
            timer = 0f;
        }
    }

    private void SpawnItem()
    {
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}