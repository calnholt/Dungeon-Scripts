using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDEquipmentBase : MonoBehaviour 
{
	[HideInInspector]
	public HUDModel hud;
	[HideInInspector]
	public int equipmentNum;
	[HideInInspector]
	public bool previousCheck;

	void Awake () 
	{
		hud = GetComponentInParent<HUDModel>();
		previousCheck = false;
	}

	virtual public void UpdateEquipmentHUD()
	{
		Debug.LogWarning( "UpdateEquipmentHUD is not implemented" );
	}

    virtual public void SetEquipmentHUDSprites(Sprite[] sprites)
    {
        Debug.LogWarning("UpdateEquipmentHUD is not implemented");
    }
}
