using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformEventManager : MonoBehaviour, IResetSection
{
    public MovingPlatform[] movingPlatforms;
    public bool isStartMovingTrigger;


	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < movingPlatforms.Length; i++)
            {
                if (isStartMovingTrigger)
                    movingPlatforms[i].SetIsMoving(true);
            }
        }
	}

    public void OnResetSection()
    {
        for (int i = 0; i < movingPlatforms.Length; i++)
        {
            if (isStartMovingTrigger)
                movingPlatforms[i].SetIsMoving(false);
            
        }
    }

}
