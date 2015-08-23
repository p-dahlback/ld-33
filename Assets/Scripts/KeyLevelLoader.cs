using UnityEngine;
using System.Collections;

namespace LD33
{
	public class KeyLevelLoader : MonoBehaviour
	{

		public ScreenManager screenManager;
		public string levelToLoad;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
			if (Input.anyKeyDown) {
				screenManager.LoadLevelWithFade (levelToLoad);
				enabled = false;
			}

		}
	}
}