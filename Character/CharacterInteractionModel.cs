using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInteractionModel : MonoBehaviour
{
	private Character m_Character;
	private Collider2D m_Collider;
	private CharacterMovementModel m_MovementModel;
	private InteractablePickup m_PickedUpObject;
	private InteractableGrabbable m_GrabbedObject;

	void Awake()
	{
		m_Character = GetComponent<Character>();
		m_Collider = GetComponent<Collider2D>();
		m_MovementModel = GetComponent<CharacterMovementModel>();
	}

	public void OnInteract(bool isHeldDown)
	{
		if( IsCarryingObject())
		{
			if (m_PickedUpObject.isThrowable && !isHeldDown)
			{
				ThrowCarryingObject();
			}
			else
			{
				PutDownCarryingObject();
			}
			return;
		}
		if( IsGrabbingObject() )
		{
			StopGrabbingObject(m_GrabbedObject);
			return;
		}

		InteractableBase usableInteractable = FindUsableInteractable();

		if( usableInteractable == null )
		{
			return;
		}

		usableInteractable.OnInteract( m_Character );
	}

	public Collider2D[] GetCloseColliders()
	{
		BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

		return Physics2D.OverlapAreaAll(
			(Vector2)transform.position + boxCollider.offset + boxCollider.size * 1.8f,
			(Vector2)transform.position + boxCollider.offset - boxCollider.size * 1.8f );
	}

	public InteractableBase FindUsableInteractable()
	{
		Collider2D[] closeColliders = GetCloseColliders();
		InteractableBase closestInteractable = null;
		float angleToClosestInteractble = Mathf.Infinity;

		for( int i = 0; i < closeColliders.Length; ++i )
		{
			InteractableBase colliderInteractable = closeColliders[ i ].GetComponent<InteractableBase>();

			if( colliderInteractable == null )
			{
				continue;
			}

			Vector3 directionToInteractble = closeColliders[ i ].transform.position - transform.position;

			float angleToInteractable = Vector3.Angle( m_MovementModel.GetFacingDirection(), directionToInteractble );

			if( angleToInteractable < 40 )
			{
				if( angleToInteractable < angleToClosestInteractble )
				{
					closestInteractable = colliderInteractable;
					angleToClosestInteractble = angleToInteractable;
				}
			}
		}

		return closestInteractable;
	}

	public void PickupObject( InteractablePickup pickupObject )
	{
		m_PickedUpObject = pickupObject;
		m_PickedUpObject.transform.parent = m_MovementModel.pickupItemParent;
		m_PickedUpObject.transform.localPosition = Vector3.zero;


		Collider2D pickupObjectCollider = pickupObject.GetComponent<Collider2D>();
		pickupObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

		if( pickupObjectCollider != null )
		{
			pickupObjectCollider.enabled = false;
		}
	}

	public void GrabObject( InteractableGrabbable grabbableObject, Character character)
	{
		m_GrabbedObject = grabbableObject;
        m_GrabbedObject.transform.parent = m_MovementModel.pickupItemParent;
		m_MovementModel.SetIsPushing(true);
		Vector3 facingDirection = m_MovementModel.GetFacingDirectionNoDiagonal();
		m_MovementModel.SetPushingDirection(facingDirection);
		if (grabbableObject.GetRestrict())
		{
			if (facingDirection == Helper.LEFT || facingDirection == Helper.RIGHT)
			{
				Debug.Log("facing left or right");
				m_MovementModel.RestrictYAxis(true);
			}
			else
			{
				Debug.Log("facing up or down");
				m_MovementModel.RestrictXAxis(true);
			}
		}
	}

	public void StopGrabbingObject(InteractableGrabbable grabbableObject)
	{
        m_GrabbedObject.transform.parent = transform.root;
		Collider2D grabObjectCollider = grabbableObject.GetComponent<Collider2D>();

		grabbableObject.SetIsGrabbing(false);

//		if( grabObjectCollider != null )
//		{
//			grabObjectCollider.isTrigger = false;
//		}

		m_MovementModel.SetIsPushing(false);
		m_GrabbedObject.SetIsGrabbing(false);
		m_MovementModel.RestrictXAxis(false);
		m_MovementModel.RestrictYAxis(false);
		m_GrabbedObject = null;
	}

	public void ThrowCarryingObject()
	{
		if (!Helper.IsTooCloseToWall(m_Character, 0.33f))
		{
			StartCoroutine(_ThrowCarryingObject());
		}
	}

	public bool IsCarryingObject()
	{
		return m_PickedUpObject != null;
	}

	public bool IsGrabbingObject()
	{
		return m_GrabbedObject != null;
	}

	IEnumerator _ThrowCarryingObject()
	{
		Collider2D pickupObjectCollider = m_PickedUpObject.GetComponent<Collider2D>();
		if( pickupObjectCollider != null )
		{
			Physics2D.IgnoreCollision( m_Collider, pickupObjectCollider );
			pickupObjectCollider.enabled = false;
		}

		m_PickedUpObject.Throw( m_Character );
		yield return new WaitForSeconds(0.2f);
		pickupObjectCollider.enabled = true;
		Physics2D.IgnoreCollision( m_Collider, pickupObjectCollider, false );
		m_PickedUpObject = null;
	}

	public void PutDownCarryingObject()
	{
        if (m_PickedUpObject != null && !Helper.IsTooCloseToWall(m_Character, 0.33f))
		{
			m_PickedUpObject.PutDown( m_Character );
			Collider2D pickupObjectCollider = m_PickedUpObject.GetComponent<Collider2D>();
			if( pickupObjectCollider != null )
			{
//				Physics2D.IgnoreCollision( m_Collider, pickupObjectCollider );
				pickupObjectCollider.enabled = true;
			}
			m_PickedUpObject = null;
		}
	}

	public InteractableGrabbable GetGrabbableObject()
	{
		return m_GrabbedObject;
	}

	public void OnPurpleReset()
	{
		if (IsGrabbingObject())
		{
			if (m_GrabbedObject.GetComponent<ResetBase>() != null)
			{
				m_GrabbedObject.GetComponent<ResetBase>().OnSectionReset();
			}
			StopGrabbingObject(m_GrabbedObject);
//			m_GrabbedObject.SetIsGrabbing(false);
//			m_GrabbedObject = null;
		}
		if (IsCarryingObject())
		{
//			PutDownCarryingObject();
//			ResetBase rb;
			if (m_PickedUpObject.GetComponent<ResetBase>() != null)
			{
				m_PickedUpObject.GetComponent<ResetBase>().OnSectionReset();
			}
			m_PickedUpObject.SetToStartParent();
			Collider2D pickupObjectCollider = m_PickedUpObject.GetComponent<Collider2D>();
			if( pickupObjectCollider != null )
			{
//				Physics2D.IgnoreCollision( m_Collider, pickupObjectCollider );
				pickupObjectCollider.enabled = true;
			}
			m_PickedUpObject = null;
//			m_PickedUpObject = null;
		}
	}

}