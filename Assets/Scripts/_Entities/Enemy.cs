using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Enemy : Entity
	{
		public Rigidbody2D bullet;
		public float bulletSpeed = 5f;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void SeekPlayer ()
		{

		}

		public void AvoidAsteroids ()
		{

		}

		public void FireBullets()
		{
			foreach (Transform child in transform)
			{
				if(child.CompareTag("Gun"))
				{
					SpawnBullet(child.gameObject);
				}
			}
		}

		private void SpawnBullet(GameObject gun)
		{
			Rigidbody2D newBullet = (Rigidbody2D) Instantiate(bullet, gun.transform.position, gun.transform.rotation);
			newBullet.velocity = newBullet.transform.up * bulletSpeed;
		}
	}
}