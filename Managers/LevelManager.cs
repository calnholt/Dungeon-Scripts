using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour 
{
	private CheckpointManager cpm;
	public SectionManager[] sectionManagers;
    [HideInInspector]
    public CoinManager coinManager;
    private int totalCoins;
    private int collectedCoins = 0;
	private float timer;
	private bool isEnd = false;

	void Start () 
	{
        
		cpm = GetComponent<CheckpointManager>();
        coinManager = GetComponent<CoinManager>();
	}
	
	void Update () 
	{
		UpdateTime();
	}

	void UpdateTime()
	{
		timer += Time.deltaTime;
	}

	public float GetCurrentTime()
	{
		return timer;
	}

    IEnumerator _EndLevel()
    {
        if (!isEnd)
        {
            isEnd = true;
			yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Level Select");
        }
    }

	public void EndLevel()
	{
        StartCoroutine(_EndLevel());
	}

	public SectionManager[] GetSectionManagers()
	{
		return sectionManagers;
	}

}
