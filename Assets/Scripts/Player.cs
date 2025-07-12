using System;
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
    
    float counter;
    public bool gameStarted = false;
    

    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gameStarted && rb.velocity.magnitude <= 1f)
            counter += Time.deltaTime;
        else
            counter = 0f;
        if (counter >= 2f)
        {
            counter = 0f;
            gameStarted = false;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        Stop();
        animator.SetTrigger("GameOver");
        transform.Translate(Vector3.up * 1);
        rb.AddForce(0,10f,0,ForceMode.Impulse);
        yield return new WaitForSeconds(3f);
        Stop();
        FindObjectOfType<LevelManager>().ReloadLevel();
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
        if (speed == 0f || speed == 4f)
            Run();
        else if (speed == 2f)
            DoubleSpeed();
    }

    public void SwitchMovement(bool isMoving)
    {
        if (isMoving && speed != 4f)
            DoubleSpeed();
        else
            Run();
    }

    public void Run()
    {
        if (!gameStarted)
            gameStarted = true;
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

    public void DoubleSpeed()
    {
        speed = 4f;
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
