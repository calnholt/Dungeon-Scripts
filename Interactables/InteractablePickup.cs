using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickup : InteractableBase, IEnterPortal<Vector3>, IPushable<Vector3>, IMovingPlatform<Transform>
{
	public float throwDistance = 5;
	public float throwSpeed = 3;
	public bool isThrowable;

	private Rigidbody2D rb;
	private bool isBeingHeld = false;
	private bool isMidair = false;

	Vector3 m_CharacterThrowPosition;
	Vector3 m_ThrowDirection;

	[HideInInspector]
	public bool stopMoving = false;
	[HideInInspector]
	public bool isBeingThrown = false;

	private Transform startParent;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		startParent = transform.parent;
	}

	public override void OnInteract( Character character )
	{
		CharacterInteractionModel interactionModel = character.GetComponent<CharacterInteractionModel>();

		if( interactionModel == null )
		{
			return;
		}

		rb.velocity = Vector3.zero;
		isBeingHeld = true;
		interactionModel.PickupObject( this );
		stopMoving = false;
	}

	public void Throw( Character character )
	{
		StartCoroutine( ThrowRoutine( character.transform.position, character.Movement.GetFacingDirectionNoDiagonal() ) );
	}

	IEnumerator ThrowRoutine( Vector3 characterThrowPosition, Vector3 throwDirection )
	{
		rb.velocity = throwDirection * throwSpeed;
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.gravityScale = 0;
		isBeingHeld = false;
		isBeingThrown = true;
		yield return new WaitForSeconds (throwDistance);
		isBeingThrown = false;
		transform.parent = transform.root;
		rb.velocity = Vector2.zero;

	}

	public void PutDown(Character character)
	{
		if (character.Movement.GetFacingDirectionNoDiagonal() != new Vector3(0, -1, 0))
			transform.position = character.gameObject.transform.position + (character.Movement.GetFacingDirectionNoDiagonal()/2);
		else 
		{
			transform.position = character.gameObject.transform.position + (character.Movement.GetFacingDirectionNoDiagonal()/1.3f);
		}
		transform.parent = startParent;
		isBeingHeld = false;
	}

	public void EnterPortal(Vector3 newDirection)
	{
		if (isBeingThrown)
		{
			rb.velocity = throwSpeed * newDirection;
		}
	}

	public void Push(Vector3 velocity)
	{
		if (velocity != Vector3.zero)
		{
			isMidair = true;
		}
		else
		{
			isMidair = false;
		}
		rb.velocity = velocity;
	}

	public void EnterMovingPlatform(Transform parent)
	{
		if (!transform.parent.IsChildOf(parent) && !isMidair)
		{
			transform.parent = parent;
		}
	}

	public void ExitMovingPlatform(Transform parent)
	{
		if (!isBeingHeld)
		{
			transform.parent = transform.root;
		}
	}

	public void SetToStartParent()
	{
		transform.parent = startParent;
		isBeingHeld = false;
	}


		
}
