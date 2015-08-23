using UnityEngine;
using System.Collections;
using LD33.UI;

namespace LD33
{
	public class ScreenManager : MonoBehaviour
	{

		public FadeInOut sceneFader;
		public AudioSource sound;
		private float screenChangeTime = 0;
		private string screenToChangeTo;

		// Use this for initialization
		void Start ()
		{
			sceneFader.FadeIn ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
			if (screenChangeTime > 0) {
				screenChangeTime -= Time.deltaTime;

				if (screenChangeTime <= 0) {

					if (screenToChangeTo != null) {
						LoadLevel (screenToChangeTo);
					} else {
						Quit ();
					}
				}
			}
		}

		public void LoadLevel (string name)
		{
			Application.LoadLevel (name);
		}

		public void LoadLevelWithFade (string name)
		{
			if (sound != null) {
				sound.Play ();
			}
			sceneFader.FadeOut ();

			screenToChangeTo = name;
			screenChangeTime = sceneFader.fadeDuration + 0.5f;
		}

		public void QuitWithFade ()
		{	
			if (sound != null) {
				sound.Play ();
			}
			sceneFader.FadeOut ();

			screenToChangeTo = null;
			screenChangeTime = sceneFader.fadeDuration;
		}

		public void Quit ()
		{
			Application.Quit ();
		}
	}
}