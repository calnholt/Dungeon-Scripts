using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMovingPlatform : MonoBehaviour 
{

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent(typeof(IMovingPlatform<Transform>)) as IMovingPlatform<Transform> != null)
        {
            IMovingPlatform<Transform> iMove = other.gameObject.GetComponent(typeof(IMovingPlatform<Transform>)) as IMovingPlatform<Transform>;
            iMove.EnterMovingPlatform(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent(typeof(IMovingPlatform<Transform>)) as IMovingPlatform<Transform> != null)
        {
            IMovingPlatform<Transform> iMove = other.gameObject.GetComponent(typeof(IMovingPlatform<Transform>)) as IMovingPlatform<Transform>;
            iMove.ExitMovingPlatform(transform);
        }
    }
}
