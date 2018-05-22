using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour 
{
	public Vector3 velocity;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.GetComponent(typeof(IPushable<Vector3>)) as IPushable<Vector3> != null)
		{
			IPushable<Vector3> iPushable = other.gameObject.GetComponent(typeof(IPushable<Vector3>)) as IPushable<Vector3>;
			iPushable.Push(velocity);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.GetComponent(typeof(IPushable<Vector3>)) as IPushable<Vector3> != null)
		{
			IPushable<Vector3> iPushable = other.gameObject.GetComponent(typeof(IPushable<Vector3>)) as IPushable<Vector3>;
			iPushable.Push(Vector3.zero);
		}
	}
}
