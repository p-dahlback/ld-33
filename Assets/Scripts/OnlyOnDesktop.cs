using UnityEngine;
using System.Collections;

public class OnlyOnDesktop : MonoBehaviour {

	void Awake()
	{
		if (Application.isMobilePlatform)
		{
			gameObject.SetActive (false);
		}
		else if (Application.isWebPlayer)
		{
			gameObject.SetActive (false);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
