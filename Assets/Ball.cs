using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 8f;
    private Vector2 direction;
    public float boundaryY = 4.5f;

    void Start()
    {
        // Start moving in a random direction
        direction = new Vector2(Random.value > 0.5f ? 1 : -1, Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos += (Vector3)direction * speed * Time.deltaTime;

        // Bounce off top and bottom
        if (pos.y > boundaryY)
        {
            pos.y = boundaryY;
            direction.y = -direction.y;
        }
        else if (pos.y < -boundaryY)
        {
            pos.y = -boundaryY;
            direction.y = -direction.y;
        }

        transform.position = pos;
    }
} 