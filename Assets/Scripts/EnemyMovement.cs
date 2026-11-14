using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 3f;
    public int patrolDestination;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (patrolPoints.Length < 2) return;

        Transform target = patrolPoints[patrolDestination];

        // Calculate horizontal direction (-1 for left, 1 for right)
        float direction = Mathf.Sign(target.position.x - transform.position.x);

        // Move horizontally while keeping Unity's natural gravity on the Y axis
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        // Flip sprite to face the movement direction
        transform.localScale = new Vector3(direction, 1, 1);

        // Check only the horizontal (X) distance to the patrol point
        if (Mathf.Abs(transform.position.x - target.position.x) < 0.2f)
        {
            patrolDestination = (patrolDestination == 0) ? 1 : 0;
        }
    }
}
