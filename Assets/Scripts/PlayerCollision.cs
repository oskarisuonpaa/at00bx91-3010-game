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

    // audio effects
    public AudioSource audioSource;
    public AudioClip eatClip;
    public AudioClip powerUpClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.mass = baseMass;
        }
    }

    bool IsBiggerThan(Transform other)
    {
        return transform.localScale.x > other.localScale.x;
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
            audioSource.PlayOneShot(eatClip);
        }

        // sound effect for powerup
        if (other.CompareTag("Powerup")) 
        {
            audioSource.PlayOneShot(powerUpClip);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Use Rigidbody2D mass for comparison instead of scale
            // float myMass = rb != null ? rb.mass : transform.localScale.x;
            // float enemyMass =
            //     collision.rigidbody != null
            //         ? collision.rigidbody.mass
            //         : collision.transform.localScale.x;

            // --- SHIELD BEHAVIOR ---
            if (isShielded)
            {
                // if (myMass > enemyMass)
                if (IsBiggerThan(collision.transform))
                {
                    // Shield + Player is bigger -> eat enemy
                    Destroy(collision.gameObject);
                    Grow();
                    FindAnyObjectByType<GameOverManager>().GameOver(true); //voitto
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
            // if (myMass > enemyMass)
            if (IsBiggerThan(collision.transform))
            {
                Destroy(collision.gameObject);
                Grow();
                FindAnyObjectByType<GameOverManager>().GameOver(true); // voitto
            }
            else
            {
                Destroy(gameObject);
                FindAnyObjectByType<GameOverManager>().GameOver(false); // häviö
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
