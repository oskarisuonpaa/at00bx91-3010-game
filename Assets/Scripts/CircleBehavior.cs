using UnityEngine;

public class CircleBehavior : MonoBehaviour
{
    public CircleSpawner spawner;

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.CircleDestroyed();
        }
    }
}
