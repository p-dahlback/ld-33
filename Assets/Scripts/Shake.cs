using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {

	public float speed = 1.0f;
	public float amount = 1.0f;

	private Vector3 defaultPos;

	// Use this for initialization
	void Start () {
		defaultPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = defaultPos;
		Vector3 pos = transform.position;
		pos.x += amount * Mathf.Sin(Time.time * speed);
		pos.y += amount * Mathf.Cos(Time.time / 3 * speed);
		transform.position = pos;
	}
}
