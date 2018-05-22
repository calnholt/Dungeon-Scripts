using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSign : InteractableBase 
{
    [TextArea]
    public string signText;

    public override void OnInteract(Character character)
    {
        if (HUDSign.IsVisible() == true)
        {
            character.Movement.SetFrozen(false);
            HUDSign.Hide();
        }
        else
        {
            character.Movement.SetFrozen(true);
            HUDSign.Show(signText);
        }

    }
}
