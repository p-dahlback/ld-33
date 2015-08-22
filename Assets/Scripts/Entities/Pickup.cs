using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Pickup : MonoBehaviour
	{
		public int value = 1;
		public float lifeTime = 5;


		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
			lifeTime -= Time.deltaTime;

			if(lifeTime <= 0)
			{
				Debug.Log("Pickup expired");
				Destroy (gameObject);
			}
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			Player player = collider.gameObject.GetComponent<Player>();

			if (player != null)
			{
				Debug.Log("Picked up mass");
				player.AddMass(value);
				Destroy (gameObject);
			}
		}
	}
}