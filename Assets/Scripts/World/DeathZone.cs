using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    /*private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.Kill(this.name);
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<GenericEnemy>().Kill();
        }
    }
}
