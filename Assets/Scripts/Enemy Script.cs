using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float enemyDamage = 5f;
    public float widerTriggerDamage = 10f;
    public Collider2D enemyTrigger; // Assign ONLY the top stomp trigger here

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthScript playerHealth = collision.gameObject.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyDamage, this, enemyTrigger);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            // Check 1: Is it touching the top trigger?
            bool hitTopTrigger = enemyTrigger != null && other.IsTouching(enemyTrigger);
            // Check 2: Is the player actually above the enemy's center?
            bool isAboveEnemy = other.transform.position.y > transform.position.y + 0.2f;
            // Check 3: Is the player falling downward (not jumping up or walking sideways)?
            bool isFalling = playerRb != null && playerRb.linearVelocity.y < -0.1f;

            if (hitTopTrigger && isAboveEnemy && isFalling)
            {
                // Valid Goomba stomp!
                Destroy(gameObject);
            }
            else
            {
                // Player hit the wider side trigger: deal damage instead
                HealthScript playerHealth = other.GetComponent<HealthScript>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(widerTriggerDamage, this, enemyTrigger);
                }
            }
        }
    }
}
