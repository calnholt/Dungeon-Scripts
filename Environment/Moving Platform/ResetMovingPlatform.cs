using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMovingPlatform : ResetBase 
{
    private MovingPlatform movingPlatform;

	void Start () 
    {
        movingPlatform = GetComponent<MovingPlatform>();
	}

	public override void OnSectionReset()
	{
        movingPlatform.SetIsMoving(false);
        movingPlatform.SetOriginalPosition();
	}
}
