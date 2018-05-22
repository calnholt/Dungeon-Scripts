using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour 
{
	private ResetBase[] resets;

	void Start()
	{
		resets = GetComponentsInChildren<ResetBase>();
        //Debug.Log(this.gameObject.name + ": " + resets.Length);
	}

	public void ResetSection()
	{
        if (resets.Length > 0)
        {
			for (int i = 0; i < resets.Length; i++)
			{
				resets[i].OnSectionReset();
			}

		}
	}

}
