using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButton : MonoBehaviour 
{
	private bool isHeldDown;

    void Start()
    {
        isHeldDown = false;
    }

	public void PressedDown()
	{
		isHeldDown = true;
	}
	
	public void NotPressedDown()
	{
		isHeldDown = false;
	}

	public bool IsPressedDown()
	{
		return isHeldDown;
	}
		
}
