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
		public float massConsumptionPerShot = 0.5f;
		private float timeSinceLastShot = 0f;
		private Blob leastConnectedBlob;

		void Awake ()
		{
			FindLeastConnectedBlob ();
		}

		public void CopyFrom (Player player)
		{
			bullet = player.bullet;
			bulletSpeed = player.bulletSpeed;
			cooldown = player.cooldown;

			Rigidbody2D body = GetComponent<Rigidbody2D> ();
			Rigidbody2D playerBody = player.GetComponent<Rigidbody2D> ();
			body.mass = playerBody.mass;
			body.angularDrag = playerBody.angularDrag;
			body.drag = playerBody.drag;
		}

		// Use this for initialization
		void Start ()
		{
	
		}

		// Update is called once per frame
		void Update ()
		{
			AimStrafeAndThrust ();
			HandleFiring ();

			if (Debug.isDebugBuild && Input.GetKeyDown (KeyCode.K)) {
				Entity entity = GetComponent<Entity> ();
				entity.Damage (9999);
			}
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

		void AimStrafeAndThrust ()
		{
			Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePosInWorld - transform.position);
			
			float horizontalThrust = Input.GetAxis ("Horizontal");
			float verticalThrust = Input.GetAxis ("Vertical");

			
			Rigidbody2D body = GetComponent<Rigidbody2D> ();
			body.AddForce (transform.up * verticalThrust * speed * Time.deltaTime);
			body.AddForce (transform.right * -horizontalThrust * speed * Time.deltaTime);
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

			if (leastConnectedBlob == null) {
				FindLeastConnectedBlob ();
			}
			Entity blobEntity = leastConnectedBlob.GetComponent<Entity> ();
			if (leastConnectedBlob == this) {

				blobEntity.Damage (massConsumptionPerShot);

			} else {

				blobEntity.Consume (massConsumptionPerShot);
			}
		}

		public void FindLeastConnectedBlob ()
		{
			Hashtable processingTable = new Hashtable ();
			processingTable.Add (gameObject.GetInstanceID (), gameObject);
			GameObject leastConnected = FindLeastConnectedBlob (gameObject, processingTable);
			leastConnectedBlob = leastConnected.GetComponent<Blob> ();
		}

		GameObject FindLeastConnectedBlob (GameObject obj, Hashtable processedObjects)
		{
			Joint2D[] joints = obj.GetComponents<Joint2D> ();
			if (joints != null && joints.Length > 0) {

				int minJoints = joints.Length;
				GameObject minJointsObject = obj;
				foreach (Joint2D joint in joints) {
					if (joint != null && joint.connectedBody != null && joint.connectedBody.gameObject != null) {
						GameObject connectedObj = joint.connectedBody.gameObject;
						if (processedObjects.ContainsKey (connectedObj.GetInstanceID ())) {
							continue;
						}

						processedObjects.Add (connectedObj.GetInstanceID (), connectedObj);
						GameObject subLeastConnected = FindLeastConnectedBlob (connectedObj, processedObjects);

						Joint2D[] subJoints = subLeastConnected.GetComponents<Joint2D> ();
						if (subJoints == null)
							return subLeastConnected;

						if (subJoints.Length < minJoints) {
							minJoints = subJoints.Length;
							minJointsObject = subLeastConnected;
						}
					} else {
						DestroyImmediate (joint);
					}
				}
				return minJointsObject;
			} 
			return obj;
		}
	}
}
