using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour 
{
    public Coin[] coins;
    private int coinCounter = 0;

	private void Start()
	{
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].SetCoinManager(this);
        }
	}

	public void CollectCoin()
    {
        coinCounter++;
    }

    public void ResetCoin()
    {
        coinCounter--;
    }

    public bool AreAllCollected()
    {
        return coinCounter == coins.Length;
    }


}
