using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

	public bool isNorth = false;
	public bool isEast = false;
	private float spawnDelay;
	private GameObject toSpawnObject;
	private Quaternion toSpawnQuaternion;

	void Update ()
	{
		if (spawnDelay > 0) {
			spawnDelay -= Time.deltaTime;

			if (spawnDelay <= 0) {
				GameObject obj = (GameObject)Instantiate (toSpawnObject, transform.position, toSpawnQuaternion);
				obj.SetActive (true);
			}
		}
	}

	public void Spawn (GameObject gameObject, Quaternion rotation, float spawnDelay)
	{
		this.toSpawnObject = gameObject;
		this.toSpawnQuaternion = rotation;
		this.spawnDelay = spawnDelay;
	}
}
