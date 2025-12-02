using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public float duration = 15f;
    public Sprite shieldSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCollision collision = other.GetComponent<PlayerCollision>();

        if (collision == null)
            return;

        // Aseta shield päälle
        collision.isShielded = true;

        // Näytä ikoni
        collision.ShowPowerupIcon(shieldSprite);

        // Ajasta efektin loppuminen
        collision.StartCoroutine(HideIconAfterDelay(collision, duration));

        Destroy(gameObject);
    }

    private System.Collections.IEnumerator HideIconAfterDelay(PlayerCollision col, float time)
    {
        yield return new WaitForSeconds(time);

        col.isShielded = false;

        col.HidePowerupIcon();
    }
}
