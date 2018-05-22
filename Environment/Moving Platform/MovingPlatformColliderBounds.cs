using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformColliderBounds : MonoBehaviour 
{



	private GameObject[] movingPlatformObj;
	private Collider2D[] objColl;

	private PolygonCollider2D polyColl;

	private const int TOP_RIGHT = 0;
	private const int TOP_LEFT = 1;
	private const int BOTTOM_LEFT = 2;
	private const int BOTTOM_RIGHT = 3;

	void Start()
	{
        MovingPlatform[] movingPlatforms = GetComponentsInChildren<MovingPlatform>();
        int length = movingPlatforms.Length;
        movingPlatformObj = new GameObject[length];
        objColl = new Collider2D[length];
        for (int i = 0; i < length; i++)
        {
            movingPlatformObj[i] = movingPlatforms[i].gameObject;
			objColl[i] = movingPlatformObj[i].GetComponent<Collider2D>();
        }
        polyColl = GetComponent<PolygonCollider2D>();
	}

	void Update()
	{
		SetPath();
	}

	private void SetPath()
	{
        for (int i = 0; i < objColl.Length; i++)
        {
            Vector2[] corners = new Vector2[4];
            corners[TOP_RIGHT] = objColl[i].bounds.max;
            corners[TOP_LEFT] = new Vector2(objColl[i].bounds.min.x, objColl[i].bounds.max.y);
            corners[BOTTOM_LEFT] = objColl[i].bounds.min;
            corners[BOTTOM_RIGHT] = new Vector2(objColl[i].bounds.max.x, objColl[i].bounds.min.y);
            polyColl.SetPath(i+1, corners);
        }
	}

}
