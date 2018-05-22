using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBombButton : MonoBehaviour, IRemoteBombExplosionTrigger
{
    private SpriteRenderer spriteRenderer;
    public enum ButtonColorEnum 
    {
        Orange, Yellow, Green, Blue
    }
    public ButtonColorEnum buttonColor;
    public bool isOnePress;
    [SerializeField]
    private Sprite[] orangeSprites;
    [SerializeField]
    private Sprite[] yellowSprites;
    [SerializeField]
    private Sprite[] greenSprites;
    [SerializeField]
    private Sprite[] blueSprites;
    private Sprite[] sprites;
    public GameObject[] triggers;
    private bool isUp = true;
    private bool stopToggling = false;

	void Start () 
    {
        sprites = new Sprite[2];
        switch (buttonColor)
        {
            case ButtonColorEnum.Orange:
                sprites[0] = orangeSprites[0];
                sprites[1] = orangeSprites[1];
                break;
            case ButtonColorEnum.Yellow:
                sprites[0] = yellowSprites[0];
                sprites[1] = yellowSprites[1];
                break;
            case ButtonColorEnum.Green:
                sprites[0] = greenSprites[0];
                sprites[1] = greenSprites[1];
                break;
            case ButtonColorEnum.Blue:
                sprites[0] = blueSprites[0];
                sprites[1] = blueSprites[1];
                break;
        }
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
	}
    	
    public void OnRemoteBombExplosionTrigger()
    {
        if (stopToggling)
            return;
        if (isOnePress)
        {
            ToggleButton();
            stopToggling = true;
        }
        TriggerEvents();
    }

    private void TriggerEvents()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            IEventTrigger iEventTrigger = triggers[i].GetComponent(typeof(IEventTrigger)) as IEventTrigger;
            iEventTrigger.OnEventTrigger();
        }
    }

    private void ToggleButton()
    {
        if (isUp)
            spriteRenderer.sprite = sprites[1];
        else
            spriteRenderer.sprite = sprites[0];
        isUp = !isUp;
    }

    public void SetIsButtonUp(bool isbuttonUp)
    {
        isUp = isbuttonUp;
    }

    public void OnSectionReset()
    {
        isUp = true;
        spriteRenderer.sprite = sprites[0];
        stopToggling = false;
    }

}
