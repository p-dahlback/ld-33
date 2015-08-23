using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Asteroid : Entity
	{
		public float minimumSpeed = 1;
		public float maximumSpeed = 2;
		public float minimumRotationSpeed = 30;
		public float maximumRotationSpeed = 90;
		public float minimumSize = 1;
		public float maximumSize = 4;
		public float deathExplosionForce = 10;
		public AudioSource breakSound;
		private float speed;
		private float rotationSpeed;

		void Awake ()
		{
			speed = Random.value * (maximumSpeed - minimumSpeed) + minimumSpeed;
			rotationSpeed = Random.value * (maximumRotationSpeed - minimumRotationSpeed) + minimumSpeed;

			if (Random.Range (1, 3) % 2 == 0) {
				rotationSpeed = -rotationSpeed;
			}


			float scale = Random.value * (maximumSize - minimumSize) + minimumSize;
			transform.localScale = new Vector3 (scale, scale, scale);

			Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();
			rigidBody.velocity = transform.up * speed;
			rigidBody.angularVelocity = rotationSpeed;
			rigidBody.rotation = Random.rotationUniform.eulerAngles.z;
		}

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
			
		}

		void OnTriggerExit2D (Collider2D collider)
		{
			if (collider.gameObject.CompareTag (Constants.TAG_ENEMY_TRIGGER)) {
				Destroy (gameObject);
			}
		}
	
		protected override void OnDeath ()
		{
			if (breakSound) {
				breakSound.Play ();
			}

			float size = transform.localScale.x;
			int replacementNumber = CalculateReplacementNumber (size);
			if (replacementNumber == 0) {
				Debug.Log ("Not replacing asteroid - too small");
				return;
			}

			Vector3 centerPosition = transform.position;
			for (int i = 0; i < replacementNumber; i++) {

				float x = centerPosition.x + (i % 2 - 1) * size / 4;
				float y = centerPosition.y + (i / 2 - 1) * size / 4;
				Vector3 position = new Vector3 (x, y, 0);
				
				transform.rotation = Quaternion.LookRotation (Vector3.forward, position - transform.position);

				GameObject newAsteroid = InstantiateSubAsteroid (size / replacementNumber);
				newAsteroid.GetComponent<Entity> ().health = health <= 1 ? 1 : health / 2;
			}

			AudioSource sound = GetComponent<AudioSource> ();
			if (sound != null) {
				sound.Play ();
			}
		}

		GameObject InstantiateSubAsteroid (float maxSize)
		{
			maximumSpeed = speed;
			minimumSpeed = speed;
			maximumRotationSpeed = Mathf.Abs (rotationSpeed * 1.2f);
			minimumRotationSpeed = Mathf.Abs (rotationSpeed * 0.8f);
			
			maximumSize = maxSize;
			minimumSize = maxSize * 0.8f;

			return Instantiate (gameObject);
		}

		int CalculateReplacementNumber (float size)
		{
			if (size / 4 >= 0.5f)
				return 4;
			if (size / 3 >= 0.5f)
				return 3;
			if (size / 2 >= 0.5f)
				return 2;
			return 0;
		}
	}
}
