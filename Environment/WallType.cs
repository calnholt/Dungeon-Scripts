using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallType : MonoBehaviour {

	public string sectionName;
	[Header("What can pass through?")]
	public bool portalShot;
	public bool player;
	public bool bomb;
	public bool warpShot;
	[Header("Triggers")]
	public bool projectileTrigger;
	public bool destroyEquipmentItems;
	public bool resetLevelItems;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Character character = other.gameObject.GetComponent<Character>();
			if (destroyEquipmentItems)
			{
				EquipmentBaseModel[] ebm = character.GetComponents<EquipmentBaseModel>();
				for (int i = 0; i < ebm.Length; i++)
				{
					ebm[i].DestroyItems();
				}
			}
			if (resetLevelItems)
			{
				character.Interaction.OnPurpleReset();
			}
		}
	}

}
