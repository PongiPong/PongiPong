using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed = 10f;
    public Rigidbody2D rb;
    private float movement;
    public float yBoundary = 4f;

    [Header("Health & Animation")]
    public int maxHealth = 10;
    public int currentHealth;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        movement = 0f;
        
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.W)) movement = 1f;
            else if (Input.GetKey(KeyCode.S)) movement = -1f;
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow)) movement = 1f;
            else if (Input.GetKey(KeyCode.DownArrow)) movement = -1f;
        }
        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
        Vector2 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -yBoundary, yBoundary);
        transform.position = clampedPosition;
    }

    public void PlayAttackAnimation()
    {
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;
        
        currentHealth -= damage;
        
        if (currentHealth > 0)
        {
            GameManager.Instance.PlaySound(GameManager.Instance.damageSound);
        }

        StartCoroutine(DamageFlashRoutine());

        if (currentHealth <= 0)
        {
            if (anim != null)
            {
                anim.SetTrigger("Death");
            }
        }
        else
        {
            if (anim != null)
            {
                anim.SetTrigger("Damaged");
            }
        }
    }
    private IEnumerator DamageFlashRoutine()
    {
        spriteRenderer.color = Color.red;
        
        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }
}