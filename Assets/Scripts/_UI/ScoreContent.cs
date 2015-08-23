using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LD33.UI
{
	public class ScoreContent : MonoBehaviour
	{

		public Text text;

		// Use this for initialization
		void Start ()
		{
			text.text = ScoreController.GetInstance ().Score.ToString ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	}
}