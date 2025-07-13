using UnityEngine;

public class BtnBackroundMovement : MonoBehaviour
{
	[SerializeField] private float speed = 5f;

	private Vector3 _startLocalPosition;

	private void Awake()
	{
		_startLocalPosition = transform.localPosition;
		Debug.Log("Saved local start position: " + _startLocalPosition);
	}

	private void OnEnable()
	{
		transform.localPosition = _startLocalPosition;
	}

	void Update()
	{
		transform.Translate(-Vector3.right * speed * Time.deltaTime, Space.Self);
	}
}

