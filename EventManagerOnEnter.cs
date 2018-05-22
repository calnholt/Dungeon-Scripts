using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerOnEnter : ResetBase {

    public GameObject[] triggers;
    private bool isActivated = false;

    public bool destroyEquipmentItems = false;

	public override void OnSectionReset()
	{
        isActivated = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            Character character = collision.gameObject.GetComponent<Character>();
            if (destroyEquipmentItems)
                character.DestroyEquipmentItems();
            for (int i = 0; i < triggers.Length; i++)
            {
                IEventTrigger iEventTrigger = triggers[i].GetComponent(typeof(IEventTrigger)) as IEventTrigger;
                iEventTrigger.OnEventTrigger();
            }
        }
	}

}
