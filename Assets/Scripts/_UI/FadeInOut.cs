using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LD33.UI
{
	public class FadeInOut : MonoBehaviour
	{
		public Image fadeImage;

		public float fadeDuration = 2.0f;

		private float leftOfFade = 0.0f;

		void Update()
		{
			if (leftOfFade > 0)
			{
				leftOfFade -= Time.deltaTime;
				if(leftOfFade <= 0)
				{
					fadeImage.gameObject.SetActive(false);
				}
			}
		}

		public void FadeIn()
		{
			fadeImage.gameObject.SetActive(true);
			fadeImage.enabled = true;
			Color color = fadeImage.color;
			fadeImage.color = new Color(color.r, color.g, color.b, 1.0f);
			fadeImage.CrossFadeAlpha(0.0f, fadeDuration, false);
			leftOfFade = fadeDuration;
		}

		public void FadeOut()
		{
			fadeImage.gameObject.SetActive(true);
			fadeImage.enabled = true;
			Color color = fadeImage.color;
			fadeImage.color = new Color(color.r, color.g, color.b, 1.0f);
			fadeImage.canvasRenderer.SetAlpha(0.0f);
			fadeImage.CrossFadeAlpha(1.0f, fadeDuration, false);
			leftOfFade = 0f;
		}
	}
}