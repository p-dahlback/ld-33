using UnityEngine;
using System.Collections;
using LD33.Entities;

namespace LD33
{
	public class GameController : MonoBehaviour
	{
		public SpawnerController spawnerController;

		private int currentWave = 0;

		// Use this for initialization
		void Start ()
		{
			NewWave();
		}
	
		// Update is called once per frame
		void Update ()
		{

		}

		public void NewWave ()
		{
			spawnerController.SpawnEnemiesAndAsteroids(1, 0);
		}

		public void GameOver ()
		{
			Debug.Log ("Game over!");
		}

		public void PlayerChangeMainBody (Player player)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag ("Player");
			if (gameObject != null) {

				Player newPlayer = gameObject.AddComponent<Player> ();
				newPlayer.CopyFrom (player);
			} else {

				Debug.Log ("Couldn't find another body");
				GameOver ();
			}

			Destroy (player);
		}
	}
}
