using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Pickup : MonoBehaviour
	{
		public float value = 0.2f;
		public float lifeTime = 5;
		public AudioSource creationSound;
		public AudioSource pickupSound;

		void Awake ()
		{
			Rigidbody2D body = GetComponent<Rigidbody2D> ();
			body.angularVelocity = Random.Range (30, 90);

			if (creationSound != null) {
				creationSound.Play ();
			}
		}

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
			Physics2D.IgnoreCollision (GetComponent<Collider2D> (), collision.collider);
			Blob blob = collision.gameObject.GetComponent<Blob> ();

			if (blob != null) {

				if (pickupSound != null) {
					pickupSound.Play ();
				}
				Destroy (this.gameObject);
				blob.AddMass (value, collision.contacts [0].point);

			}

		}
	}
}