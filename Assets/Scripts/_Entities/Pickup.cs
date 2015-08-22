using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Pickup : MonoBehaviour
	{
		public int value = 1;
		public float lifeTime = 5;


		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
			lifeTime -= Time.deltaTime;

			if (lifeTime <= 0) {
				Debug.Log ("Pickup expired");
				Destroy (gameObject);
			}
		}

		void OnCollisionEnter2D (Collision2D collision)
		{
			Blob blob = collision.gameObject.GetComponent<Blob> ();

			if (blob != null) {
				Debug.Log ("Picked up mass");
				blob.AddMass (value, collision.contacts[0].point);
				Destroy (gameObject);
			}
		}
	}
}