using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Entity : MonoBehaviour
	{

		public int health = 100;
		public Faction faction;

		public GameObject replacementOnDeath;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{

			checkDamage ();
		}

		public void Damage (int damage)
		{
			health -= damage;
			checkDamage();
		}

		private void checkDamage ()
		{

			if (health == 0) {

				OnDeath();

				if (replacementOnDeath != null) 
				{
					GameObject deadClone = (GameObject) Instantiate(replacementOnDeath, transform.position, transform.rotation);
				}
				Destroy (gameObject);
			}
		}

		protected virtual void OnDeath()
		{
		}
	}

}