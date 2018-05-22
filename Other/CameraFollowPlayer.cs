using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour 
{
	public enum CameraPosition
	{
		xyz, x, y, xy, none
	}
	public enum CameraRotation
	{
		z, none
	}
	public GameObject player;       //Public variable to store a reference to the player game object
	private Vector3 offset;         //Private variable to store the offset distance between the player and camera
	public CameraPosition positionType;
	public CameraRotation rotationType;
//	public bool lookAt;

	void Start () 
	{
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - player.transform.position;
	}

	// LateUpdate is called after Update each frame
	void Update () 
	{
		switch (positionType)
		{
			case (CameraPosition.y):
			transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
			break;

			case (CameraPosition.xy):
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
			break;
		}

//		switch (rotationType)
//		{
//			case (CameraRotation.z):
//			transform.localRotation = new Quaternion(transform.eulerAngles.x, transform.eulerAngles.y, player.transform.eulerAngles.z, 0f);
//			break;
//		}
//		if(lookAt)
//		{
//			transform.LookAt(player.transform);
//		}
//		else
//		{
//			transform.rotation = player.transform.rotation;
//		}

		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
//		transform.position = player.transform.position + offset;
//		transform.position = player.transform.position;
	}
}