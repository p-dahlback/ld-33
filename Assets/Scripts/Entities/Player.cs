using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Player : MonoBehaviour
	{

		public Camera camera;
		public Rigidbody2D bullet;
		public float bulletSpeed = 10f;
		public float cooldown = 0.15f;
		private float timeSinceLastShot = 0f;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (timeSinceLastShot < cooldown) {
				timeSinceLastShot += Time.deltaTime;
			}

			Vector3 mousePosInWorld = camera.ScreenToWorldPoint (Input.mousePosition);
			transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePosInWorld - transform.position);

			if (timeSinceLastShot >= cooldown && Input.GetKeyDown (KeyCode.Space)) {
				FireBullet ();
				
				timeSinceLastShot = 0;
			}
		}

		public void FireBullet ()
		{
			Rigidbody2D newBullet = (Rigidbody2D)Instantiate (bullet, transform.position, transform.rotation);
			newBullet.gameObject.SetActive (true);
			newBullet.velocity = new Vector2 (transform.up.x, transform.up.y) * bulletSpeed;
		}

		public void AddMass (int value)
		{

		}
	}
}
