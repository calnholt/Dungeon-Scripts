using UnityEngine;
using System.Collections;

public class CharacterBaseControl : MonoBehaviour
{
	protected Character character;
	protected CharacterMovementModel m_MovementModel;
	protected CharacterInteractionModel m_InteractionModel;
	protected CharacterMovementView m_MovementView; 
	protected EquipmentBaseModel[] m_EquipmentModel;

	public float holdTimerThreshold;
	public float buttonUpThreshold;
	protected float[] holdTimers;
	protected bool[] canUse;

	void Awake()
	{
		character = GetComponent<Character>();
		m_MovementModel = GetComponent<CharacterMovementModel>();
		m_MovementView = GetComponent<CharacterMovementView>();
		m_InteractionModel = GetComponent<CharacterInteractionModel>();
		m_EquipmentModel = GetComponents<EquipmentBaseModel>();
		holdTimers = new float[3];
		canUse = new bool[3] {true, true, true};
	}
		
	protected void SetDirection( Vector2 direction )
	{
		if( m_MovementModel == null )
		{
			return;
		}

		m_MovementModel.SetDirection( direction );
	}

	protected void OnInteractPressed(bool isHoldDown)
	{
		if (m_InteractionModel == null)
		{
			return;
		}
		m_InteractionModel.OnInteract(isHoldDown);
		
	}

	protected void OnEquipmentPressed(int selection, bool isHoldDown)
	{
		if (m_EquipmentModel[selection] == null)
		{
			return;
		}
		m_EquipmentModel[selection].OnEquipmentAction(character, isHoldDown);
	}

	protected void SetCameraDirection(Vector2 direction)
	{
		if( m_MovementModel == null )
		{
			return;
		}

		m_MovementModel.SetCameraDirection( direction );
	}

	protected void ResetCamera()
	{
		if( m_MovementModel == null )
		{
			return;
		}

		m_MovementModel.ResetCamera();
	}


		
}