using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
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
            float mySize = transform.localScale.x;
            float enemySize = collision.transform.localScale.x;

            if (mySize > enemySize)
            {
                Destroy(collision.gameObject);
                Grow();
            }
            else
            {
                Destroy(gameObject); // player gets eaten
            }
        }
    }

    void Grow()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0f);
    }
}