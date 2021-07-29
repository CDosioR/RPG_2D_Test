using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Vector3 pos1, pos2;
    public float speed;
    public Vector3 startPos;

    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pos1)
        {
            Debug.Log("position es igual a START");
            nextPos = pos2;
        }

        if (transform.position == pos2)
        {
            Debug.Log("position es igual a END");
            nextPos = pos1;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1, pos2);
    }
}
