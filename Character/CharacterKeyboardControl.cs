using UnityEngine;
using System.Collections;

public class CharacterKeyboardControl : CharacterBaseControl 
{
	void Start() 
	{
        if (PlatformManager.PM.isMobile)
            enabled = false;
        else 
		    SetDirection( new Vector2( 0, -1 ) );
	}

	void Update() 
	{
		UpdateDirection();
		UpdateAction();
		UpdateCamera();
	}

	void UpdateAction()
	{

		ActionWithHoldCheck(KeyCode.H, 0, false);
		ActionWithHoldCheck(KeyCode.J, 1, true);
		ActionWithHoldCheck(KeyCode.K, 2, true);

	}

	void UpdateDirection()
	{
		Vector2 newDirection = Vector2.zero;

		if( Input.GetKey( KeyCode.W ) )
		{
			newDirection.y = 1;
		}

		if( Input.GetKey( KeyCode.S ) )
		{
			newDirection.y = -1;
		}

		if( Input.GetKey( KeyCode.A ) )
		{
			newDirection.x = -1;
		}

		if( Input.GetKey( KeyCode.D ) )
		{
			newDirection.x = 1;
		}

		SetDirection( newDirection );

	}

	void UpdateCamera()
	{
		Vector2 newDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 

		if( Input.GetKey( KeyCode.Slash) )
		{
			ResetCamera();
		}

		SetCameraDirection( newDirection );
	}

	protected void ActionWithHoldCheck(KeyCode keycode, int _selection, bool _isEquipment)
	{
		if (Input.GetKey( keycode ) && canUse[_selection])
		{
			holdTimers[_selection] += Time.deltaTime;
			if (holdTimers[_selection] > holdTimerThreshold)
			{
				if (_isEquipment)
				{
					OnEquipmentPressed(_selection-1, true);
				}
				else
				{
					OnInteractPressed(true);
				}
				canUse[_selection] = false;
			}
		}

		else if (Input.GetKeyUp(keycode))
		{
			if (holdTimers[_selection] > 0 && canUse[_selection])
			{
				if (_isEquipment)
				{
					OnEquipmentPressed(_selection-1, false);
				}
				else
				{
					OnInteractPressed(false);
				}
			}
			holdTimers[_selection] = 0;
			canUse[_selection] = true;
		}
	}
}