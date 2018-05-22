using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListener : MonoBehaviour 
{
	public GameObject[] triggers;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<TriggerBase>() != null)
		{
			TriggerBase tb = other.gameObject.GetComponent<TriggerBase>();
			if (tb.isProjectileTrigger)
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
