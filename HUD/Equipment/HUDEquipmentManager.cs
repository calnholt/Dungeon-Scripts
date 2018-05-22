using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDEquipmentManager : HUDModel 
{
    private EquipmentManager equipmentManager;
    private HUDSpriteManager[] spriteManagers;
    public GameObject[] HUDEquipment;

	void Start () 
    {
        equipmentManager = levelManager.gameObject.GetComponent<EquipmentManager>();
        spriteManagers = GetComponentsInChildren<HUDSpriteManager>();
        SetHUDEquipment();
	}
	
	void SetHUDEquipment()
    {
		if (equipmentManager.equipTypes.Length == 1)
		{
			HUDEquipment[1].SetActive(false);
		}
        for (int i = 0; i < equipmentManager.equipTypes.Length; i++)
        {
            HUDEquipmentBase equipmentBase = null;
            if (equipmentManager.equipTypes[i] == EquipType.Portal)
            {
                equipmentBase = HUDEquipment[i].AddComponent<HUDPortalGun>();
            }
            else if (equipmentManager.equipTypes[i] == EquipType.Warp)
            {
                equipmentBase = HUDEquipment[i].AddComponent<HUDWarpToShot>();
            }
            else if (equipmentManager.equipTypes[i] == EquipType.Bomb)
                equipmentBase = HUDEquipment[i].AddComponent<HUDRemoteBomb>();
            SetSprites(equipmentBase, i);
            equipmentBase.equipmentNum = i;
        }
    }

    private void SetSprites(HUDEquipmentBase _equipmentBase, int i)
    {
        for (int j = 0; j < spriteManagers.Length; j++)
        {
            if (equipmentManager.equipTypes[i] == spriteManagers[j].equipType)
            {
                _equipmentBase.SetEquipmentHUDSprites(spriteManagers[j].sprites);
            }
        }
    }
}
