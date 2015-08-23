using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Entity : MonoBehaviour
	{

		public float health = 5;
		public Faction faction;
		public GameObject replacementOnDeath;
		private float currentHealth;

		public float CurrentHealth {
			get { return currentHealth; }
		}

		void Awake ()
		{
			currentHealth = health;
		}

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
						Debug.Log ("Misc! Harm!");
						Damage (1);
					} else {
						Debug.Log ("Other faction! Damage!");
						Damage (entity.currentHealth);

						if (currentHealth <= 0 && entity.gameObject != null && 
							(entity.gameObject.CompareTag (Constants.TAG_CURRENT_PLAYER) 
							|| entity.gameObject.CompareTag (Constants.TAG_PLAYER))) {
							Debug.Log ("Adding points for killing unit!");
							GameController.GetInstance ().AddScore (0.3f);
						}
					}
				}
			}
		}

		/**
		 * Damages the entity, killing it if its health reaches zero. 
		 */
		public void Damage (float damage)
		{
			AudioSource sound = GetComponent<AudioSource> ();
			if (sound) {
				sound.Play ();
			}

			currentHealth -= damage;
			checkDamage (false);
		}

		/**
		 * Consumes the entity's health points in return for actions or benefits.
		 * If the health reaches zero, the entity disappears.
		 */
		public void Consume (float damage)
		{
			currentHealth -= damage;
			checkDamage (true);
		}

		public void ResetHealth ()
		{
			currentHealth = health;
			checkDamage (false);
		}

		private void checkDamage (bool isConsumption)
		{

			if (currentHealth <= 0) {

				OnDeath ();

				if (!isConsumption && replacementOnDeath != null) {
					GameObject replacement = (GameObject)Instantiate (replacementOnDeath, transform.position, transform.rotation);
					Rigidbody2D body = replacement.GetComponent<Rigidbody2D> ();
					replacement.SetActive (true);
					if (body != null) {
						Rigidbody2D currentBody = gameObject.GetComponent<Rigidbody2D> ();
						body.velocity = currentBody.velocity * 0.3f;
					}

				}
				
				Player player = GetComponent<Player> ();
				bool isPlayer = player != null && player.enabled;
				Destroy (gameObject);

				if (isPlayer) {
					GameController controller = GameController.GetInstance ();
					controller.PlayerChangeMainBody (player);
				}

			}
		}

		protected virtual void OnDeath ()
		{
		}
	}

}