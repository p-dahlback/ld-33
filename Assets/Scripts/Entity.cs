using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{

	public int health = 100;
	public Faction faction;

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
	}

	private void checkDamage ()
	{

		if (health == 0) {
			GameObject.DestroyObject (this);
		}
	}
}
