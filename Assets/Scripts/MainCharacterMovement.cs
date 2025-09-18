using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 10f;
    private bool isGrounded;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        move();
        jump();

    }

    void jump()
    {
        float currentYPosition = transform.position.y;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
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
