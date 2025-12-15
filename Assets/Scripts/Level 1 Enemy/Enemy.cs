
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
    public float detectionRange = 20f;
    public float attackRange = 20f;
    public float moveSpeed = 2f;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f;
    private Shoot shootScript;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        originalScale = transform.localScale;
        shootScript = GetComponentInChildren<Shoot>();
    }

    void Update()
    {
        if (isDead) return;

        // flip();
        DetectAndAttackPlayer();
    }

    void flip()
    {

        // if (IsPlayerInRange()) return;


        // if (firePoint != null)
        // {
        //     float x = Mathf.Abs(firePoint.localPosition.x);
        //     firePoint.localPosition = new Vector3(isFlipped ? -x : x, firePoint.localPosition.y, firePoint.localPosition.z);
        // }

        // if (!isFiring)
        // {
        //     flipTimer += Time.deltaTime;
        //     if (flipTimer >= 3f)
        //     {
        //         SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //         isFlipped = !isFlipped;
        //         spriteRenderer.flipX = isFlipped;
        //         flipTimer = 0f;
        //     }
        // }

        if (IsPlayerInRange()) return;

        if (!isFiring)
        {
            flipTimer += Time.deltaTime;
            if (flipTimer >= 3f)
            {
                isFlipped = !isFlipped;
                // Use localScale instead of flipX to be consistent
                transform.localScale = new Vector3(isFlipped ? -originalScale.x : originalScale.x, originalScale.y, originalScale.z);
                flipTimer = 0f;
            }
        }

        // Update firePoint based on current scale
        if (firePoint != null)
        {
            float x = Mathf.Abs(firePoint.localPosition.x);
            firePoint.localPosition = new Vector3(transform.localScale.x < 0 ? -x : x, firePoint.localPosition.y, firePoint.localPosition.z);
        }

    }

    bool IsPlayerInRange()
    {
        if (player == null) return false;

        float distance = PlayerDisTance();
        return distance <= detectionRange;
    }

    float PlayerDisTance()
    {
        if (player == null) return Mathf.Infinity;

        return Vector2.Distance(transform.position, player.position);
    }

    void DetectAndAttackPlayer()

    {

        if (!player) return;

        float distance = PlayerDisTance();

        if (IsPlayerInRange())
        {

            if (player.position.x < transform.position.x)
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            else
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);




            if (distance <= attackRange)
            {

                TryAttack();

            }


        }

    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            shootScript.StartAudioFire();
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
