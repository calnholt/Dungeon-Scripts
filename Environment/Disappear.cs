using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : ResetBase, IStandingTrigger, IProjectileTrigger, IEventTrigger
{
    #region IProjectileTrigger implementation

    public bool isDisabledAtStart = false;

    void Start()
    {
        if (isDisabledAtStart)
            gameObject.SetActive(false);
    }

	public override void OnSectionReset()
	{
        if (isDisabledAtStart)
        {
            gameObject.SetActive(false);
        }
	}

	public void OnProjectileTrigger ()
	{
		ToggleEnabled();
	}

	#endregion

	public void OnStandTrigger()
	{
		ToggleEnabled();
	}

    public void OnEventTrigger()
    {
        ToggleEnabled();
    }

	public void ToggleEnabled()
	{
		if (gameObject.activeInHierarchy)
			gameObject.SetActive(false);
		else
			gameObject.SetActive(true);
        for (int i = 0; i < 2; i++)
        {
            if (transform.Find("Portal(Clone)") != null)
            {
                Debug.Log("enter destroy portal");
                Destroy(transform.Find("Portal(Clone)").gameObject);
            }
        }
	}


}
