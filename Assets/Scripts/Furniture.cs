using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class Furniture : MonoBehaviour
{
    private bool isSelected;
    private Plane dragPlane;
    private Camera cam;
    private Vector3 offset;

    private Vector3 startPosition;
    private Transform player;
    private bool badPosition = false;

    private void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
    }

    private void Update()
    {
        if (isSelected)
        {
            MoveWithMouse();
            if (Input.GetKeyDown(KeyCode.R))
                Rotate(-90);
        }
    }
    
    void Rotate(float angle)
    {
        transform.RotateAround(transform.position, Vector3.up, angle);
    }

    void MoveWithMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float enter = 0f;

        if (dragPlane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            transform.position = hitPoint + offset;
            transform.position = new Vector3((float)Math.Round(transform.position.x), 0f, (float)Math.Round(transform.position.z));
        }
    }

    private void OnMouseDown()
    {
        transform.SetParent(null);
        isSelected = true;
        dragPlane = new Plane(Vector3.up, Vector3.zero); // XZ plane at object's current Y
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float enter;
        if (dragPlane.Raycast(ray, out enter))
        {
            transform.position = ray.GetPoint(enter);
            offset = transform.position - ray.GetPoint(enter);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Furniture>(out Furniture collider))
        {
            badPosition = true;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent<Furniture>(out Furniture collider))
        {
            badPosition = false;
        }
    }


    private void OnMouseUp()
    {
        if (badPosition)
        {
            transform.SetParent(cam.gameObject.transform);
            transform.position = startPosition;
        }
        isSelected = false;
    }
}