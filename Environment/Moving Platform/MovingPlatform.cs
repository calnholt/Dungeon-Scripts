using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IEventTrigger, IResetSection
{
	public Vector3 destination;
	public float speed;
	public float waitTime;
	private float waitTimeCounter = 0;
	private Vector3[] destinations;
	private int index = 0;
    public bool isMovingEventTrigger = false;
    public bool stopAtEnd = false;
    [SerializeField]
    private bool isMoving = true;

    private GameObject movingPlatformObj;
    private Collider2D objColl;
    private bool isAtEnd = false;
    private PolygonCollider2D coll;

    private const int TOP_RIGHT = 0;
    private const int TOP_LEFT = 1;
    private const int BOTTOM_LEFT = 2;
    private const int BOTTOM_RIGHT = 3;

	void Start()
	{
        coll = GetComponent<PolygonCollider2D>();
        movingPlatformObj = GetComponentInChildren<MovingPlatform>().gameObject;
        //movingPlatformObj = GetComponentInChildren<MovingPlatform>().gameObject;
        objColl = movingPlatformObj.GetComponent<Collider2D>();

		destinations = new Vector3[2];
        destinations[0] = movingPlatformObj.transform.position;
		destinations[1] = destinations[0] + destination;
    }

	void Update()
    {
        if (isMoving)
        {
            if (stopAtEnd)
            {
                if (movingPlatformObj.transform.position != destinations[1])
                    Move();
                return;
            }
            else
                Move();
        }
	}
	
	private void Move()
	{
        movingPlatformObj.transform.position = Vector3.MoveTowards(movingPlatformObj.transform.position, destinations[index], Time.deltaTime * speed);
        if (movingPlatformObj.transform.position == destinations[index])
		{
            isAtEnd = true;
			waitTimeCounter += Time.deltaTime;
			if (waitTimeCounter >= waitTime)
			{
				index = Mathf.Abs(index - 1);
				waitTimeCounter = 0;
			}
		}
	}

    private void SetPath()
    {
        Vector2[] corners = new Vector2[4];
        corners[TOP_RIGHT] = objColl.bounds.max;
        corners[TOP_LEFT] = new Vector2(objColl.bounds.min.x, objColl.bounds.max.y);
        corners[BOTTOM_LEFT] = objColl.bounds.min;
        corners[BOTTOM_RIGHT] = new Vector2(objColl.bounds.max.x, objColl.bounds.min.y);
        coll.SetPath(0, corners);
    }


    public void SetIsMoving(bool toggle)
    {
        isMoving = toggle;
    }

    public void OnEventTrigger()
    {
        if (isMovingEventTrigger)
        {
            SetIsMoving(!isMoving);
        }
    }

    public void OnResetSection()
    {
        if (isMovingEventTrigger)
        {
            SetIsMoving(false);
            transform.position = destinations[0];
        }
    }

    public void SetOriginalPosition()
    {
        transform.position = destinations[0];
    }

}
