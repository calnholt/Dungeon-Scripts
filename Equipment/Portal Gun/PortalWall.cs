using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalWall : MonoBehaviour 
{
	[Header("N = 0, E = 1, S = 2, W = 3")]
	[Range(0,3)]
	[SerializeField]
	private int direction;

	public int GetDirection()
	{
		return direction;
	}
}
