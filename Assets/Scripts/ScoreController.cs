using UnityEngine;
using System.Collections;

namespace LD33
{
	public class ScoreController : MonoBehaviour
	{
		private static ScoreController _instance;
		
		public int scoreModifier = 100;

		private int score;

		public int Score
		{
			get { return score; }
		}

		public static ScoreController GetInstance()
		{
			return _instance;
		}

		public void AddScore(float mass)
		{
			score += (int) (mass * scoreModifier);
		}

		public void Reset()
		{
			score = 0;
		}

		void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
				DontDestroyOnLoad (this);
			} else {
				Destroy (gameObject);
			}
		}

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	}
}