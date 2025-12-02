using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerups;
    public float spawnInterval = 20f;

    private float minX = -50f;
    private float maxX = 50f;
    private float minY = -50f;
    private float maxY = 50f;

    private void Start()
    {
        Debug.Log("Spawner started");
        InvokeRepeating(nameof(SpawnPowerup), 2f, spawnInterval);
    }

    void SpawnPowerup()
    {
        if (powerups == null || powerups.Length == 0)
        {
            Debug.LogError("NO POWERUP PREFABS ASSIGNED!");
            return;
        }

        GameObject prefab = powerups[Random.Range(0, powerups.Length)];

        Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        Debug.Log("Spawn at: " + pos);
        Instantiate(prefab, pos, Quaternion.identity);
    }
}
