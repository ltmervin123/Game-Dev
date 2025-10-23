using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private float speed = 3f;
    private float jumpForce = 5f;
    private bool isGrounded;
    private bool isFiring; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        fire();      
        move();
        jump();
        Animation();
    }

    void Animation()
    {
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isJumping", !isGrounded);
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !isFiring)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    void fire()
    {
     
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isFiring)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetTrigger("isFiring");
            isFiring = true;

            //Animator childAnimator = GetComponentInChildren<Animator>();
            //AnimatorStateInfo stateInfo = childAnimator.GetCurrentAnimatorStateInfo(0);
            //float fireAnimLength = stateInfo.length;
            //Invoke(nameof(ResetFiring), fireAnimLength);
        }
    }

    void ResetFiring()
    {
        isFiring = false;
    }

    void move()
    {

        //if (isFiring) return;

        if (isFiring)
        {
            ResetFiring();
            return;
        }

        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.linearVelocity.y);
        flip();
    }

    void flip()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (Input.GetAxis("Horizontal") < 0)
            spriteRenderer.flipX = true;
        else if (Input.GetAxis("Horizontal") > 0)
            spriteRenderer.flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy"))
        {
            isGrounded = false;
        }
    }
}
