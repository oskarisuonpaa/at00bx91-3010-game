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
        // Random position inside 100x100 square
        float x = Random.Range(-mapSize / 2, mapSize / 2);
        float y = Random.Range(-mapSize / 2, mapSize / 2);
        Vector2 spawnPos = new Vector2(x, y);

        GameObject circle = Instantiate(circlePrefab, spawnPos, Quaternion.identity);
        circle.transform.localScale = new Vector3(2f, 2f, 1f);

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