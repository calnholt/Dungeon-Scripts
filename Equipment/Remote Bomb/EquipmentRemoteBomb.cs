using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentRemoteBomb : EquipmentBaseModel 
{
	private GameObject remoteBombPrefab;

	private GameObject remoteBomb;
		
	public override void OnEquipmentAction(Character character, bool isHeldDown)
	{   
        if (remoteBomb == null)
        {
            SpawnRemoteBomb(character);
            if (isHeldDown && !character.Interaction.IsCarryingObject())
            {
                character.Interaction.PickupObject(remoteBomb.GetComponent<InteractablePickup>());
			}
        }
		else 
		{
			DetonateRemoteBomb();
		}
	}

	private void SpawnRemoteBomb(Character character)
	{
		remoteBomb = Instantiate(remoteBombPrefab, character.transform.parent);
        remoteBomb.transform.position = character.Movement.GetFacingDirectionNoDiagonal()/2 + character.transform.position;
	}

	private void DetonateRemoteBomb()
	{
		remoteBomb.GetComponent<RemoteBomb>().Explode(remoteBomb.transform.position);
		remoteBomb = null;
	}

	public override void DestroyItems()
	{
		Destroy(remoteBomb);
	}

	public override void SetPrefabs(GameObject[] prefabs)
	{
        remoteBombPrefab = prefabs[0];
	}

	public override bool GetHUDStatus()
	{
        return (remoteBomb != null);
	}

	//private void Spawn 
}
