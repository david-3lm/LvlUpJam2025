using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Furniture : MonoBehaviour
{
    private bool isSelected;
    private Plane dragPlane;
    private Camera cam;
    private Vector3 offset;

    private void Start()
    {
        cam = Camera.main;
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
            transform.position = new Vector3((float)Math.Round(transform.position.x), transform.position.y, (float)Math.Round(transform.position.z));
        }
    }

    private void OnMouseDown()
    {
        isSelected = true;
        dragPlane = new Plane(Vector3.up, transform.position); // XZ plane at object's current Y
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float enter;
        if (dragPlane.Raycast(ray, out enter))
        {
            offset = transform.position - ray.GetPoint(enter);
        }
    }

    private void OnMouseUp()
    {
        isSelected = false;
    }
}