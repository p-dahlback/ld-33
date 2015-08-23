using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Enemy : Entity
	{
		public enum Ai
		{
			HARMLESS,
			STRAIGHT_SHOOTER,
			SEEK_PLAYER,
		}

		public Ai aiSetting;
		public Rigidbody2D bullet;
		public float bulletSpeed = 5f;
		public float speed = 5f;
		public float cooldown = 1.0f;
		public float aiDistance = 2.5f;
		public float rotationSpeed = 45f;
		private bool aiStarted = false;
		private float timeSinceLastShot;
		private Blob target;

		void Awake ()
		{
			Rigidbody2D body = gameObject.GetComponent<Rigidbody2D> ();
			body.velocity = body.transform.up * speed;
		}

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (aiStarted) {

				switch (aiSetting) {
				default:
				case Ai.HARMLESS:
					break;
				case Ai.STRAIGHT_SHOOTER:
					FireBullets ();
					break;
				case Ai.SEEK_PLAYER:

					SeekPlayer ();
					break;
				}
			}
		}

		void OnTriggerEnter2D (Collider2D collider)
		{
			if (collider.gameObject.CompareTag (Constants.TAG_ENEMY_TRIGGER)) {
				aiStarted = true;
			}
		}

		void OnTriggerExit2D (Collider2D collider)
		{
			if (collider.gameObject.CompareTag (Constants.TAG_ENEMY_TRIGGER)) {
				Destroy (gameObject);
			}
		}

		public void SeekPlayer ()
		{
			if (target == null) {
				FindTarget ();
			}

			if (target != null) {
				
				Rigidbody2D body = gameObject.GetComponent<Rigidbody2D> ();
				Quaternion targetRotation = Quaternion.LookRotation (Vector3.forward, target.transform.position - transform.position);
				float targetRotZ = targetRotation.eulerAngles.z;
				
				body.MoveRotation (Mathf.MoveTowardsAngle (body.rotation, targetRotZ, rotationSpeed * Time.deltaTime));
				
				FireBullets ();

				if ((target.transform.position - transform.position).magnitude < 2.5f) {	
				
					body.velocity = body.velocity * 0.8f * Time.deltaTime;

				} else {
					body.velocity = body.transform.up * speed;
				}
			}
		}
		
		public void AvoidAsteroids ()
		{

		}

		public void FindTarget ()
		{
			Blob[] targets = GameObject.FindObjectsOfType<Blob> ();
			int index = Random.Range (0, targets.Length);
			target = targets [index];
		}

		public void FireBullets ()
		{
			if (timeSinceLastShot < cooldown) {
				timeSinceLastShot += Time.deltaTime;
			}

			if (timeSinceLastShot >= cooldown) {
				timeSinceLastShot = 0;
				foreach (Transform child in transform) {
					if (child.CompareTag ("Gun")) {
						SpawnBullet (child);
					}
				}
			}
		}

		private void SpawnBullet (Transform gun)
		{
			Rigidbody2D newBullet = (Rigidbody2D)Instantiate (bullet, gun.position, gun.rotation);
			newBullet.gameObject.SetActive (true);
			newBullet.velocity = newBullet.transform.up * bulletSpeed;
		}
	}
}