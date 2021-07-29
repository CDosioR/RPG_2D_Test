using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidPushEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
    }
}
