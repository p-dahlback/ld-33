using UnityEngine;
using System.Collections;

public class KillPlane : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collider)
	{
//		Debug.Log("Hit kill plane, destroying");
		Destroy (collider.gameObject);
	}
}
