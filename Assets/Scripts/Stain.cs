using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stain : MonoBehaviour
{
    
    //NO FUNCIONMA
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("mori");
        Destroy(this.gameObject);
    }
}
