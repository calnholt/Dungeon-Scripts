using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPortalGun : EquipmentBaseModel
{
	public GameObject bluePortalShotPrefab;
	public GameObject redPortalShotPrefab;
	public GameObject portalPrefab;

	private GameObject currentPortalShot;
	private GameObject[] activePortals = new GameObject[2] {null, null};

	public bool isWarping = false;

	private const int BLUE = 0;
	private const int RED = 1;

	private bool isBlue = true;
//	private bool firstShot = true;

	public override void OnEquipmentAction(Character character, bool isHoldDown)
	{
		if (currentPortalShot == null && !isHoldDown)
		{
            ShootPortalShot();
		}
        else if (currentPortalShot != null && !isHoldDown)
        {
            Destroy(currentPortalShot);
            ShootPortalShot();
        }
		else if (isHoldDown)
		{
			SwitchColor();
		}
	}

	public void SpawnPortal(Vector3 portalPosition, PortalWall portalWall)
	{
		SwitchColor();
		int color = RED;
		if (!isBlue)
			color = BLUE;
		{
			Destroy(activePortals[color]);
			activePortals[color] = Instantiate(portalPrefab, portalWall.gameObject.transform);
			activePortals[color].transform.position = portalPosition;
			activePortals[color].GetComponent<Portal>().SetVariables(portalWall.GetDirection(), color, this);
		}
	}

	public void WarpPlayer(int color)
    {
        Debug.Log("Warp Player");
		Portal otherPortal = GetOtherPortal(color);
		otherPortal.SetPlayerEnterTrigger(false);
		character.CenterCamera(character, otherPortal.transform.position, false);
        character.transform.position = otherPortal.transform.position + (otherPortal.GetNewDirection()/8);
	}

	public void WarpObject(int color, GameObject obj)
	{
        Debug.Log("Warp Object");
		Portal otherPortal = GetOtherPortal(color);
		otherPortal.SetObjectEnterTrigger(false);
		if (obj.name == "Warp Shot(Clone)")
		{
			obj.transform.position = otherPortal.transform.position + (otherPortal.GetNewDirection()/5);
		}
		else
			obj.transform.position = otherPortal.transform.position + (otherPortal.GetNewDirection()/5);
		IEnterPortal<Vector3> iEnterPortal = obj.GetComponent(typeof(IEnterPortal<Vector3>)) as IEnterPortal<Vector3>;
		iEnterPortal.EnterPortal(otherPortal.GetNewDirection());
	}

	public void TogglePlayerPortals(bool toggle)
	{
		for (int i = 0; i < activePortals.Length; i++)
		{
			activePortals[i].GetComponent<Portal>().SetPlayerEnterTrigger(toggle);
			activePortals[i].GetComponent<Portal>().SetPlayerExitTrigger(toggle);
		}
	}

	public void ToggleObjectPortals(bool toggle)
	{
		for (int i = 0; i < activePortals.Length; i++)
		{
			activePortals[i].GetComponent<Portal>().SetObjectEnterTrigger(toggle);
			activePortals[i].GetComponent<Portal>().SetObjectExitTrigger(toggle);
		}
	}

	public void SwitchColor()
	{
		isBlue = !isBlue;
	}

	public bool AreTwoPortalsActive()
	{
		return (activePortals[RED] != null && activePortals[BLUE] != null);
	}

	public Portal GetOtherPortal(int color)
	{
		if (color == BLUE)
			return activePortals[RED].GetComponent<Portal>();
		else
			return activePortals[BLUE].GetComponent<Portal>();
	}

	public override void DestroyItems()
	{
		for (int i = 0; i < activePortals.Length; i++)
		{
			Destroy(activePortals[i]);
		}
	}

	public override bool GetHUDStatus()
	{
		return isBlue;
	}

    private void ShootPortalShot()
    {
        if (!Helper.IsTooCloseToWall(character, 0.1f))
        {
            if (isBlue)
            {
                currentPortalShot = Instantiate(bluePortalShotPrefab);
            }
            else
            {
                currentPortalShot = Instantiate(redPortalShotPrefab);
            }

            currentPortalShot.transform.position = character.transform.position;
            currentPortalShot.GetComponent<PortalShot>().pg = this;
        }
    }

	public override void SetPrefabs(GameObject[] prefabs)
	{
        bluePortalShotPrefab = prefabs[0];
        redPortalShotPrefab = prefabs[1];
        portalPrefab = prefabs[2];
	}

}
