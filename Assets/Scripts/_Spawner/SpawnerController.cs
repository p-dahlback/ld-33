using UnityEngine;
using System.Collections;
using LD33.Entities;

namespace LD33
{
	public class SpawnerController : MonoBehaviour
	{

		public Spawner[] spawners;
		public Enemy[] enemyPrefabs;
		public Asteroid[] asteroidPrefabs;
		public float maxWaitTime = 1.0f;

		public void SetEnemyPrefabs(params Enemy[] enemies)
		{
			enemyPrefabs = enemies;
		}

		public void SetAsteroidPrefabs(params Asteroid[] asteroids)
		{
			asteroidPrefabs = asteroids;
		}

		public void SpawnEnemiesAndAsteroids (int enemyCount, int asteroids)
		{
			ArrayList spawnerSelection = new ArrayList();
			spawnerSelection.AddRange(spawners);

			SpawnFromSelection(spawnerSelection, enemyPrefabs, enemyCount);
			SpawnFromSelection(spawnerSelection, asteroidPrefabs, asteroids);
		}

		private void SpawnFromSelection(IList spawners, MonoBehaviour[] prefabs, int numberToSpawn)
		{
			if (spawners.Count == 0) 
			{
				Debug.Log ("Skipping spawn of " + numberToSpawn + " units - no spawners left");
				return;
			}

			for (int i = 0; i < numberToSpawn; i++)
			{
				int index = Random.Range (0, spawners.Count);
				Spawner spawner = (Spawner) spawners[index];
				
				Vector3 direction = DirectionFromSpawner(spawner);
				Quaternion rotation = Quaternion.LookRotation (Vector3.forward, direction);
				float time = Random.value * maxWaitTime;
				spawner.Spawn(prefabs[Random.Range (0, prefabs.Length)].gameObject, rotation, time);
				
				spawners.RemoveAt (index);
				if (spawners.Count == 0)
					return;
			}
		}

		public Spawner GetRandomSpawner ()
		{
			int index = Random.Range (0, spawners.Length);
			return spawners [index];
		}

		public Vector3 DirectionFromSpawner (Spawner startSpawner)
		{
			ArrayList spawnerSelection = new ArrayList ();

			foreach (Spawner spawner in spawners) {
				if (spawner.isNorth != startSpawner.isNorth && spawner.isEast != startSpawner.isEast) {
					spawnerSelection.Add (spawner);
				}
			}

			int index = Random.Range (0, spawnerSelection.Count);
			Spawner chosenSpawner = (Spawner)spawnerSelection [index];

			return (chosenSpawner.transform.position - startSpawner.transform.position).normalized;
		}

	}
}
	