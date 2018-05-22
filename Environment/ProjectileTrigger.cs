using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
	public GameObject[] triggers;
	public int color;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<TriggerBase>() != null)
		{
			if (other.gameObject.GetComponent<TriggerBase>().isProjectileTrigger && color == other.gameObject.GetComponent<TriggerBase>().color)
			{
				for (int i = 0; i < triggers.Length; i++)
				{
					IProjectileTrigger iProjectileTrigger = triggers[i].GetComponent(typeof(IProjectileTrigger)) as IProjectileTrigger;
					iProjectileTrigger.OnProjectileTrigger();
				}
			}
		}
	}
}
