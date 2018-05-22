using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTouchTile : ResetBase 
{
    private TouchTileManager touchTileManager;
    private TouchTile touchTile;

	void Awake () 
    {
        touchTileManager = GetComponent<TouchTileManager>();
        touchTile = GetComponent<TouchTile>();

	}
	
    public override void OnSectionReset()
    {
        if (touchTileManager != null)
            touchTileManager.OnSectionReset();
        else
            touchTile.OnSectionReset();
    }
}
