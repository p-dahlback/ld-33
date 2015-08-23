using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public bool isNorth = false;
	public bool isEast = false;

	public GameObject Spawn(GameObject gameObject, Quaternion rotation)
	{
		GameObject ret = (GameObject) Instantiate(gameObject, transform.position, rotation);
		ret.SetActive (true);
		return ret;
	}
}
