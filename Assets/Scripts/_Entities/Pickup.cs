using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Pickup : MonoBehaviour
	{
		public float value = 0.2f;
		public float lifeTime = 5;

		void Awake()
		{
			Rigidbody2D body = GetComponent<Rigidbody2D>();
			body.angularVelocity = Random.Range(30, 90);
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
				blob.AddMass (value, collision.contacts [0].point);
				Destroy (gameObject);
			}

		}
	}
}