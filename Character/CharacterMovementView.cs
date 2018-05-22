using UnityEngine;
using System.Collections;

public class CharacterMovementView : MonoBehaviour
{
	public Animator Animator;

	private CharacterMovementModel m_MovementModel;

	void Awake()
	{
		m_MovementModel = GetComponent<CharacterMovementModel>();

		if( Animator == null )
		{
			Debug.LogError( "Character Animator is not setup!" );
			enabled = false;
		}
	}

	void Start()
	{
	}

	public void Update() 
	{
		UpdateDirection();   
	}
		
	void UpdateDirection()
	{
		Vector3 direction;

		if (m_MovementModel.IsDead())
		{
			Animator.SetFloat( "DirectionX", 0 );
			Animator.SetFloat( "DirectionY", -1 );
			Animator.SetBool( "IsMoving", false );
			return;
		}

		if (m_MovementModel.IsPushing())
		{
			direction = m_MovementModel.GetPushingDirection();
		}
		else
		{
			direction = m_MovementModel.GetDirection();
		}
			
		if( direction != Vector3.zero )
		{
            if (direction.x > 0 && direction.y == 0)
            {
                Animator.SetFloat("DirectionX", 1);
                Animator.SetFloat("DirectionY", 0);
            }
            else if (direction.x < 0 && direction.y == 0)
            {
                Animator.SetFloat("DirectionX", -1);
                Animator.SetFloat("DirectionY", 0);
            }
            else if (direction.x == 0 && direction.y > 0)
            {
                Animator.SetFloat("DirectionX", 0);
                Animator.SetFloat("DirectionY", 1);
            }
            else if (direction.x == 0 && direction.y < 0)
            {
                Animator.SetFloat("DirectionX", 0);
                Animator.SetFloat("DirectionY", -1);
            }
            else if ((direction.x > 0 && direction.y > 0) || (direction.x > 0 && direction.y < 0))
            {
                Animator.SetFloat("DirectionX", 1);
                Animator.SetFloat("DirectionY", 0);
            }
            else if ((direction.x < 0 && direction.y > 0) || (direction.x < 0 && direction.y < 0))
            {
                Animator.SetFloat("DirectionX", -1);
                Animator.SetFloat("DirectionY", 0);
            }
		}
		
		Animator.SetBool( "IsMoving", m_MovementModel.IsMoving() );

	}


}