using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour 
{
	public Color inactiveColor;
	private Color activeColor;
	private int checkpointNum;
	private bool isActive = false;
	private bool isEnd = false;
	private SpriteRenderer sr;
	private Animator anim;
	private CheckpointManager cpm;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		Color color = new Color(sr.color.r, sr.color.b, sr.color.b, 255f/255f);
		activeColor = color;
        //cpm = GameObject.Find("Managers").GetComponent<CheckpointManager>();
		cpm = GetComponentInParent<CheckpointManager>();
		anim = GetComponent<Animator>();
		if (checkpointNum == 0)
		{
			isActive = true;
		}
		else
		{
			SetActive(false);
		}
	}

	public void SetCheckpointNumber(int number)
	{
		checkpointNum = number;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") && !isActive)
		{
			SetActive(true);
			if (isEnd)
			{
				cpm.EndLevel();
			}
            return;
        }
	}

	void SetActive(bool _isActive)
	{
		isActive = _isActive;
		if (isActive)
		{
			isActive = true;
			anim.enabled = true;
			cpm.SetCurrentCheckpoint(checkpointNum);
			sr.color = activeColor;
		}
		else
		{
			sr.color = inactiveColor;
			anim.enabled = false;
		}
	}

	public void SetEnd()
	{
		isEnd = true;
	}


}
