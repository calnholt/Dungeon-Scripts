using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileColor : ProjectileBase
{
	[SerializeField]
	private Color[] color;
	[SerializeField]
	private int colorIndex;
			
	public void SetColor(int index)
	{
		GetComponentInChildren<SpriteRenderer>().color = color[index];
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}
	}
}
