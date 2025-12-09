using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleSpawner : MonoBehaviour
{
    public GameObject circlePrefab;
    public int totalCircles = 100;
    public float mapSize = 100f;

    private bool isQuitting = false;
    private int currentCircles;

    void Start()
    {
        SpawnInitialCircles();
    }

    void SpawnInitialCircles()
    {
        for (int i = 0; i < totalCircles; i++)
        {
            SpawnCircle();
        }
    }

    public void SpawnCircle()
    {
        float scale = 1f;

        // calculate radius from prefab sprite size
        float circleRadius = circlePrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
        float radius = circleRadius * scale;

        // Random position inside 100x100 square
        float x = Random.Range(-mapSize / 2 + radius, mapSize / 2 - radius);
        float y = Random.Range(-mapSize / 2 + radius, mapSize / 2 - radius);
        Vector2 spawnPos = new Vector2(x, y);

        GameObject circle = Instantiate(circlePrefab, spawnPos, Quaternion.identity);
        circle.transform.localScale = new Vector3(scale, scale, 1f);

        // Hook destruction callback to notify spawner of the amount of it's children
        CircleBehavior behavior = circle.AddComponent<CircleBehavior>();
        behavior.spawner = this;

        currentCircles++;
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public void CircleDestroyed()
    {
        if (!isQuitting && gameObject.scene.isLoaded) // double safety check
        {
            currentCircles--;
            SpawnCircle(); // Always keep 100 alive
        }
    }
}
