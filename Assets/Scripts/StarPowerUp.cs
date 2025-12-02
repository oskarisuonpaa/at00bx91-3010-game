using UnityEngine;

public class StarPowerUp : MonoBehaviour
{
    public float duration = 15f;
    public Sprite starSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCollision collision = other.GetComponent<PlayerCollision>();

        if (collision == null)
            return;

        // Aseta tuplakasvu
        collision.growMultiplier = 2f;

        // Näytä ikoni
        collision.ShowPowerupIcon(starSprite);

        // Ajasta efektin loppuminen
        collision.StartCoroutine(HideIconAfterDelay(collision, duration));

        Destroy(gameObject);
    }

    private System.Collections.IEnumerator HideIconAfterDelay(PlayerCollision col, float time)
    {
        yield return new WaitForSeconds(time);

        col.growMultiplier = 1f;

        col.HidePowerupIcon();
    }
}
