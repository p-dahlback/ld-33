using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{


	public float damageValue;
	public Faction faction;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void onPhysics2DCollision (Collider2D collider)
	{
		Entity entity = gameObject.GetComponent (Entity);

		if (entity != null) {
			if (entity.faction == null) {
				throw new UnityException ("Object of type " + collider.gameObject.GetType () + " and ID "
					+ collider.gameObject.GetInstanceID () + " has faction null");
			}

			if (entity.faction != faction || entity.faction == Faction.MISC) {
				entity.Damage (damageValue);
			}
		}

		GameObject.DestroyObject (this);
	}
}
