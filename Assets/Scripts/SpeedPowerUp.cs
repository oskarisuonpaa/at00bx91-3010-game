using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float percent = 0.30f;
    public float duration = 15f;
    public Sprite speedSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement movement = other.GetComponent<PlayerMovement>();
        PlayerCollision collision = other.GetComponent<PlayerCollision>();

        if (movement == null || collision == null)
            return;

        // Näytä ikoni PlayerCollisionin kautta
        collision.ShowPowerupIcon(speedSprite);

        // Käynnistä speed boost
        movement.StartCoroutine(movement.ActivateSpeedBoost(percent, duration));

        // Piilota ikoni kun efekti loppuu
        collision.StartCoroutine(HideIconAfterDelay(collision, duration));

        Destroy(gameObject);
    }

    private System.Collections.IEnumerator HideIconAfterDelay(PlayerCollision col, float time)
    {
        yield return new WaitForSeconds(time);
        col.HidePowerupIcon();
    }
}
