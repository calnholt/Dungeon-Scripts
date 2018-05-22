using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDirection : MonoBehaviour 
{
	private TouchDpadManager tdm;
	public Vector3 direction;
	private bool toggleCamera = false;

	void Start () 
	{
		tdm = GetComponentInParent<TouchDpadManager>();
//		direction.Normalize();
	}
	
	public Vector3 GetDirection()
	{
		return direction;
	}
		
	public void SetDirection()
	{
		tdm.SetDirection(direction);
	}

	public void NoDirection()
	{
		tdm.SetDirection(Vector3.zero);
	}

	public bool IsCameraToggled()
	{
		return tdm.IsCameraToggled();
	}

}
