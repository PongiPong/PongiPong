using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 8f;
    private Vector2 direction;
    public float boundaryY = 4.5f;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        
        // Start moving in a random direction
        direction = new Vector2(Random.value > 0.5f ? 1 : -1, Random.Range(-1f, 1f)).normalized;
        rb.velocity = direction * speed;
    }

    void Update()
    {
        // Bounce off top and bottom boundaries
        if (transform.position.y > boundaryY)
        {
            Vector2 velocity = rb.velocity;
            velocity.y = -Mathf.Abs(velocity.y);
            rb.velocity = velocity;
            transform.position = new Vector3(transform.position.x, boundaryY, transform.position.z);
        }
        else if (transform.position.y < -boundaryY)
        {
            Vector2 velocity = rb.velocity;
            velocity.y = Mathf.Abs(velocity.y);
            rb.velocity = velocity;
            transform.position = new Vector3(transform.position.x, -boundaryY, transform.position.z);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Bounce off paddles
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Vector2 velocity = rb.velocity;
            velocity.x = -velocity.x; // Reverse horizontal direction
            rb.velocity = velocity;
        }
    }
} 