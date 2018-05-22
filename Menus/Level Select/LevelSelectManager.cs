using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour 
{
    public GameObject gLevelInfos;
    private LevelInfo[] levelInfos;
    public Image previewImage;
    public Text levelTxt;
    public Text itemTxt;
    public Text difficultyTxt;
    public Text coinsTxt;

    private int i = 0;

	void Start () 
    {
        levelInfos = gLevelInfos.GetComponents<LevelInfo>();
        DisplayLevelInfo();
	}

    void Update()
    {
        GetKeyboardInput();
    }


    void DisplayLevelInfo()
    {
        levelTxt.text = levelInfos[i].GetLevelName();
        string itemString = "Items: ";
        if (levelInfos[i].GetItem(0) != null)
            itemString += levelInfos[i].GetItem(0);
        if (levelInfos[i].GetItem(1) != null)
            itemString += ", " + levelInfos[i].GetItem(1);
        itemTxt.text = itemString;
        previewImage.sprite = levelInfos[i].GetScreenshot();
        difficultyTxt.text = "Difficulty: " + levelInfos[i].GetDifficulty();
        coinsTxt.text = "Coins: " + levelInfos[i].GetNumOfCoins();
    }

    public void NextLevel()
    {
        i++;
        //Debug.Log("i: " + i);
        if (i > levelInfos.Length-1)
            i = 0;
        DisplayLevelInfo();
    }


    public void PreviousLevel()
    {
        i--;
        //Debug.Log("i: " + i);
        if (i < 0)
            i = levelInfos.Length-1;
        DisplayLevelInfo();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelInfos[i].GetScene());
    }

    void GetKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            PreviousLevel();
        else if (Input.GetKeyDown(KeyCode.D))
            NextLevel();
        else if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.KeypadEnter))
            LoadLevel();
    }
	
}
