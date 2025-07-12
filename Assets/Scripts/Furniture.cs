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
    public bool clicked = true;

    private Transform furnitureParent;
    
    private LevelManager levelManager;
    private FurnitureButton button;
    
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
            if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1))
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
        if (Input.GetMouseButtonDown(0) && isSelected && clicked)
            PutFurniture();
        else if (!clicked)
            clicked = true;
    }
    
    public void SelectFurniture(FurnitureButton b)
    {
        button = b;
        if (isDecoration || isPlaying)
            return;
        if (levelManager == null)
            StartManual();
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

    private void PutFurniture()
    {
        if ((!isDecoration && CheckOverlap()) || !IsInsideMap())
        {
            WrongPosition();
        }
        EmptyChecker();
        isSelected = false;
        clicked = false;
    }

    // private void OnMouseUp()
    // {
    //     if ((!isDecoration && CheckOverlap()) || !IsInsideMap())
    //     {
    //         WrongPosition();
    //     }
    //     EmptyChecker();
    //     isSelected = false;
    // }
    //
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

    private void WrongPosition()
    {
        if (!isSelected)
            return;
        transform.SetParent(furnitureParent);
        transform.localPosition = startPosition;
        button.Wrong();
    }

    void TogglePlay()
    {
        // if (isPlaying && playerScript.speed == 0)
        // {
        //     isPlaying = false;
        // }
        // else 
        if (!isPlaying && playerScript.speed != 0)// && IsInsideMap())
        {
            isPlaying = true;
        }
    }

    public void StartManual()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.gameObject.GetComponent<Player>();
        furnitureParent = transform.parent;
        startPosition = transform.localPosition;
        levelManager = FindObjectOfType<LevelManager>();
        checkers = GetComponentsInChildren<OverlapChecker>().ToList();
    }
}