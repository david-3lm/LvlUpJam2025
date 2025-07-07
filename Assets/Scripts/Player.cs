using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space) && speed == 0f)
        //    IncreaseSpeed();
        //else if (Input.GetKeyDown(KeyCode.Space) && speed >= 1f)
        //    Stop();
        
        //if (Input.GetKeyDown(KeyCode.D))
        //    Rotate(90);
        //else if (Input.GetKeyDown(KeyCode.A))
        //    Rotate(-90);
        
        if (speed < 1f)
            return;
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    public void Run()
    {
        Debug.Log("Run");
        IncreaseSpeed();
        //transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    public void IncreaseSpeed()
    {
        Debug.Log("IncreaseSpeed");
        speed = 2f;
    }

    public void Stop()
    {
        Debug.Log("stop");
        speed = 0f;
    }

    //ROTATE RIGHT 90 ROTATE LEFT -90
    void Rotate(float angle)
    {
        transform.RotateAround(transform.position, Vector3.up, angle);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("choque con " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("right"))
            Rotate(90);
        else if (collision.gameObject.CompareTag("left"))
            Rotate(-90);
    }
}
