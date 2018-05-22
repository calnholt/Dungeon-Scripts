using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPortalGun : HUDEquipmentBase 
{
	private Image image;
	private Sprite[] nextShot;
	private int currentColor;

	private const int BLUE = 0;
	private const int RED = 1;


	void Start()
	{
		image = GetComponent<Image>();
		previousCheck = hud.character.EquipmentModel[equipmentNum].GetHUDStatus();
		currentColor = BLUE;
		image.sprite = nextShot[BLUE];
	}

	void Update()
	{
		UpdateEquipmentHUD ();
	}

	public override void UpdateEquipmentHUD ()
	{
		bool isBlue = hud.character.EquipmentModel[equipmentNum].GetHUDStatus();
		if (isBlue != previousCheck)
		{
			currentColor = Mathf.Abs(currentColor - 1);
			image.sprite = nextShot[currentColor];
		}
		previousCheck = isBlue;
	}

	public override void SetEquipmentHUDSprites(Sprite[] sprites)
	{
        nextShot = new Sprite[2];
        nextShot[0] = sprites[0];
        nextShot[1] = sprites[1];
	}
}
