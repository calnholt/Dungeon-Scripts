using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDWarpToShot : HUDEquipmentBase 
{
	private Image image;
	private Sprite sprite;
	private ShrinkExpand se;
    private Color color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0 / 255f);

	void Start()
	{
		image = GetComponent<Image>();
		se = GetComponent<ShrinkExpand>();
		se.enabled = false;
		previousCheck = hud.character.EquipmentModel[equipmentNum].GetHUDStatus();
		image.sprite = sprite;
		image.color = color;
	}
	
	void Update () 
	{
		UpdateEquipmentHUD();
	}

	public override void UpdateEquipmentHUD ()
	{
		bool isShotActive = !hud.character.EquipmentModel[equipmentNum].GetHUDStatus();
		if (isShotActive != previousCheck)
		{
			if (isShotActive)
			{
				se.enabled = true;
				color.a = 255/255f;
				image.color = color;
			}
			else
			{
				se.enabled = false;
				color.a = 150/255f;
				image.color = color;
				transform.localScale = Vector3.one;
			}
		}
		previousCheck = isShotActive;
	}

	public override void SetEquipmentHUDSprites(Sprite[] sprites)
	{
        sprite = sprites[0];
        ShrinkExpand shrinkExpand = gameObject.AddComponent<ShrinkExpand>();
        shrinkExpand.SetValues(0.015f, 0.015f, 0, 0.85f, 1.15f);
	}
}
