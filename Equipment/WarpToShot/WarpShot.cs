using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpShot : ProjectileBase
{

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<WallType>() != null)
		{
			if (other.gameObject.GetComponent<WallType>().warpShot)
			{
				return;
			}
			else
			{
				Destroy(gameObject);
			}
		}
		else if (other.gameObject.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}
	}
}
