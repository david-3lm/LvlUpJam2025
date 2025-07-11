using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapChecker : MonoBehaviour
{
    private BoxCollider collider;
    public MeshRenderer mesh;
    public Material[] materialArray;
    
    [SerializeField] private Material good;
    [SerializeField] private Material bad;
    [SerializeField] private Material empty;
    
    
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
        ChangeToEmpty();
    }

    public bool CheckOverlap()
    {
        Vector3 center = transform.position + transform.rotation * collider.center;
        Vector3 halfExtents = Vector3.Scale(collider.size * 0.5f, transform.lossyScale);
        LayerMask mask = LayerMask.GetMask("Ignore Raycast", "Suciedad");
        Collider[] overlaps = Physics.OverlapBox(center, halfExtents, transform.rotation, mask);
        foreach (var col in overlaps)
        {
            if (col.gameObject != gameObject && !col.gameObject.CompareTag("Floor"))
            {
                ChangeToBad();
                Debug.Log("Hay overlap con"+ col.gameObject.name);
                return true;
            }
        }
        ChangeToGood();
        return false;
    }

    private void ChangeToGood()
    {
        materialArray = mesh.materials;
        materialArray[0] = good;
        materialArray[0].color = good.color;
        mesh.materials = materialArray;
    }

    private void ChangeToBad()
    {
        materialArray = mesh.materials;
        materialArray[0] = bad;
        materialArray[0].color = bad.color;
        mesh.materials = materialArray;
    }

    public void ChangeToEmpty()
    {
        materialArray = mesh.materials;
        materialArray[0] = empty;
        materialArray[0].color = empty.color;
        mesh.materials = materialArray;
    }
    
    private void OnDrawGizmos()
    {
        Vector3 center = transform.position + transform.rotation * collider.center;
        Vector3 halfExtents = Vector3.Scale(collider.size * 0.5f, transform.lossyScale);

        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(center, transform.rotation, Vector3.one);
        Gizmos.DrawWireSphere(center, 1f);
        Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2f); // halfExtents * 2 = full size
    }
}
