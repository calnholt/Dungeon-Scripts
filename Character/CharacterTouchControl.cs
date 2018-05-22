using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTouchControl : CharacterBaseControl 
{
	public TouchDpadManager touch;
	public TouchButton[] touchButton;

	void Start() 
	{
        if (!PlatformManager.PM.isMobile)
            enabled = false;
        else 
		    SetDirection( new Vector2( 0, -1 ) );
	}
	
	void Update() 
	{
		UpdateDirection();
		UpdateCamera();
		ActionWithHoldCheck(touchButton[0], 1, true);
        if (touchButton[1] != null)
		    ActionWithHoldCheck(touchButton[1], 2, true);
        ActionWithHoldCheck(touchButton[2], 0, false);
	}

	void UpdateDirection()
	{
		if (!touch.IsCameraToggled())
		{
			SetDirection( touch.GetDirection()/2);
		}
	}

	void UpdateCamera()
	{
		if (touch.IsCameraToggled())
		{
			SetCameraDirection( touch.GetDirection() );
		}
        if (touch.IsCameraReset())
        {
            ResetCamera();
            touch.ToggleCameraReset(false);
        }
	}

	protected void ActionWithHoldCheck(TouchButton touchButton, int _selection, bool _isEquipment)
	{
		if (touchButton.IsPressedDown() && canUse[_selection])
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

		else if (!touchButton.IsPressedDown())
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
