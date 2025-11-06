
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private Color originalColor;
    public float flashDuration = 0.1f;


    private float flipTimer = 0f;
    private bool isFlipped = true;
    private bool isDead = false;

    private bool isFiring = false;
    private float health = 5f;

    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireCooldown = 0.0f;
    private float nextFireTime = 0.5f;

    [Header("Player Detection")]
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 5f;
    public float moveSpeed = 2f;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isDead) return;

        // flip();
        DetectAndAttackPlayer();
    }

    void flip()
    {


        if (firePoint != null)
        {
            float x = Mathf.Abs(firePoint.localPosition.x);
            firePoint.localPosition = new Vector3(isFlipped ? -x : x, firePoint.localPosition.y, firePoint.localPosition.z);
        }

        if (!isFiring)
        {
            flipTimer += Time.deltaTime;
            if (flipTimer >= 3f)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                isFlipped = !isFlipped;
                spriteRenderer.flipX = isFlipped;
                flipTimer = 0f;
            }
        }

    }

    void DetectAndAttackPlayer()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {

            if (player.position.x < transform.position.x)
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            else
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);




            if (distance <= attackRange)
            {

                TryAttack();

            }
            else
            {
                // Move towards player
                Debug.Log("Enemy moving towards player");
            }

        }
        else
        {

            // Idle state
            Debug.Log("Enemy idle");
        }
    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        isFiring = true;
        anim.SetTrigger("attack");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        float dir = transform.localScale.x < 0 ? -1f : 1f;
        bulletScript.SetDirection(new Vector2(dir, 0));
        bulletScript.speed = bulletSpeed;
        nextFireTime = Time.time + fireCooldown;
        Invoke(nameof(ResetFiring), 0.350f);
    }

    void ResetFiring()
    {
        anim.ResetTrigger("attack");
        isFiring = false;
    }

    public void HitFlash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine());
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HitFlash();
            OnBulletHit();
        }
    }

    private void OnBulletHit()
    {
        health -= 1f;

        if (health <= 0 && !isDead)
        {
            isDead = true;
            anim.SetTrigger("die");
            Invoke(nameof(enemyDeath), 1.5f);
        }
    }

    private void enemyDeath()
    {
        Destroy(gameObject);
    }
}
