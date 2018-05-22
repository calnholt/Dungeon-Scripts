using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCoins : HUDModel 
{
    private Image[] images;
    private bool areAllCollected = false;
    private int numOfCoins;
    private Color collectedColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
    private Color notCollectedColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 127 / 255f);

	private void Start()
	{
        images = GetComponentsInChildren<Image>();
        numOfCoins = levelManager.coinManager.coins.Length;
        for (int i = 0; i < images.Length; i++)
        {
            if (i < numOfCoins)
            {
                images[i].color = notCollectedColor;
            }
            else
            {
                images[i].enabled = false;
            }
        }
	}

	private void Update()
	{
        if (!areAllCollected)
        {
            CheckForCoinCollect();
        }
	}

	private void CheckForCoinCollect()
    {
        for (int i = 0; i < numOfCoins; i++)
        {
            if (levelManager.coinManager.coins[i].IsCollected())
            {
                images[i].color = collectedColor;
            }
            else
            {
                images[i].color = notCollectedColor;
            }
        }
    }

}
