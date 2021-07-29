using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerController : MonoBehaviour
{
    public static LevelManagerController instance;

    public Transform respawnPoint;
    public GameObject playerPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void Respawn()
    {
        Debug.Log("Respawning at position: "+"x"+ respawnPoint.position.x + " y"+ respawnPoint.position.y);
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }
}
