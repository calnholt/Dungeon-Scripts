using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGrabbable : InteractableBase
{
	public bool restrictAxis;
    public bool canGoThroughPortal = false;
	private bool isGrabbing = false;
	private Character character;
	private float distance;
	private Rigidbody2D rb;
	private Vector3 velocity;


	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update () 
	{
		if (isGrabbing)
		{
			MoveGrabbable();
		}
	}

	public override void OnInteract( Character _character )
	{
		CharacterInteractionModel interactionModel = _character.GetComponent<CharacterInteractionModel>();
		character = _character;


		if ( interactionModel == null )
		{
			return;
		}

		if (!isGrabbing)
		{
			interactionModel.GrabObject( this, character );
			isGrabbing = true;
			distance = Vector3.Distance(character.transform.position, transform.position);
		}
		else
		{
			isGrabbing = false;
           
		}
	}

	public void SetIsGrabbing(bool _isGrabbing)
	{
		isGrabbing = _isGrabbing;
		rb.velocity = new Vector3(0,0,0);
	}

	public bool GetRestrict()
	{
		return restrictAxis;
	}

    //public void EnterPortal(Vector3 newDirection)
    //{
    //    if (canGoThroughPortal)
    //    {
    //        character.Interaction.StopGrabbingObject(this);
    //        return;
    //    }
    //}

	public void MoveGrabbable()
	{
		float moveOffset = 0.02f;
		Vector3 direction = character.Movement.GetPushingDirection();
		rb.velocity = character.Movement.GetCharacterSpeed();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Wall") && isGrabbing)
		{
			character.Interaction.StopGrabbingObject(this);

			Debug.Log("Enter on trigger box?");
		}
	}
}
