using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour 
{
    private LevelManager levelManager;
    public Character character;
    private EquipmentBaseModel[] equipment;
    private PrefabManager[] prefabs;

	public EquipType[] equipTypes;

	private const int WARP = 0;
    private const int PORTAL = 1;

	void Awake () 
    {
        levelManager = GetComponent<LevelManager>();
        //equipment = character.gameObject.GetComponents<EquipmentBaseModel>();
        prefabs = GetComponentsInChildren<PrefabManager>();
        SetEquipment();
	}

	void SetEquipment()
    {
        GameObject player = character.gameObject;
        for (int i = 0; i < equipTypes.Length; i++)
        {
            EquipmentBaseModel equipmentBase = null;
            if (equipTypes[i] == EquipType.Portal)
                equipmentBase = player.AddComponent<EquipmentPortalGun>();
            else if (equipTypes[i] == EquipType.Warp)
                equipmentBase = player.AddComponent<EquipmentWarpToShot>();
            else if (equipTypes[i] == EquipType.Bomb)
                equipmentBase = player.AddComponent<EquipmentRemoteBomb>();
			SetPrefabs(equipmentBase, i);
        }
    }

    private void SetPrefabs(EquipmentBaseModel _equipmentBase, int i)
    {
        for (int j = 0; j < prefabs.Length; j++)
        {
            if ((int)equipTypes[i] == prefabs[j].GetEquipment())
            {
                _equipmentBase.SetPrefabs(prefabs[j].prefabs);
            }
        }
    }

}
