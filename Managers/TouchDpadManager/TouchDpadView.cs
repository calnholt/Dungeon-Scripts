using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchDpadView : MonoBehaviour 
{
    public Image[] panels;
    private TouchDpadManager touchDpadManager;

    private const int N = 0;
    private const int E = 1;
    private const int S = 2;
    private const int W = 3;

    private bool[] isEnabled;

	void Start () 
    {
        isEnabled = new bool[] { false, false, false, false};
        touchDpadManager = GetComponent<TouchDpadManager>();	
	}
	
	void Update () 
    {
        DisplayDirectionPress();
	}

    void DisplayDirectionPress()
    {
        Vector2 direction = touchDpadManager.GetDirection();

        if (direction.x > 0)
        {
            isEnabled[E] = true;
            isEnabled[W] = false;
        }
        if (direction.x < 0)
        {
            isEnabled[W] = true;
            isEnabled[E] = false;
        }
        if (direction.y > 0)
        {
            isEnabled[N] = true;
            isEnabled[S] = false;
        }
        if (direction.y < 0)
        {
            isEnabled[N] = false;
            isEnabled[S] = true;
        }
        if (direction.x == 0)
        {
            isEnabled[E] = false;
            isEnabled[W] = false;
        }
        if (direction.y == 0)
        {
            isEnabled[N] = false;
            isEnabled[S] = false;
        }

        for (int i = 0; i < isEnabled.Length; i++)
        {
            if (isEnabled[i])
                panels[i].enabled = true;
            else
                panels[i].enabled = false;
        }

    }
}
