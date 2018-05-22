using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour 
{
	public GameObject[] checkpoints;
	public GameObject endPoint;
	private int currentCheckpoint = 0;
	private LevelManager lm;
	private int totalNumOfCheckpoints;
	private int numOfActivatedCheckpoints;

	void Awake()
	{
		SetCheckpoints();
		lm = GetComponent<LevelManager>();
	}

	public void SetCurrentCheckpoint(int index)
	{
		currentCheckpoint = index;
		numOfActivatedCheckpoints++;
	}

	public Vector3 GetCurrentCheckpointPosition()
	{
        return checkpoints[currentCheckpoint].transform.position;
	}

	public int GetCurrentCheckpoint()
	{
		return currentCheckpoint;
	}

	private void SetCheckpoints()
	{
        int i;
		for (i = 0; i < checkpoints.Length; i++)
		{
			checkpoints[i].GetComponent<Checkpoint>().SetCheckpointNumber(i);
		}
		endPoint.GetComponent<Checkpoint>().SetEnd();
        endPoint.GetComponent<Checkpoint>().SetCheckpointNumber(i);
		totalNumOfCheckpoints = checkpoints.Length;
	}

	public void EndLevel()
	{
		lm.EndLevel();
	}

}
