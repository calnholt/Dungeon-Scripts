using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType
{
	None, Portal, Warp, Bomb
}
public class PrefabManager : MonoBehaviour 
{
    public EquipType equipType;

    public GameObject[] prefabs;

    public int GetEquipment()
    {
        return (int)equipType;
    }
}
