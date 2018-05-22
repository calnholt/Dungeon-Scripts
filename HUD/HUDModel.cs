using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDModel : MonoBehaviour 
{
    [HideInInspector]
    public Character character;
    [HideInInspector]
	public LevelManager levelManager;

    void Awake()
    {
        character = GameObject.FindWithTag("Player").GetComponent<Character>();

        levelManager = GameObject.Find("Managers").GetComponent<LevelManager>();
	}
}
