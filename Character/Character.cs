using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public CharacterMovementModel Movement;
	public CharacterInteractionModel Interaction;
	public CharacterMovementView MovementView;
	[HideInInspector]
	public EquipmentBaseModel[] EquipmentModel;

	void Start()
	{
		EquipmentModel = GetComponents<EquipmentBaseModel>();
	}

	public void CenterCamera(Character character, Vector3 position, bool isPlayerDead)
	{
        if (character.Interaction.IsGrabbingObject() && isPlayerDead)
		{
			character.Interaction.StopGrabbingObject(character.Interaction.GetGrabbableObject());
		}
        character.gameObject.transform.position = position;
		character.GetComponentInChildren<CameraBehavior>().CenterCamera();
	}

    public void DestroyEquipmentItems()
    {
        for (int i = 0; i < EquipmentModel.Length; i++)
        {
            EquipmentModel[i].DestroyItems();
        }
    }
}