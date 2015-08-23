using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour
{

	public float selfDestructTime;
	public GameObject replacementObject;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		selfDestructTime -= Time.deltaTime;

		if (selfDestructTime <= 0) {
			Die ();
		}
	}

	void Die ()
	{
		if (replacementObject != null) {
			GameObject replacement = (GameObject)Instantiate (replacementObject, transform.position, transform.rotation);
			Rigidbody2D body = replacement.GetComponent<Rigidbody2D> ();
			replacement.SetActive (true);
			if (body != null) {
				Rigidbody2D currentBody = gameObject.GetComponent<Rigidbody2D> ();
				body.velocity = currentBody.velocity;
			}
		}
		Destroy (gameObject);
	}
}
