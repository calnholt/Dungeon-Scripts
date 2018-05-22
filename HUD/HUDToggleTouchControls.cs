using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDToggleTouchControls : MonoBehaviour {

    public GameObject touchControls;

	void Start () 
    {
        if (PlatformManager.PM.isMobile)
            touchControls.SetActive(true);
        else
            touchControls.SetActive(false);
	}

}
