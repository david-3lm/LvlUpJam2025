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
    private Player playerScript;
    public bool isDecoration = false;
    public bool isPlaying = false;

    private Transform furnitureParent;
    
    private LevelManager levelManager;
    
    List<OverlapChecker> checkers;
    private void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.gameObject.GetComponent<Player>();
        furnitureParent = transform.parent;
        startPosition = transform.localPosition;
        levelManager = FindObjectOfType<LevelManager>();
        checkers = GetComponentsInChildren<OverlapChecker>().ToList();
    }

    private void Update()
    {
        TogglePlay();
        if (isSelected && !isPlaying)
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
        if (isDecoration || isPlaying)
            return;
        transform.SetParent(levelManager.furnitureRoot);
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

    void TogglePlay()
    {
        if (isPlaying && playerScript.speed == 0)
        {
            isPlaying = false;
        }
        else if (!isPlaying && playerScript.speed != 0)
        {
            isPlaying = true;
        }
    }
    
}