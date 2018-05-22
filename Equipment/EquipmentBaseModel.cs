using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBaseModel : MonoBehaviour
{
	[HideInInspector]
	public Character character;

	void Awake()
	{
		character = GetComponent<Character>();
	}

	virtual public void OnEquipmentAction(Character character, bool isHoldDown)
	{
		Debug.LogWarning( "OnInteract is not implemented" );
	}

	virtual public void DestroyItems()
	{
		Debug.LogWarning( "DestroyItems is not implemented" );
	}

	virtual public bool GetHUDStatus()
	{
		Debug.LogWarning( "GetHUDStatus is not implemented" );
		return false;
	}

    virtual public void SetPrefabs(GameObject[] prefabs)
    {
        Debug.LogWarning("SetPrefabs is not implemented");
    }
}
