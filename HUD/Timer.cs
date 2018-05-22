using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
	private HUDModel hud;
	private Text text;

	void Start () 
	{
		hud = GetComponentInParent<HUDModel>();
		text = GetComponentInChildren<Text>();
	}
	
	void Update () 
	{
		text.text = hud.levelManager.GetCurrentTime().ToString("00:00.00");
	}
}
