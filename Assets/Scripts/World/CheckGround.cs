using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{

    public static bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            PlayerMovement.SetCanJump(true);
            PlayerMovement.ResetJumpCount();

        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            isGrounded = true;
            PlayerMovement.SetCanJump(true);
            PlayerMovement.ResetJumpCount();
            gameObject.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, 500f));

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
