using UnityEngine;
using System.Collections;

namespace LD33.Entities
{
	public class Blob : MonoBehaviour
	{
		
		public Blob blobFactory;
		public float pulseInterval = 0.5f;
		public float minimumScale = 0.5f;
		public float maximumScale = 1.5f;
		public int pulsesPerInterval = 2;
		private float deltaTime;

		
		public float maxMass = 1.0f;
		private float leftOverMass = 0;

		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
			MultiPulse ();
		}

		void MultiPulse ()
		{
			Vector3 scaleVector = transform.localScale;
		
			deltaTime += Time.deltaTime;
			if (deltaTime > pulseInterval) {
				deltaTime %= pulseInterval;	
			}
		
			float intervalPerPulse = pulseInterval / pulsesPerInterval;
			float currentPulse = deltaTime / intervalPerPulse;
			float pulseDelta = deltaTime % intervalPerPulse;

			float minMaxDiff = maximumScale - minimumScale;
			float diffPerPulse = minMaxDiff / pulsesPerInterval;


			float newScale = minimumScale + (currentPulse + 1) * diffPerPulse * Mathf.Sin (pulseDelta / intervalPerPulse);

			scaleVector.Set (newScale, newScale, newScale);
			transform.localScale = scaleVector;
		}

		public void AddMass (float value, Vector2 point)
		{
			minimumScale += value;
			maximumScale += value;

			if(maximumScale > maxMass)
			{
				float diff = maximumScale - maxMass;
				maximumScale = maxMass;
				
				leftOverMass = diff;
				if(diff >= 0.5f)
				{
					AddBlob(point);
				}
			}
		}
		
		private void AddBlob(Vector2 point)
		{
			GameObject newBlob = (GameObject) Instantiate(blobFactory, new Vector3(point.x, point.y, 0), transform.rotation);
			Blob blobSettings = newBlob.GetComponent<Blob>();
			blobSettings.AddMass(leftOverMass, point);
			
			SpringJoint2D joint = gameObject.AddComponent<SpringJoint2D>();
			joint.anchor = point;
			joint.distance = 0.2f;
			joint.dampingRatio = 0.2f;
			joint.connectedAnchor = point;
			
			leftOverMass = 0;
		}
	}
}
