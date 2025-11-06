using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private float flipTimer = 0f;
    private bool isFlipped = true;
    private bool isDead = false;
    private float health = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        flip();


    }



    void flip()
    {
        if (isDead) return;

        flipTimer += Time.deltaTime;
        if (flipTimer >= 3f)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            isFlipped = !isFlipped;
            spriteRenderer.flipX = isFlipped;
            flipTimer = 0f;
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Bullet"))
        {
            OnBulletHit();
        }

    }

    private void OnBulletHit()
    {

        if (health == 0 && !isDead)
        {
            isDead = true;
            anim.SetTrigger("die");
            Invoke(nameof(enemyDeath), 1.5f);
            return;
        }

        health -= 1f;

    }

    private void enemyDeath()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            OnBulletHit();
        }
    }
}