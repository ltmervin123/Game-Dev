using UnityEngine;


public class PlayerMovement : MonoBehaviour
{   
    private Animator anim;
    private Rigidbody2D rb;
    private float speed = 3.5f;
    private float jumpForce = 6f;
    private bool isGrounded;
  



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        move();
        jump();
        Animation();

    }
        
    void Animation()
    {
        bool isMoving = rb.linearVelocity.x != 0;
        anim.SetBool("isMoving", isMoving);
    }

    void jump()
    {
        float currentYPosition = transform.position.y;
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetBool("isJumping", true);
        }
    }

    void move()
    {
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.linearVelocity.y);

        flip();
    }

    void flip()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            return;
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            return;
        }
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
  
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


}
