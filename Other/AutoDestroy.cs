﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

	public void DestroyThis()
	{
		Destroy(gameObject);
	}

	public void TurnOffSprite()
	{
		GetComponent<SpriteRenderer>().enabled = false;
	}
    
}