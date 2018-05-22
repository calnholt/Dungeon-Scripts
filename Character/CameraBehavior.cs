using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour 
{
	public float timeBeforeMoveBack = 1f;
	public float speed = 2;
	public float cameraDistance;
	public Vector2 boundaryPos;
	private Vector2 boundaryNeg;
	private CharacterMovementModel movementModel;
	private Rigidbody2D rb;
	private bool resetCamera = false;
    private float maxX;
    private float maxY;
    private float minX;
    private float minY;

	void Start () 
	{
		movementModel = GetComponentInParent<CharacterMovementModel>();
		rb = GetComponent<Rigidbody2D>();
		boundaryNeg = boundaryPos * -1;
	}

	void FixedUpdate()
	{
		MoveCamera();
	}

	void Update () 
	{
		CheckMoveBackToPlayer();
	}

	void CheckMoveBackToPlayer()
	{
		if (movementModel.GetCameraDirection() != Vector2.zero)
		{
			resetCamera = false;
		}
			
		if (resetCamera)
		{
			transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)movementModel.gameObject.transform.position, Time.deltaTime * 5f);
			if ((Vector2)transform.position == (Vector2)movementModel.gameObject.transform.position)
			{
				resetCamera = false;
				if (movementModel.IsDead())
				{
					movementModel.GetComponentInChildren<SpriteRenderer>().enabled = true;
					movementModel.SetIsDead(false);
				}
			}
		}

	}

	public void ResetCamera()
	{
		resetCamera = true;
	}

	public bool IsCameraOnPlayer()
	{
		return !resetCamera;
	}

	void MoveCamera()
	{
		Vector2 cameraDirection = movementModel.GetCameraDirection();

		if( cameraDirection != Vector2.zero )
		{
			cameraDirection.Normalize();
			rb.bodyType = RigidbodyType2D.Dynamic;
		}
		else
		{
			rb.bodyType = RigidbodyType2D.Kinematic;
		}

		rb.velocity = cameraDirection * speed;

	}

    void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.name == "Player")
        {
            SetMinMax();
            float x = 0;
            float y = 0;
            float offset = 0.01f;

            if (transform.localPosition.x < minX + .04f && transform.localPosition.x > minX - .04f)
            {
                x = offset;
            }
            else if (transform.localPosition.x < maxX + .04f && transform.localPosition.x > maxX - .04f)
            {
                x = -offset;
            }
            if (transform.localPosition.y < minY + .04f && transform.localPosition.y > minY - .04f)
            {
                y = offset;
            }
            else if (transform.localPosition.y < maxY + .04f && transform.localPosition.y > maxY - .04f)
            {
                y = -offset;
            }
			transform.position = new Vector3(transform.position.x + x, transform.position.y + y, 0);
        }
	}

	public void CenterCamera()
	{
		transform.position = movementModel.gameObject.transform.position;
	}

	public void SetCameraPosition(Vector3 position)
	{
		transform.position = position;
	}

    private void SetMinMax()
    {
        if (transform.localPosition.x > maxX)
            maxX = transform.localPosition.x;
        if (transform.localPosition.y > maxY)
            maxY = transform.localPosition.y;
        if (transform.localPosition.x < minX)
            minX = transform.localPosition.x;
        if (transform.localPosition.y < minY)
            minY = transform.localPosition.y;
    }

}
