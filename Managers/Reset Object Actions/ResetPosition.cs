using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : ResetBase 
{
	private Vector3 startposition;

	void Start()
	{
		startposition = transform.position;
	}

	public override void OnSectionReset ()
	{
		transform.position = startposition;
	}

}
