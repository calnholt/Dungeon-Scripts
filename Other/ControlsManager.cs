using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour 
{
	public static ControlsManager CM;
	[SerializeField]
	private GameObject controlGameObject;
	[HideInInspector]
	public Controls controls;

	public void SetControls()
	{
		controls.horizontal = "Horizontal";
		controls.vertical = "Vertical";
		controls.interact = "Interact";
		controls.s1 = "Special1";
		controls.s2 = "Special2";
	}

	void Awake()
	{
		if (CM == null)
		{
			CM = this;
		}
		controls = controlGameObject.GetComponent<Controls>();
	}

	void Start()
	{
		SetControls();
	}
}
