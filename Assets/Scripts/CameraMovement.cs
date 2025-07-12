using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera cam;
    Plane []planes;
    GameObject playerObject;
    public Vector3 offset = new Vector3(8, 11, -8);
    public float smoothSpeed = 0.125f;
    Collider objCollider;
    bool firstFrame = true;

    [SerializeField] private bool followPlayer = false;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        // transform.position = new Vector3(playerObject.transform.position.x - 7f, transform.position.y, playerObject.transform.position.z - 9f);
        cam = Camera.main;
        objCollider =  playerObject.GetComponent<Collider>();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            MoveCamera(-0.1f, 0.1f);
        if (Input.GetKey(KeyCode.S))
            MoveCamera(0.1f, -0.1f);
        if (Input.GetKey(KeyCode.A))
            MoveCamera(-0.1f, -0.1f);
        if (Input.GetKey(KeyCode.D))
            MoveCamera(0.1f, 0.1f);
    }
    
    void LateUpdate()
    {
        if (followPlayer)
        {
            Vector3 desiredPosition = playerObject.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            if (playerObject.GetComponent<Player>().speed == 0 && firstFrame)
            {
                transform.position = desiredPosition;
                firstFrame = false;
            }
            else if (playerObject.GetComponent<Player>().speed == 0)
                return;
            transform.position = smoothedPosition;
        }
    }

    void MoveCamera(float speedX, float speedY)
    {
        transform.position += new Vector3(speedX, 0f, speedY);

    }
}
