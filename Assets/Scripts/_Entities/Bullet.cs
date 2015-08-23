using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Bullet : MonoBehaviour
	{
		public int damageValue;
		public float lifeTime = 4;
		public Faction faction;

		public Vector2 velocity;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
			velocity = GetComponent<Rigidbody2D>().velocity;
			lifeTime -= Time.deltaTime;

			if(lifeTime < 0) 
			{
				Destroy (gameObject);
			}
		}

		void OnCollisionEnter2D (Collision2D collision)
		{
			Faction otherFaction = Faction.MISC;
			bool factionCollision = false;
			Entity entity = collision.gameObject.GetComponent<Entity> ();
			Bullet bullet = collision.gameObject.GetComponent<Bullet>(); 

			if(entity != null)
			{
				otherFaction = entity.faction;
				factionCollision = true;

			} else if(bullet != null)
			{
				otherFaction = bullet.faction;
				factionCollision = true;
			}

			if (factionCollision) {

				if (otherFaction != faction || otherFaction == Faction.MISC) {
					Debug.Log("Hit damageable object");
					if(entity != null) {
						entity.Damage (damageValue);
					}
				} else {
					Debug.Log("Ignoring object from same faction");
//					Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
					return;
				}
			}

			Destroy(gameObject);
		}
	}

}