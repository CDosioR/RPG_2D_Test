using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public float projectileSpeed;
    public GameObject explosion;
    private Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        if (PlayerMovement.direction == 1)
        {
            spriteRenderer.flipX = true;
            rigidbody.velocity = transform.right * -projectileSpeed;

        } else {
            spriteRenderer.flipX = false;
            rigidbody.velocity = transform.right * projectileSpeed;
        }

        Invoke("DestroyFireball", 0.7f);

    }

    private void DestroyFireball()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("PassableGround"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Instantiate(explosion, transform.position, Quaternion.identity);

                collision.gameObject.GetComponent<GenericEnemy>().TakeDamage(damage);

                //Destroy(collision.gameObject);
                Destroy(gameObject);
            } else
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
        }
       
    }
}
