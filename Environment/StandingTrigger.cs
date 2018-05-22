using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingTrigger : MonoBehaviour 
{
	public GameObject[] triggers;


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			for (int i = 0; i < triggers.Length; i++)
			{
				IStandingTrigger iStandStrigger = triggers[i].GetComponent(typeof(IStandingTrigger)) as IStandingTrigger;
				iStandStrigger.OnStandTrigger();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			for (int i = 0; i < triggers.Length; i++)
			{
				IStandingTrigger iStandStrigger = triggers[i].GetComponent(typeof(IStandingTrigger)) as IStandingTrigger;
				iStandStrigger.OnStandTrigger();
			}
		}
	}
}
