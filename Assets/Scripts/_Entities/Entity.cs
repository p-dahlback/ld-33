using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Entity : MonoBehaviour
	{

		public int health = 5;
		public Faction faction;
		public GameObject replacementOnDeath;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{

			checkDamage (false);
		}

		void OnCollisionEnter2D (Collision2D collision)
		{
			if (faction != Faction.MISC) {
				Entity entity = collision.gameObject.GetComponent<Entity> ();
				if (entity != null) {
					if (entity.faction == faction) {
						Physics2D.IgnoreCollision (gameObject.GetComponent<Collider2D> (), collision.collider);
					} else if (entity.faction == Faction.MISC) {
						Damage (1000);
					} else {
						Damage (entity.health);
					}
				}
			}
		}

		/**
		 * Damages the entity, killing it if its health reaches zero. 
		 */
		public void Damage (int damage)
		{
			health -= damage;
			checkDamage (false);
		}

		/**
		 * Consumes the entity's health points in return for actions or benefits.
		 * If the health reaches zero, the entity disappears.
		 */
		public void Consume (int damage)
		{
			health -= damage;
			checkDamage (true);
		}

		private void checkDamage (bool isConsumption)
		{

			if (health == 0) {

				OnDeath ();

				if (!isConsumption && replacementOnDeath != null) {
					Instantiate (replacementOnDeath, transform.position, transform.rotation);
				}
				Destroy (gameObject);
			}
		}

		protected virtual void OnDeath ()
		{
		}
	}

}