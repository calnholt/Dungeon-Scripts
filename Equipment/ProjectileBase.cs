using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour, IEnterPortal<Vector3>
{
	public bool enterPortal;
	protected Rigidbody2D rb;
	public float speed;
	[HideInInspector]
	public Vector3 direction;

	void Awake () 
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () 
	{
		rb.velocity = direction * speed;
	}



	public void SetDirection(Vector3 _direction)
	{
		direction = _direction;
	}

	public void SetSpeed(float _speed)
	{
		speed = _speed;
	}

	public Vector3 GetDirection()
	{
		return direction;
	}

	public float GetSpeed()
	{
		return speed;
	}

	public void EnterPortal(Vector3 _direction)
	{
		if (enterPortal)
		{
			direction = _direction;
		}
	}
}
