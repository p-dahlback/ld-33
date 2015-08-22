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
		public float maxMass = 1.0f;
		public float requiredMassForSplit = 0.5f;
		private float deltaTime;
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
			Debug.Log ("Picked up mass: " + value);
			if (maximumScale >= maxMass) {
				leftOverMass += value;
			} else {

				minimumScale += value;
				maximumScale += value;

				if (maximumScale > maxMass) {
					float diff = maximumScale - maxMass;
					maximumScale = maxMass;
					minimumScale -= diff;
					
					leftOverMass = diff;
				}
			}
			if (leftOverMass >= requiredMassForSplit) {
				AddBlob (point);
			}
		}
		
		private void AddBlob (Vector2 point)
		{
			leftOverMass -= requiredMassForSplit;

			Blob newBlob = (Blob)Instantiate (blobFactory, new Vector3 (point.x, point.y, 0), transform.rotation);
			newBlob.blobFactory = blobFactory;
			newBlob.gameObject.SetActive (true);
			newBlob.AddMass (leftOverMass, point);

//			SpringJoint2D joint = gameObject.AddComponent<SpringJoint2D> ();
//			joint.distance = 0.05f;
//			joint.dampingRatio = 0.95f;
//			joint.anchor = transform.InverseTransformPoint(point);
//			joint.connectedBody = newBlob.gameObject.GetComponent<Rigidbody2D> ();

			DistanceJoint2D joint = gameObject.AddComponent<DistanceJoint2D>();
			joint.distance = 0.05f;
			joint.anchor = transform.InverseTransformPoint(point);;
			joint.connectedBody = newBlob.gameObject.GetComponent<Rigidbody2D>();

			leftOverMass = 0;
		}
	}
}
