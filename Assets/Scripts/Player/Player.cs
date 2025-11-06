using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Restart Settings")]
    public Restart restart;

    [Header("Player Settings")]
    public HealthBar healthBar;
    private float health = 100f;
    public float speed = 2.5f;
    public float jumpForce = 4.5f;
    private bool isGrounded;
    private bool isFiring;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireCooldown = 0.0f;
    private float nextFireTime = 0.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleFire();
        HandleMove();
        HandleJump();
        HandleAnimation();
    }

    void HandleAnimation()
    {
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isJumping", !isGrounded);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !isFiring)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void HandleFire()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            ShootBullet();
        }

    }

    void ShootBullet()
    {
        isFiring = true;
        anim.SetTrigger("isFiring");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        float dir = spriteRenderer.flipX ? -1f : 1f;
        bulletScript.SetDirection(new Vector2(dir, 0));
        bulletScript.speed = bulletSpeed;
        nextFireTime = Time.time + fireCooldown;
        Invoke(nameof(ResetFiring), 0.350f);
    }

    void ResetFiring()
    {
        anim.ResetTrigger("isFiring");
        isFiring = false;
    }



    void HandleMove()
    {
        if (isFiring) return;

        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput != 0)
        {
            bool facingLeft = moveInput < 0;
            spriteRenderer.flipX = facingLeft;


            if (firePoint != null)
            {
                float x = Mathf.Abs(firePoint.localPosition.x);
                firePoint.localPosition = new Vector3(facingLeft ? -x : x, firePoint.localPosition.y, firePoint.localPosition.z);
            }
        }
    }

    void hitDamage()
    {
        health -= 20f;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            die();
        }
    }

    void die()
    {

        restart.ShowRestartPanel();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitDamage();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy"))
        {
            isGrounded = true;
        }

        // if (collision.gameObject.CompareTag("LevelOneEndCheckPoint"))
        // {
        //     SceneManager.LoadScene("Level3");
        // }
    }

}


