using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Debug.unityLogger.Log("Mouse Down");
    //         Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity);
    //         if (hit.collider != null)
    //         {
    //             Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), hit.transform.position, Color.red, 2f);
    //             GetFurniture(hit.collider.gameObject);
    //         }
    //     }
    // }
    //
    // void GetFurniture(GameObject furniture)// {
    //     TryGetComponent(typeof(Furniture), out Component furnitureComponent);
    //     if (furnitureComponent != null)
    //
    // }
}
