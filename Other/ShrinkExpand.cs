using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkExpand : MonoBehaviour 
{

	public float changeX, changeY, changeZ;
	public float sizeMin, sizeMax;

	private bool isIncreasing;

	void Update () 
	{
		ExpandContract();
	}

	void ExpandContract()
	{
		if (isIncreasing)
		{
			transform.localScale += new Vector3(changeX, changeY, changeZ);
			if (transform.localScale.x > sizeMax)
			{
				isIncreasing = false;
			}
		}
		else
		{
			transform.localScale -= new Vector3(changeX, changeY, changeZ);
			if (transform.localScale.x < sizeMin)
			{
				isIncreasing = true;
			}
		}
	}

    public void SetValues(float _changeX,float  _changeY, float _changeZ, float _sizeMin, float _sizeMax)
    {
        changeX = _changeX;
        changeY = _changeY;
        changeZ = _changeZ;
        sizeMin = _sizeMin;
        sizeMax = _sizeMax;
    }
}
