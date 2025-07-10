using UnityEngine;

public class UIPersistent : MonoBehaviour
{
    private void Awake() => DontDestroyOnLoad(gameObject);
}
