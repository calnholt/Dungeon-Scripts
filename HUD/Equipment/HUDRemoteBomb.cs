using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDRemoteBomb : HUDEquipmentBase 
{
    private Image image;
    private Sprite[] sprites;

	void Start () 
    {
        image = GetComponent<Image>();
        previousCheck = hud.character.EquipmentModel[equipmentNum].GetHUDStatus();
        image.sprite = sprites[0];
	}

    void Update()
    {
        UpdateEquipmentHUD();
    }
	
	public override void UpdateEquipmentHUD()
	{
        bool isBombActive = !hud.character.EquipmentModel[equipmentNum].GetHUDStatus();
        if (isBombActive != previousCheck)
        {
            if (isBombActive)
            {
                image.sprite = sprites[0];
            }
            else
            {
                image.sprite = sprites[1];
            }
        }
        previousCheck = isBombActive;
	}

	public override void SetEquipmentHUDSprites(Sprite[] _sprites)
	{
        sprites = new Sprite[2];
        sprites[0] = _sprites[0];
        sprites[1] = _sprites[1];
	}
}
