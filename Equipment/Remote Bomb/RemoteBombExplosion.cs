using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBombExplosion : MonoBehaviour 
{
    private bool enter = false;
	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.GetComponent(typeof(IRemoteBombExplosionTrigger)) as IRemoteBombExplosionTrigger != null && !enter)
        {
            IRemoteBombExplosionTrigger iTrigger = other.gameObject.GetComponent(typeof(IRemoteBombExplosionTrigger)) as IRemoteBombExplosionTrigger;
            iTrigger.OnRemoteBombExplosionTrigger();
			enter = true;
		}
	}
}
