using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBomb : MonoBehaviour 
{
	[SerializeField]
	private GameObject explosionPrefab;

	public void Explode(Vector2 explosionPosition)
	{
		GameObject explosion = Instantiate(explosionPrefab, transform.root);
		explosion.transform.position = explosionPosition;
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.CompareTag("Death"))
            Destroy(gameObject);
	}

}
