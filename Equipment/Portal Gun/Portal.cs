using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour 
{
	private int color;
	private EquipmentPortalGun pg;
	[SerializeField]
	private Sprite[] sprites;

//	public Transform exitTransform;

	private bool playerEnterTrigger = true;
	private bool playerExitTrigger = true;

	private bool objectEnterTrigger = true;
	private bool objectExitTrigger = true;


	public int direction;

	private const int NORTH = 0;
	private const int EAST = 1;
	private const int SOUTH = 2;
	private const int WEST = 3;

	private const int BLUE = 0;
	private const int RED = 1;

	void Start()
	{
		SetColor();
		SetOrientation();
	}

	public void SetVariables(int _direction, int _color, EquipmentPortalGun _pg)
	{
		direction = _direction;
		color = _color;
		pg = _pg;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (pg.AreTwoPortalsActive())
		{
			if (other.gameObject.CompareTag("Player") && playerEnterTrigger)
			{
				
				playerExitTrigger = false;
				pg.WarpPlayer(color);
				return;
			}
			else if (other.gameObject.GetComponent(typeof(IEnterPortal<Vector3>)) as IEnterPortal<Vector3> != null && objectEnterTrigger)
			{
				objectExitTrigger = false;
				pg.WarpObject(color, other.gameObject);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (pg.AreTwoPortalsActive())
		{
			if (other.gameObject.CompareTag("Player") && playerExitTrigger)
			{
				pg.TogglePlayerPortals(true);
				return;
			}
			else if (other.gameObject.GetComponent(typeof(IEnterPortal<Vector3>)) as IEnterPortal<Vector3> != null && objectExitTrigger)
			{
				pg.ToggleObjectPortals(true);
				return;
			}
		}
	}
		
	public void SetPlayerEnterTrigger(bool toggle)
	{
		playerEnterTrigger = toggle;
	}

	public void SetPlayerExitTrigger(bool toggle)
	{
		playerExitTrigger = toggle;
	}

	public void SetObjectEnterTrigger(bool toggle)
	{
		objectEnterTrigger = toggle;
	}

	public void SetObjectExitTrigger(bool toggle)
	{
		objectExitTrigger = toggle;
	}

	public Vector3 GetNewDirection()
	{
		Vector3 newDirection;
		switch (direction)
		{
		case NORTH:
			newDirection = new Vector3(0,-1,0);
			break;
		case EAST:
			newDirection = new Vector3(-1,0,0);
			break;
		case SOUTH:
			newDirection = new Vector3(0,1,0);
			break;
		case WEST:
			newDirection = new Vector3(1,0,0);
			break;
		default:
			newDirection = Vector3.zero;
			break;
		}
		return newDirection;
	}

	private void SetOrientation()
	{
		if (direction == NORTH || direction == SOUTH)
		{
			gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
		}
		else
		{
			gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		}
	}

	private void SetColor()
	{
		SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
		sr.sprite = sprites[color];
	}
	
}
