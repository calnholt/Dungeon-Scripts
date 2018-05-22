using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRemoteBombButton : ResetBase 
{
    private RemoteBombButton button;

	void Awake () 
    {
        button = GetComponent<RemoteBombButton>();	
	}

	public override void OnSectionReset()
	{
        button.OnSectionReset();
	}
}
