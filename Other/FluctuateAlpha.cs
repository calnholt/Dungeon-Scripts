using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluctuateAlpha : MonoBehaviour 
{
	public float speed;
	[Range(0,1)]
	public float maxAlpha;
	[Range(0,1)]
	public float minAlpha;
	private SpriteRenderer sr;
	private Color originalColor;
	private float t;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		originalColor = sr.color;

	}

	void Update () 
	{
		t += speed * Time.deltaTime;
		sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(maxAlpha, minAlpha, t));
//		Mathf.PingPong(

		if (t > 1.0f)
		{
			float temp = maxAlpha;
			maxAlpha = minAlpha;
			minAlpha = temp;
			t = 0.0f;
		}
	}
}
