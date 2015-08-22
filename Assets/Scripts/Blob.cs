using UnityEngine;
using System.Collections;

public class Blob : MonoBehaviour
{

	public float pulseInterval = 0.5f;
	public float minimumScale = 0.5f;
	public float maximumScale = 1.5f;
	public int pulsesPerInterval = 2;
	private float deltaTime;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		MultiPulse ();
	}

	void NormalPulse ()
	{
		Vector3 scaleVector = transform.localScale;
		
		deltaTime += Time.deltaTime;
		if (deltaTime > pulseInterval) {
			deltaTime %= pulseInterval;	
		}
		
		float minMaxDiff = maximumScale - minimumScale;
		float newScale = minimumScale + minMaxDiff * Mathf.Sin (deltaTime / pulseInterval);

		scaleVector.Set (newScale, newScale, newScale);
		transform.localScale = scaleVector;
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
}
