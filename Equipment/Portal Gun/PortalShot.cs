using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalShot : ProjectileBase
{
	//public float speed;

	[HideInInspector]
	public EquipmentPortalGun pg;
	[HideInInspector]
	public Character character;
	//private Rigidbody2D rb;
	//private Vector3 direction;
    private bool hasSpawnedPortal = false;

	void Start () 
	{
		//rb = GetComponent<Rigidbody2D>();
		character = pg.character;
		direction = character.Movement.GetFacingDirectionNoDiagonal();
	}
	
	//void FixedUpdate () 
	//{
	//	//rb.velocity = direction * speed;
	//}

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.GetComponent<PortalWall>() != null && !hasSpawnedPortal)
		{
            hasSpawnedPortal = true;
			pg.SpawnPortal(gameObject.transform.position, other.gameObject.GetComponent<PortalWall>());
			Destroy(gameObject);
		}
		else if (other.gameObject.GetComponent<WallType>() != null)
		{
			if (other.gameObject.GetComponent<WallType>().portalShot)
			{
				return;
			}
			else
			{
				Destroy(gameObject);
			}
		}
		else if (other.gameObject.CompareTag("Wall") || other.gameObject.GetComponent<Portal>() != null)
		{
			Destroy(gameObject);
		}
	}

	public void ChangeDirectionFromPortal(Vector3 newDirection)
	{
		rb.velocity = newDirection * speed;
	}
}
