using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] GameObject botPrefab;

    public int amountOfBots = 10;

    public float spawnHeight = 1;
    public float spawnMargin = 3;

    float spawnMinLength, spawnMaxLength;
    float spawnMinWidth, spawnMaxWidth;

    Vector3 spawnBias;

    void Start()
    {
        spawnMinLength = -(platform.transform.localScale.x / 2 - spawnMargin);
        spawnMaxLength = platform.transform.localScale.x / 2 - spawnMargin;

        spawnMinWidth = -(platform.transform.localScale.z / 2 - spawnMargin);
        spawnMaxWidth = platform.transform.localScale.z / 2 - spawnMargin;

        spawnBias = platform.transform.position;

        SpawnBots(amountOfBots);
    }

    public void SpawnBots(int amount)
    {
        Debug.Log("Function started");

        Vector3 spawnLoc;

        for (int i = 0; i < amount; i++)
        {
            spawnLoc = new Vector3(Random.Range(spawnMinWidth, spawnMaxWidth), spawnHeight, Random.Range(spawnMinLength, spawnMaxLength)) + spawnBias;

            Debug.Log("Spawned bot at location" + spawnLoc);

            Instantiate(botPrefab, spawnLoc, Quaternion.identity);
        }
    }
}
