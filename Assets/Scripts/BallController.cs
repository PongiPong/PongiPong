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
        
        direction = new Vector2(Random.value > 0.5f ? 1 : -1, Random.Range(-1f, 1f)).normalized;
        rb.velocity = direction * speed;
    }

    void Update()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (transform.position.y > boundaryY)
        {
            if (GameManager.Instance != null && GameManager.Instance.wallBounceSound != null)
                GameManager.Instance.PlaySound(GameManager.Instance.wallBounceSound);
            Vector2 velocity = rb.velocity;
            velocity.y = -Mathf.Abs(velocity.y);
            rb.velocity = velocity;
            transform.position = new Vector3(transform.position.x, boundaryY, transform.position.z);
        }
        else if (transform.position.y < -boundaryY)
        {
            if (GameManager.Instance != null && GameManager.Instance.wallBounceSound != null)
                GameManager.Instance.PlaySound(GameManager.Instance.wallBounceSound);
            Vector2 velocity = rb.velocity;
            velocity.y = Mathf.Abs(velocity.y);
            rb.velocity = velocity;
            transform.position = new Vector3(transform.position.x, -boundaryY, transform.position.z);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (GameManager.Instance != null && GameManager.Instance.attackSound != null)
                GameManager.Instance.PlaySound(GameManager.Instance.attackSound);
            
            Paddle paddle = collision.gameObject.GetComponent<Paddle>();
            if (paddle != null)
            {
                paddle.PlayAttackAnimation();
            }

            Vector2 velocity = rb.velocity;
            velocity.x = -velocity.x;
            rb.velocity = velocity;
        }
    }
}