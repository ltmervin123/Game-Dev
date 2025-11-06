using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float maxRange = 1f;
    public float lifetime = 1f;
    private Vector2 direction;
    private Vector2 startPosition;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {

        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(startPosition, transform.position) >= maxRange) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Obstacle") || collision.CompareTag("Enemy") || collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

    }


}

