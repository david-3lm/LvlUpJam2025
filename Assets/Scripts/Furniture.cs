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
    public bool isDecoration = false;

    private void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.localPosition;
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
        if (isDecoration)
            return;
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
    
    private bool CheckOverlap()
    {
        Vector3 center = transform.position + transform.rotation * GetComponent<BoxCollider>().center;
        Vector3 halfExtents = GetComponent<BoxCollider>().size * 0.5f;
        Collider[] overlaps = Physics.OverlapBox(center, halfExtents, transform.rotation);
        foreach (var col in overlaps)
        {
            if (col.gameObject != gameObject && !col.gameObject.CompareTag("Floor"))
            {
                Debug.Log("Hay overlap con"+ col.gameObject.name);
                return true;
            }
        }
        return false;
    }

    private void OnMouseUp()
    {
        if (!isDecoration && CheckOverlap())
        {
            transform.SetParent(cam.gameObject.transform);
            transform.localPosition = startPosition;
        }
        isSelected = false;
    }
    
    private void OnDrawGizmos()
    {
        if (!isSelected) return;

        BoxCollider box = GetComponent<BoxCollider>();
        Vector3 center = transform.position + transform.rotation * GetComponent<BoxCollider>().center;
        Vector3 halfExtents = Vector3.Scale(box.size * 0.5f, transform.lossyScale);

        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(center, transform.rotation, Vector3.one);
        Gizmos.DrawWireSphere(center, 1f);
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2f); // halfExtents * 2 = full size
    }
}