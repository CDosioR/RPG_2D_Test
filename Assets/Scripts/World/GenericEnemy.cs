using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{

    private Animator animator;

    public float maxHealth;
    public float currentHealth;

    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        currentHealth = maxHealth;
        isMoving = false;

    }

    // Update is called once per frame
    void Update()
    {

        animator.SetBool("moving", isMoving);

        HandleHealth();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("hurt");
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(50f, 50f));
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

        StartCoroutine(BlinkColorOnDamageTaken());

    }

    private void HandleHealth()
    {
        if(currentHealth <= 0f)
        {
            Kill();
        }
    }

    private IEnumerator BlinkColorOnDamageTaken()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red; //FF4F4F
         yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        /*yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;*/

    }

    public void Kill()
    {
        currentHealth = 0;
        animator.SetTrigger("die");
        Invoke("DestroyGenericEnemy", 1.2f);
    }

    private void DestroyGenericEnemy()
    {
        Destroy(gameObject);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50f, 50f));
        }
    }*/


}
