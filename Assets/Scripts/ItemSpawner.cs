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

    private bool lastWasAnomaly;
    private int normalCount;

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
        bool spawnAnomaly = false;

        if (lastWasAnomaly)
        {
            spawnAnomaly = false;
        }
        else if (normalCount >= 4)
        {
            spawnAnomaly = true;
        }
        else
        {
            int randomNumber = Random.Range(0, 100);

            if (randomNumber < anomalyChance)
                spawnAnomaly = true;
        }

        if (spawnAnomaly)
        {
            Instantiate(anomalyItem, transform.position, Quaternion.identity);

            lastWasAnomaly = true;
            normalCount = 0;
        }
        else
        {
            Instantiate(normalItem, transform.position, Quaternion.identity);

            lastWasAnomaly = false;
            normalCount++;
        }
    }
}