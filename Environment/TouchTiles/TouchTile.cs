using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTile : MonoBehaviour, IEventTrigger
{
    public Color triggerColor = new Color(255 / 255f, 77 / 255f, 77 / 255f, 255 / 255f);
    public GameObject[] triggers;
    private bool entered = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isLocked = false;

    [Header("Settings")]
	public bool isLockedAfterOneTouch = false;

    [Header("On Press Actions")]
    public bool destroyEquipmentItems = false;

    [Header("What activates this touch tile?")]
    public bool isPlayerTrigger = true;
    public bool isCrateTrigger = false;
    public bool isRemoteBombTrigger = false;
    private bool isActive = false;
    private int numOfEnters = 0;
    private int numOfEntersMax = 0;

    [Header("Event Triggers")]
	public bool isLockedOnComplete = false;

	void Start () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        numOfEntersMax = GetNumOfEnters();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
        bool allowEnter = false;
        if (isLockedAfterOneTouch)
        {
            if (CheckTriggers(other.gameObject) && !isLocked)
            {
                if (destroyEquipmentItems)
                {
                    other.gameObject.GetComponent<Character>().DestroyEquipmentItems();
                }
                isActive = true;
                isLocked = true;
                spriteRenderer.color = triggerColor;
                OnTrigger();
            }
            return;
        }
        else if (numOfEnters != numOfEntersMax && !isLocked)
        {
            if (CheckTriggers(other.gameObject))
            {
                numOfEnters++;
                allowEnter = true;
            }
            if (numOfEnters > 0 && allowEnter)
            {
                isActive = true;
                if (triggers.Length > 0 && numOfEnters == 1)
                    OnTrigger();
                if (!isLocked)
                {
                    spriteRenderer.color = triggerColor;
                    entered = true;
                }
            }
        }
	}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isLockedAfterOneTouch && !isLocked)
        {
            Debug.Log("ontriggerexit() touchtile");
            if (numOfEnters != 0)
            {
                if (CheckTriggers(other.gameObject))
                    numOfEnters--;
                if (numOfEnters == 0)
                {
                    if (triggers.Length > 0)
                    {
                        isActive = false;
                        OnTrigger();
                        spriteRenderer.color = originalColor;
                        entered = false;
                    }
                }
            }
        }
    }


	public void ClearTouchTile()
    {
        entered = false;
        spriteRenderer.color = originalColor;
    }

    public bool GetEntered()
    {
        return entered;
    }

    public void SetLocked(bool toggle)
    {
        isLocked = toggle;
    }

    private void OnTrigger()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            IEventTrigger iEventTrigger = triggers[i].GetComponent(typeof(IEventTrigger)) as IEventTrigger;
            iEventTrigger.OnEventTrigger();
        }
    }

    private int GetNumOfEnters()
    {
        int counter = 0;
        if (isPlayerTrigger)
            counter++;
        if (isCrateTrigger)
            counter++;
        if (isRemoteBombTrigger)
            counter++;
        return counter;
    }

    public void OnSectionReset()
    {
        isLocked = false;
        isActive = false;
        spriteRenderer.color = originalColor;
        numOfEnters = 0;
    }

    private bool CheckTriggers(GameObject obj)
    {
        return ((obj.CompareTag("Player") && isPlayerTrigger) ||
                (obj.CompareTag("Crate") && isCrateTrigger) ||
                (obj.GetComponent<RemoteBomb>() != null && isRemoteBombTrigger));
    }

    public void OnEventTrigger()
    {
        if (isLockedOnComplete)
        {
            Debug.Log("OnEventTrigger touchtile");
            isLocked = true;
            spriteRenderer.color = triggerColor;
        }
    }

}
