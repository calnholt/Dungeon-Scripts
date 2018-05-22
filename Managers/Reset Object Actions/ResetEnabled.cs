using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEnabled : ResetBase 
{
	private bool isEnabled;

	void Start () 
	{
		isEnabled = isActiveAndEnabled;
	}

	public override void OnSectionReset ()
	{
		gameObject.SetActive(isEnabled);
	}

}
