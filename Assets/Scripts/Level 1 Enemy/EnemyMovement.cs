using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private float flipTimer = 0f;
    private bool isFlipped = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
       
    }

    void Update()
    {
        flip();
        //fire();
        //move();
        //jump();
        //Animation();

    }

    //void Animation()
    //{
    //    bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
    //    anim.SetBool("isMoving", isMoving);
    //    anim.SetBool("isJumping", !isGrounded);
    //}

    //void jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.W) && isGrounded && !isFiring)
    //    {
    //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    //        isGrounded = false;
    //    }
    //}

    //void fire()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isFiring)
    //    {
    //        isFiring = true;
    //        anim.SetTrigger("isFiring");
    //        float fireAnimLength = anim.GetCurrentAnimatorStateInfo(0).length;
    //        Invoke(nameof(ResetFiring), fireAnimLength);
    //    }
    //}

    //void ResetFiring()
    //{
    //    isFiring = false;
    //}

    //void move()
    //{
    //    if (isFiring) return;

    //    rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.linearVelocity.y);
    //    flip();
    //}

    void flip()
    {
        flipTimer += Time.deltaTime;
        if (flipTimer >= 3f)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            isFlipped = !isFlipped;
            spriteRenderer.flipX = isFlipped;
            flipTimer = 0f; 
        }
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.flipX = true;
        //if (Input.GetAxis("Horizontal") < 0)
        //    spriteRenderer.flipX = true;
        //else if (Input.GetAxis("Horizontal") > 0)
        //    spriteRenderer.flipX = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //    }
    //}
}