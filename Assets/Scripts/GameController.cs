using UnityEngine;
using System.Collections;
using LD33.Entities;

namespace LD33
{
	public class GameController : MonoBehaviour
	{
		private static GameController _instance;
		public SpawnerController spawnerController;
		private int currentWave = 0;

		public static GameController GetInstance ()
		{
			return _instance;
		}

		void Awake ()
		{
			if (_instance == null) {
				_instance = this;

			} else {
				Destroy (gameObject);
			}
		}

		// Use this for initialization
		void Start ()
		{
			NewWave ();
		}
	
		void FixedUpdate ()
		{
			MarkDisconnectedBlobsForPickup ();
		}

		// Update is called once per frame
		void Update ()
		{

		}

		public void NewWave ()
		{
			spawnerController.SpawnEnemiesAndAsteroids (1, 0);
		}

		public void GameOver ()
		{
			Debug.Log ("Game over!");
		}

		public void PlayerChangeMainBody (Player player)
		{
			Debug.Log ("---Changing main body");
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Player");
			if (gameObjects != null && gameObjects.Length > 0) {

				int maxJoints = 0;
				GameObject maxJointsObject = null;
				Joint2D[] maxJointsArray = null;
				foreach (GameObject obj in gameObjects) {

					if (!obj.name.Equals ("Player")) {

						if (maxJointsObject == null) {
							maxJointsObject = obj;
						} else {

							Joint2D[] joints = obj.GetComponents<Joint2D> ();
							if (joints != null && joints.Length > maxJoints) {
								maxJoints = joints.Length;
								maxJointsObject = obj;
								maxJointsArray = joints;
							}
						
						}
					}
				}
				if (maxJointsObject != null) {

					maxJointsObject.name = "Player";
					DestroyImmediate (maxJointsObject.gameObject.GetComponent<Pickup> ());

					Player newPlayer = maxJointsObject.GetComponent<Player> ();
					newPlayer.enabled = true;
					newPlayer.CopyFrom (player);

					return;
				}
			}

			Debug.Log ("Couldn't find another body");
			GameOver ();

			Destroy (player);
		}

		public void MarkDisconnectedBlobsForPickup ()
		{
			GameObject[] parts = GameObject.FindGameObjectsWithTag ("Player");
			GameObject headObject = null;
			Joint2D[] joints = null;

			foreach (GameObject obj in parts) {
				if (obj.name.Equals ("Player")) {
				
					headObject = obj;
					joints = headObject.GetComponents<Joint2D> ();
				}
			}

			if (headObject != null && joints != null) {
				MarkForPickup (headObject, joints, parts);
			}
		}

		void MarkForPickup (GameObject headObject, Joint2D[] joints, GameObject[] playerParts)
		{
			Hashtable table = new Hashtable ();
			table.Add (headObject.GetInstanceID (), headObject);

			if (joints != null) {
				foreach (Joint2D joint in joints) {

					if (joint != null && joint.connectedBody != null && joint.connectedBody.gameObject != null) {
						GameObject obj = joint.connectedBody.gameObject;
						table.Add (obj.GetInstanceID (), obj);
						ProcessConnectedObjects (obj, table);
					} else {
						DestroyImmediate (joint);
					}
				}
			}

			if (table.Count < playerParts.Length) {
				foreach (GameObject part in playerParts) {
					if (part.name.Equals ("Player"))
						continue;

					if (!table.ContainsKey (part.GetInstanceID ())) {
						MarkForPickup (part);
					}
				}
			}
		}

		void ProcessConnectedObjects (GameObject baseObject, Hashtable processedObjects)
		{
			Pickup pickup = baseObject.GetComponent<Pickup> ();
			Joint2D[] joints = baseObject.GetComponents<Joint2D> ();

			if (pickup.enabled) {
				pickup.enabled = false;
				baseObject.tag = Constants.TAG_PLAYER;
				baseObject.layer = Constants.PLAYER_LAYER;
			}

			if (joints != null && joints.Length > 0) {
				foreach (Joint2D joint in joints) {

					if (joint != null && joint.connectedBody != null && joint.gameObject != null) {
						GameObject obj = joint.connectedBody.gameObject;

						if (!processedObjects.ContainsKey (obj.GetInstanceID ())) {
						
							if (obj.name.Equals (Constants.TAG_PLAYER)) {
								processedObjects.Add (obj.GetInstanceID (), obj);
								continue;
							} else {

								processedObjects.Add (obj.GetInstanceID (), obj);
								ProcessConnectedObjects (obj, processedObjects);
							}
						}
					} else {
						DestroyImmediate (joint);
					}
				}
			}
		}

		void MarkForPickup (GameObject obj)
		{
			Pickup pickup = obj.GetComponent<Pickup> ();
			Blob blob = obj.GetComponent<Blob> ();

			if (pickup != null && blob != null) {
				
				pickup.enabled = true;
				pickup.value = (blob.LeftOverMass + blob.maximumScale);
				if (pickup.value < 0.15f) {
					pickup.value = 0.15f;
				}

				obj.tag = Constants.TAG_PICKUP;
				obj.layer = Constants.BLOB_PICKUP_LAYER;
			}
		}
	}

}
