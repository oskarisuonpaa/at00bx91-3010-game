using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool isShielded = false;
    public float growMultiplier = 1f;
    public SpriteRenderer powerupIcon;
    public Sprite starSprite;
    public Sprite shieldSprite;
    public Sprite speedSprite;

    [Header("Mass Settings")]
    public float baseMass = 1f;
    public float massPerGrowth = 0.5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.mass = baseMass;
        }
    }

    public void ShowPowerupIcon(Sprite icon)
    {
        if (powerupIcon != null)
        {
            powerupIcon.sprite = icon;
            powerupIcon.enabled = true;
            powerupIcon.transform.localScale = Vector3.one * 0.1f;
        }
    }

    public void HidePowerupIcon()
    {
        if (powerupIcon != null)
        {
            powerupIcon.enabled = false;
            powerupIcon.sprite = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MassCell") || other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            Grow();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Use Rigidbody2D mass for comparison instead of scale
            float myMass = rb != null ? rb.mass : transform.localScale.x;
            float enemyMass =
                collision.rigidbody != null
                    ? collision.rigidbody.mass
                    : collision.transform.localScale.x;

            // --- SHIELD BEHAVIOR ---
            if (isShielded)
            {
                if (myMass > enemyMass)
                {
                    // Shield + Player is bigger -> eat enemy
                    Destroy(collision.gameObject);
                    Grow();
                }
                else
                {
                    // Shield + Enemy is bigger -> do NOTHING
                    // Player should not die or eat the enemy
                    // Shield stays active
                }

                return; // Important: stop here
            }

            // --- NORMAL BEHAVIOR (no shield) ---
            if (myMass > enemyMass)
            {
                Destroy(collision.gameObject);
                Grow();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void Grow()
    {
        float growAmount = growMultiplier * 0.1f;
        transform.localScale += new Vector3(growAmount, growAmount, 0f);

        // Also increase mass
        if (rb != null)
        {
            rb.mass += massPerGrowth * growMultiplier;
        }
    }
}
