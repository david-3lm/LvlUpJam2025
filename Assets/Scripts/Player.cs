using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed { get; set; }
    private bool isWaiting = false;
    
    private Animator animator;
    
    [SerializeField]private Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //if (Input.GetKeyDown(KeyCode.Space) && speed == 0f)
        //    IncreaseSpeed();
        //else if (Input.GetKeyDown(KeyCode.Space) && speed >= 1f)
        //    Stop();
        
        //if (Input.GetKeyDown(KeyCode.D))
        //    Rotate(90);
        //else if (Input.GetKeyDown(KeyCode.A))
        //    Rotate(-90);
        
        // if (speed < 1f)
        //     return;
        float gravity = rb.velocity.y;
        rb.velocity = transform.forward * speed;
        rb.velocity = new Vector3(rb.velocity.x, gravity, rb.velocity.z);
    }

    public void SwitchMovement()
    { 
        if (speed == 0f)
            Run();
        else
            Stop();
    }

    public void SwitchMovement(bool isMoving)
    {
        if (isMoving)
            Stop();
        else
            Run();
    }

    public void Run()
    {
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
        speed = 0f;
    }

    //ROTATE RIGHT 90 ROTATE LEFT -90
    void Rotate(float angle)
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
        var rot = transform.rotation.eulerAngles.y;

        transform.rotation = Quaternion.Euler(0, Mathf.Round(rot + angle), 0);
        // transform.RotateAround(transform.position, Vector3.up, transform.rotation.y + angle);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isWaiting || speed == 0f)
            return;

        if (collision.gameObject.CompareTag("right"))
        {
            if (!animator.GetBool("Crashed"))
                animator.SetBool("Crashed", true);
            Debug.Log("choque con " + collision.gameObject.name);
            Rotate(90);
            StartCoroutine(WaitBeforeNextCollision());
        }
        else if (collision.gameObject.CompareTag("left"))
        {
            if (!animator.GetBool("Crashed"))
                animator.SetBool("Crashed", true);
            Debug.Log("choque con " + collision.gameObject.name);
            Rotate(-90);
            StartCoroutine(WaitBeforeNextCollision());
        }
    }

    private IEnumerator WaitBeforeNextCollision()
    {
        isWaiting = true;
        yield return new WaitForSeconds(0.05f);
        isWaiting = false;
        animator.SetBool("Crashed", false);
    }
}
