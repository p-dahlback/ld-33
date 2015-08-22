using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Player : MonoBehaviour
	{
		public Rigidbody2D bullet;
		public float bulletSpeed = 10f;
		public float cooldown = 0.15f;
		public float speed = 1000.0f;
		public float rotationSpeed = 180;
		private float timeSinceLastShot = 0f;

		public void CopyFrom (Player player)
		{
			bullet = player.bullet;
			bulletSpeed = player.bulletSpeed;
			cooldown = player.cooldown;
		}

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
			RotateAndThrust ();
			HandleFiring ();
		}

		void HandleFiring ()
		{
			if (timeSinceLastShot < cooldown) {
				timeSinceLastShot += Time.deltaTime;
			}
			
			if (timeSinceLastShot >= cooldown && Input.GetAxis ("Fire1") != 0) {
				FireBullet ();
				
				timeSinceLastShot = 0;
			}
		}

		void RotateAndThrust ()
		{
			float horizontalThrust = Input.GetAxis ("Horizontal");
			float verticalThrust = Input.GetAxis ("Vertical");

			Rigidbody2D body = GetComponent<Rigidbody2D> ();

			if (Mathf.Abs (body.angularVelocity) < rotationSpeed) {

				body.AddTorque (horizontalThrust * rotationSpeed * Time.deltaTime);
			}
			body.AddForce (transform.up * verticalThrust * speed * Time.deltaTime);
		}

		void MouseMove ()
		{
			Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePosInWorld - transform.position);
		}

		public void FireBullet ()
		{
			Rigidbody2D newBullet = (Rigidbody2D)Instantiate (bullet, transform.position, transform.rotation);
			newBullet.gameObject.SetActive (true);
			newBullet.velocity = new Vector2 (transform.up.x, transform.up.y) * bulletSpeed;
		}
	}
}
