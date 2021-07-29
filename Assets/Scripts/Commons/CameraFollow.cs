using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject objectToFollow;
    private System.Type type;

    public Vector2 startPosition, endPosition;

    private Vector2 currentPosition;


    // Start is called before the first frame update
    void Start()
    {
        type = objectToFollow.GetType();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (objectToFollow == null)
        {
            Object[] objs = FindObjectsOfType(type);
            objectToFollow = (GameObject) objs[0];
        }

        HandleCameraFollow();

    }

    private void HandleCameraFollow()
    {
    
        float posX = objectToFollow.transform.position.x;
        float posY = objectToFollow.transform.position.y;

        transform.position = new Vector3(
            Mathf.Clamp(posX, startPosition.x, endPosition.x),
            Mathf.Clamp(posY, startPosition.y, endPosition.y),
            transform.position.z);

        currentPosition = transform.position;
    }

    public bool IsResetPosition()
    {
        if(currentPosition.x == startPosition.x)
        {
            return true;
        } else
        {
            return false;
        }
    }



}
