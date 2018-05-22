using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectiles : MonoBehaviour 
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.GetComponent<ProjectileBase>() != null)
            Destroy(collision.gameObject);
	}

}
