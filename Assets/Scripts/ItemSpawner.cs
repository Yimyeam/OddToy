using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject normalItem;

    [SerializeField]
    private GameObject anomalyItem;

    [SerializeField]
    private float spawnInterval = 2f;

    [SerializeField]
    private int anomalyChance = 30;

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
        int randomNumber = Random.Range(0, 100);

        if (randomNumber < anomalyChance)
        {
            Instantiate(anomalyItem, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(normalItem, transform.position, Quaternion.identity);
        }
    }
}