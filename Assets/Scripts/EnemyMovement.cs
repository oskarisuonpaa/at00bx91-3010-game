using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float visionRange = 5f;       // How far the enemy can "see"
    public float speed = 2f;             // Movement speed
    public Transform player;             // Reference to player circle
    public float searchInterval = 0.5f;  // How often to refresh food search

    private Transform currentFoodTarget;
    private Rigidbody2D rb;              // Rigidbody reference
    private Vector2 chaseTarget;         // Current target position

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Auto-find player by tag if not assigned
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
            else Debug.LogError("No Player found in scene!");
        }

        StartCoroutine(FoodSearchRoutine());
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            float dist = Vector2.Distance(transform.position, player.position);
            Debug.Log("Enemy at " + transform.position + " sees Player at " + player.position + " distance: " + dist);

            if (IsBiggerThanPlayer())
            {
                // Predator mode: chase the player
                chaseTarget = player.position;
            }
            else
            {
                // Survival mode: eat food instead
                if (currentFoodTarget != null)
                    chaseTarget = currentFoodTarget.position;
            }
        }
        else
        {
            // Default: seek food
            if (currentFoodTarget != null)
                chaseTarget = currentFoodTarget.position;
        }
    }

    private void FixedUpdate()
    {
        // Physics-friendly movement
        if (chaseTarget != Vector2.zero)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, chaseTarget, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }

    // --- Detection ---
    bool CanSeePlayer()
    {
        return Vector2.Distance(transform.position, player.position) <= visionRange;
    }

    bool IsBiggerThanPlayer()
    {
        return transform.localScale.x > player.localScale.x;
    }

    // --- Coroutine for food search ---
    IEnumerator FoodSearchRoutine()
    {
        while (true)
        {
            GameObject[] massCells = GameObject.FindGameObjectsWithTag("MassCell");
            Transform nearest = null;
            float minDist = Mathf.Infinity;

            foreach (GameObject massCell in massCells)
            {
                if (massCell == null) continue;
                float dist = Vector2.Distance(transform.position, massCell.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = massCell.transform;
                }
            }

            currentFoodTarget = nearest;

            yield return new WaitForSeconds(searchInterval);
        }
    }

    // --- Collision Handling ---
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MassCell"))
        {
            Grow();
            Destroy(collision.gameObject);
        }
    }

    // --- Growth mechanic ---
    void Grow()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0f);
    }
}
