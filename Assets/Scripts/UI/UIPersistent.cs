using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPersistent : MonoBehaviour
{
    private void Awake() => DontDestroyOnLoad(gameObject);
}
