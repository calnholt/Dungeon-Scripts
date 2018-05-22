using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour 
{
    public static PlatformManager PM;
    public bool isMobile;

	void Start () 
    {
        if (PM == null)
        {
            PM = this;
        }
	}
	
	void Update () {
		
	}
}
