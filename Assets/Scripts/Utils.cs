using UnityEngine;
using System.Collections;

namespace LD33
{
	public class Utils
	{

		public static void AddExplosionForce (Rigidbody2D rigidBody, float explosionForce, Vector3 explosionPosition, float explosionRadius)
		{
			Vector3 direction = rigidBody.transform.position - explosionPosition;
			float wearOff = 1 - (direction.magnitude / explosionRadius);
			rigidBody.AddForce (direction.normalized * explosionForce * wearOff);
		}

	}

}