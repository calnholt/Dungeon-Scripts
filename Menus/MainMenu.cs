using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.KeypadEnter))
            LoadLevelSelect();
	}

	public void LoadLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
	
}
