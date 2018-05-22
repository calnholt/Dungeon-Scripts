using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour 
{
    [SerializeField]
    private string scene;
    [SerializeField]
    private string levelName;
    [SerializeField]
    [Range(1,3)]
    private int difficulty;
    [SerializeField]
    private Sprite screenshot;
    [SerializeField]
    [Range(0, 3)]
    private int numOfCoins;

    private enum ItemTypes
    {
        Portal, Remote_Bomb, Warp_to_Shot, none
    }
    [SerializeField]
    private ItemTypes[] items;
    //[SerializeField]
    //private ItemTypes item2;


	public string GetLevelName()
    {
        return levelName;
    }

    public string GetScene()
    {
        return scene;
    }
	
    public int GetDifficulty()
    {
        return difficulty;
    }

    public string GetItem(int index)
    {
        if (items[index] == ItemTypes.Portal)
        {
            return "Portal";
        }
        else if (items[index] == ItemTypes.Warp_to_Shot)
        {
            return "Warp to Shot";
        }
        else if (items[index] == ItemTypes.Remote_Bomb)
        {
            return "Remote Bomb";
        }
        return null;
    }

    public string GetDifficultyString()
    {
        if (difficulty == 1)
            return "Easy";
        else if (difficulty == 2)
            return "Medium";
        else
            return "Hard";
    }

    public Sprite GetScreenshot()
    {
        return screenshot;
    }

    public int GetNumOfCoins()
    {
        return numOfCoins;
    }
}
