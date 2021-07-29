using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    Animator animator;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;

    //Health variables
    private float maxHealth = 100;
    private float currentHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        healthBar = FindObjectOfType<HealthBar>();

        this.currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        healthBar.setHealth(this.currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        HandleHealth();

        if (Input.GetKeyDown("x"))
        {
            TakeDamage(this.name, 20f);
        }
    }

    private void HandleHealth()
    {
        if (this.currentHealth <= 0f)
        {
            Kill(this.name);
            StartCoroutine(Respawn());
        }
    }

    public void TakeDamage(string source, float damage)
    {
        Debug.Log("Damage taken " + damage + " from " + source);
        this.currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }

    public void Kill(string source)
    {
        this.currentHealth = 0f;
        healthBar.setHealth(0f);
        animator.SetTrigger("die");
        Debug.Log("Killed by "+source);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
        LevelManagerController.instance.Respawn();
    }

}
