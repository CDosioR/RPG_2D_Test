using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    //Components
    Animator animator;
    Rigidbody2D rigidBody2D;


    //Combat variables
    public Transform firePosition;
    public GameObject fireBall;
    public float fireBallCooldown = 5f;
    private bool onCooldown;
    public static bool isBlowing = false;

    public Transform attackPoint;
    public Transform attackPointFlipX;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackDamage;

    // Start is called before the first frame update
    void Start()
    {

        animator = gameObject.GetComponent<Animator>();
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();

        onCooldown = false;

    }

    // Update is called once per frame
    void Update()
    {

        HandleAttack();
        HandleFireBall();
        HandleBlock();

    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");

            Vector2 attackPosition = PlayerMovement.direction == 1 ? attackPointFlipX.position : attackPoint.position;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayers);

            //Damage them

            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit " + enemy.name);

                enemy.gameObject.GetComponent<GenericEnemy>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPointFlipX.position, attackRange);
    }

    private void HandleFireBall()
    {
        if (Input.GetMouseButtonDown(2) && CheckGround.isGrounded)
        {

            if (!onCooldown)
            {
                animator.SetTrigger("cast");
                //Wait for animation to play
                onCooldown = true;
                Invoke("CastFireball", 0.3f);
            }

        }
    }

    private void CastFireball()
    {        
        Instantiate(fireBall, firePosition.position, firePosition.rotation);
        CooldownStart();
    }

    public void CooldownStart()
    {
        StartCoroutine(CooldownCoroutine());
    }
    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(fireBallCooldown);
        onCooldown = false;
    }

    private void HandleBlock()
    {
        if (CheckGround.isGrounded)
        {
            if (Input.GetMouseButtonDown(1))
            {
                rigidBody2D.velocity = Vector2.zero;
                isBlowing = true;
                //PlayerMovement.SetCanJump(false);
                PlayerMovement.SetCanMove(false);
                animator.SetBool("moving", false);
            }

            if (Input.GetMouseButtonUp(1))
            {
                isBlowing = false;
                //PlayerMovement.SetCanJump(true);
                PlayerMovement.SetCanMove(true);
            }

            animator.SetBool("isBlowing", isBlowing);
        }
    }
}
