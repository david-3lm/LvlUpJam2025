using System;
using System.Collections.Generic;
using System.Linq;
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
    public bool isDecoration = false;

    private Transform furnitureParent;
    
    private LevelManager levelManager;
    
    List<OverlapChecker> checkers;
    private void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        furnitureParent = transform.parent;
        startPosition = transform.localPosition;
        levelManager = FindObjectOfType<LevelManager>();
        checkers = GetComponentsInChildren<OverlapChecker>().ToList();
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
        CheckOverlap();
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
        dragPlane = new Plane(Vector3.up, Vector3.up); // XZ plane at object's current Y
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

        foreach (var ch in checkers)
        {
            if (ch.CheckOverlap())
                return true;
        }
        return false;
    }

    private void EmptyChecker()
    {
        foreach (var ch in checkers)
        {
            ch.ChangeToEmpty();
        }
    }

    private bool IsInsideMap()
    {
        int rows = levelManager.currentLevel.rows;
        int cols = levelManager.currentLevel.cols;
        if (transform.position.x > rows || transform.position.x < 0f ||
            transform.position.z > cols || transform.position.z < 0)
        {
            return false;
        }
        return true;
    }

    private void OnMouseUp()
    {
        if ((!isDecoration && CheckOverlap()) || !IsInsideMap())
        {
            transform.SetParent(furnitureParent);
            transform.localPosition = startPosition;
        }
        EmptyChecker();
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