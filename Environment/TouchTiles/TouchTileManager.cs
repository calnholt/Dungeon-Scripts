using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTileManager : MonoBehaviour
{
	public float offset = 0.3f;
    public GameObject[] triggers;
    public bool lockOnComplete = true;
    private TouchTile[] touchTile;
    private Collider2D[] touchTileColliders;
    private PolygonCollider2D polyColl;
	private bool allEnabled;

    private const int BOTTOM_RIGHT = 0; //
    private const int BOTTOM_LEFT = 1;
    private const int TOP_LEFT = 2; //
    private const int TOP_RIGHT = 3;

	private void Start()
	{
		touchTile = GetComponentsInChildren<TouchTile>();
        touchTileColliders = new Collider2D[touchTile.Length];
		for (int i = 0; i < touchTile.Length; i++)
        {
			touchTileColliders[i] = touchTile[i].GetComponent<Collider2D>();
        }
        polyColl = GetComponent<PolygonCollider2D>();
        SetPath();
	}
   
    private void Update()
    {
        if (CheckAllEnabled() && !allEnabled)
        {
			allEnabled = true;
            if (lockOnComplete)
            {
                ToggleLocked(allEnabled);
            }
            OnTrigger();
        }
    }

    private void SetPath()
    {
        for (int i = 0; i < touchTileColliders.Length; i++)
        {
            Vector2[] corners = new Vector2[4];
            corners[TOP_RIGHT] = touchTileColliders[i].bounds.max + new Vector3(offset, offset, 0);
            corners[BOTTOM_RIGHT] = new Vector2(touchTileColliders[i].bounds.max.x + offset, touchTileColliders[i].bounds.min.y - offset); //
            corners[BOTTOM_LEFT] = touchTileColliders[i].bounds.min - new Vector3(offset, offset, 0);
            corners[TOP_LEFT] = new Vector2(touchTileColliders[i].bounds.min.x - offset, touchTileColliders[i].bounds.max.y + offset); // 
            polyColl.SetPath(i + 1, corners);
        }
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            if (!allEnabled || (allEnabled && !lockOnComplete))
            {
                if (allEnabled && !lockOnComplete)
                    OnTrigger();
                for (int i = 0; i < touchTile.Length; i++)
                {
                    touchTile[i].ClearTouchTile();
                    allEnabled = false;
                }
            }
        }
	}
    
    private bool CheckAllEnabled()
    {
        for (int i = 0; i < touchTile.Length; i++)
        {
            if (!touchTile[i].GetEntered())
                return false;
        }
        return true;
    }

    private void ToggleLocked(bool toggle)
    {
        for (int i = 0; i < touchTile.Length; i++)
        {
            touchTile[i].SetLocked(toggle);
            if (!toggle)
                touchTile[i].ClearTouchTile();
        }
    }

    public void OnSectionReset()
    {
		allEnabled = false;
        ToggleLocked(false);
    }

    private void OnTrigger()
	{
        for (int i = 0; i < triggers.Length; i++)
        {
            IEventTrigger iEventStrigger = triggers[i].GetComponent(typeof(IEventTrigger)) as IEventTrigger;
            iEventStrigger.OnEventTrigger();
        }
	}

}
