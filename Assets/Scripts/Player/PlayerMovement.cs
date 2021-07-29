using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Constants
    private static int MAX_JUMP_COUNT = 2;

    //Components
    Animator animator;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;

    //Movement variables
    private static bool canJump;
    private static bool canMove = true;
    private static int jumpCount;
    public static int direction;
    public float speed;
    private float movX;

    //Dash variables
    public float dashForce;
    public float startDashTimer;
    float currentDashTimer;
    float dashDirection;
    bool isDashing;

    public Transform dashEffectPoint;
    public GameObject dashEffect;

    // Start is called before the first frame update
    void Start()
    {

        animator = gameObject.GetComponent<Animator>();
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        jumpCount = MAX_JUMP_COUNT;
        direction = 2;//Default direction is right

    }

    void Update()
    {
        movX = Input.GetAxis("Horizontal");

        if (canMove)
        {
            rigidBody2D.velocity = new Vector2(movX * speed, rigidBody2D.velocity.y);
        }

        animator.SetBool("isGrounded", CheckGround.isGrounded);

        HandleMovement();
        HandleJump();
        HandleDash();
    }

    public void HandleMovement()
    {
        if (canMove)
        {
            if (Input.GetKey("left") || Input.GetKey("a"))
            {
                animator.SetBool("moving", true);
                direction = 1;
                if (CheckGround.isGrounded)
                {
                    rigidBody2D.AddForce(new Vector2(-700f * Time.deltaTime, 0));
                }
                else
                {
                    rigidBody2D.AddForce(new Vector2(-300f * Time.deltaTime, 0));
                }

                spriteRenderer.flipX = true;
            }
            else if (Input.GetKey("right") || Input.GetKey("d"))
            {
                animator.SetBool("moving", true);
                direction = 2;
                if (CheckGround.isGrounded)
                {
                    rigidBody2D.AddForce(new Vector2(700f * Time.deltaTime, 0));
                }
                else
                {
                    rigidBody2D.AddForce(new Vector2(300f * Time.deltaTime, 0));
                }

                spriteRenderer.flipX = false;
            }
            else if (Input.GetKeyUp("left") || Input.GetKeyUp("a"))
            {
                animator.SetBool("moving", false);
            }
            else if (Input.GetKeyUp("right") || Input.GetKeyUp("d"))
            {
                animator.SetBool("moving", false);
            }
            /*else
            {
                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
            }*/
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown("space"))
        {

            if (canJump && !PlayerCombat.isBlowing)
            {
                float jumpForce = jumpCount > 1 ? 250f : 300f;
                animator.SetTrigger("jump");
                rigidBody2D.AddForce(new Vector2(0, jumpForce));
                animator.SetBool("isGrounded", CheckGround.isGrounded);
                jumpCount--;
                Debug.Log("JumpCount es: " + jumpCount);
                if (jumpCount > 0)
                {
                    canJump = true;
                }
                else
                {
                    canJump = false;
                }
            }

        }
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && CheckGround.isGrounded && rigidBody2D.velocity.x != 0)
        {
            if(direction == 1)
            {
                dashEffect.GetComponent<SpriteRenderer>().flipX = true;
            } else
            {
                dashEffect.GetComponent<SpriteRenderer>().flipX = false;
            }
            Instantiate(dashEffect, dashEffectPoint.position, Quaternion.identity);
            isDashing = true;
            currentDashTimer = startDashTimer;
            rigidBody2D.velocity = Vector2.zero;
            dashDirection = movX;

            animator.SetTrigger("dash");
        }
        if (isDashing)
        {
            rigidBody2D.velocity = transform.right * dashDirection * dashForce;

            currentDashTimer -= Time.deltaTime;

            if (currentDashTimer <= 0)
            {
                isDashing = false;
            }
        }
    }

    public static void SetCanJump(bool value)
    {
        canJump = value;
    }

    public static void SetCanMove(bool value)
    {
        canMove = value;
    }

    public static void ResetJumpCount()
    {
        jumpCount = MAX_JUMP_COUNT;
    }
}
