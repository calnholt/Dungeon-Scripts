using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchDpadManager : MonoBehaviour
{
	private Vector3 currentDirection;
	private bool cameraToggle = false;
    private bool cameraReset = false;
    public Image cameraIcon;
    private Color cameraInactive;
    private Color cameraActive;
    private float cameraTimer;
    int tap = 0;

	void Start () 
	{
        if (!PlatformManager.PM.isMobile)
            gameObject.SetActive(false);
        else
        {
            cameraInactive = cameraIcon.color;
            cameraActive = new Color(cameraInactive.r, cameraInactive.g, cameraInactive.b, 1f);
            currentDirection = new Vector3(0, 0, 0);
        }
	}

	private void Update()
	{
        if (IsCameraToggled())
        {
            cameraTimer += Time.deltaTime;
            if (cameraTimer > 0.3f)
                tap = 0;
        }
        else
            cameraTimer = 0;
	}

	public void SetDirection(Vector3 direction)
	{
		currentDirection = direction;
	}

	public Vector3 GetDirection()
	{
		return currentDirection;
	}

	public bool IsCameraToggled()
	{
		return cameraToggle;
	}

	public void ToggleCamera()
	{
		cameraToggle = !cameraToggle;
        if (IsCameraToggled())
        {
            cameraIcon.color = cameraActive;
            tap++;
            Debug.Log("tap: " + tap);
            if (tap == 2)
            {
                cameraReset = true;
                tap = 0;
            }
        }
        else
            cameraIcon.color = cameraInactive;
	}

    public bool IsCameraReset()
    {
        return cameraReset;
    }

    public void ToggleCameraReset(bool toggle)
    {
        cameraReset = toggle;
    }
}
