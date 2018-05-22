using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWarpToShot : EquipmentBaseModel
{
	public GameObject warpShotPrefab;

	private GameObject activeWarpShot;

    private float originalSpeed;
    private bool cantMove = false;

    public override void OnEquipmentAction(Character character, bool isHoldDown)
	{
        if (character.Movement.IsFrozen())
        {
            return;
        }
		if (activeWarpShot == null)
		{
            if (isHoldDown)
			{
				SpawnStillWarpShot(character);
                cantMove = true;
			}
			else
			{
				SpawnMovingWarpShot(character);
			}
		}
		else 
		{
			ProjectileBase projectileBase = activeWarpShot.GetComponent<ProjectileBase>();
            if (cantMove && isHoldDown)
            {
                cantMove = false;
                Destroy(projectileBase.gameObject);
                activeWarpShot = null;
            }
            else if (isHoldDown && projectileBase.GetSpeed() > 0)
            {
                originalSpeed = projectileBase.GetSpeed();
                projectileBase.SetSpeed(0);
            }
            else if (isHoldDown && Helper.NearlyEqual(projectileBase.GetSpeed(), 0))
            {
                projectileBase.SetSpeed(originalSpeed);
            }
            else
            {
                WarpToShot();
            }
		}
	}

	private void SpawnMovingWarpShot(Character character)
	{
        if (!Helper.IsTooCloseToWall(character, 0.1f))
        {
            activeWarpShot = Instantiate(warpShotPrefab, character.transform.root);
            activeWarpShot.GetComponent<WarpShot>().SetDirection(character.Movement.GetFacingDirectionNoDiagonal());
            activeWarpShot.transform.position = character.Movement.GetFacingDirectionNoDiagonal() / 3 + character.transform.position;
        }
	}

	private void SpawnStillWarpShot(Character character)
	{
		activeWarpShot = Instantiate(warpShotPrefab, character.transform.root);
		activeWarpShot.GetComponent<WarpShot>().SetSpeed(0);
		activeWarpShot.transform.position = character.transform.position;
	}

	private void WarpToShot()
	{
		character.CenterCamera(character, activeWarpShot.transform.position, false);
		Destroy(activeWarpShot);
		activeWarpShot = null;
	}

	public override void DestroyItems()
	{
		Destroy(activeWarpShot);
	}

	public override bool GetHUDStatus()
	{
		return (activeWarpShot == null);
	}

	public override void SetPrefabs(GameObject[] prefabs)
	{
        warpShotPrefab = prefabs[0];
	}
}
