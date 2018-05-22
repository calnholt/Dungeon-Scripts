using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCoin : ResetBase 
{

    private Coin coin;

	void Start () 
    {
        coin = GetComponent<Coin>();	
	}
	
    public override void OnSectionReset()
    {
        coin.OnSectionReset();
    }
	
}
