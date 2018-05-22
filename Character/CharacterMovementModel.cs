using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;

public class CharacterMovementModel : MonoBehaviour, IMovingPlatform<Transform>
{
	public float characterSpeed;
	public Camera camera;
	public float cameraSpeed;
	private Rigidbody2D camera_rb;
	public Transform pickupItemParent;

	private Vector3 m_MovementDirection;
	private Vector3 m_FacingDirection;
	private Rigidbody2D m_Body;
	private Vector2 cameraDirection;

	private bool isDead = false;
	private bool isPushing = false;
	private Vector3 pushingDirection;

	private bool restrictX = false;
	private bool restrictY = false;

    private bool m_IsFrozen;

	private float characterSpeedCopy;

    private Transform levelTransform;


	void Awake()
	{
        levelTransform = transform.parent;
		m_Body = GetComponent<Rigidbody2D>();
		camera_rb = camera.GetComponent<Rigidbody2D>();
		m_FacingDirection = new Vector2(0,-1);
		characterSpeedCopy = characterSpeed;
	}

	void FixedUpdate()
	{
		UpdateMovement();
	}
		
	void UpdateMovement()
	{
        if (m_IsFrozen)
        {
            return;
        }
		if (isDead)
		{
			m_FacingDirection = new Vector3(0,-1);
			characterSpeed = 0;
//			m_Body.velocity = m_MovementDirection * 0;
		}
		else if (isPushing)
		{
			m_FacingDirection = pushingDirection;
			if (restrictX)
			{
				m_MovementDirection = new Vector3(0, m_MovementDirection.y, 0);
			}
			else if (restrictY)
			{
				m_MovementDirection = new Vector3(m_MovementDirection.x, 0, 0);
			}
			characterSpeed = characterSpeedCopy/1.5f;
//			m_Body.velocity = m_MovementDirection * characterSpeed/1.5f;
		}
		else
		{
			if( m_MovementDirection != Vector3.zero )
			{
				m_MovementDirection.Normalize();
			}

			Vector3 facingDirection = m_MovementDirection;

			if(facingDirection == Vector3.zero)
			{
				facingDirection = m_FacingDirection;
				characterSpeed = 0;
			}
			else
			{
				if (facingDirection.x < 0)
					facingDirection.x = -1;
				else if (facingDirection.x > 0)
					facingDirection.x = 1;
				if (facingDirection.y < 0)
					facingDirection.y = -1;
				else if (facingDirection.y > 0)
					facingDirection.y = 1;
			}

			m_FacingDirection = facingDirection;

			characterSpeed = characterSpeedCopy;
		}

		m_Body.velocity = characterSpeed * m_MovementDirection;

	}
		
	public void SetDirection( Vector2 direction )
	{
		m_MovementDirection = direction;
	}

	public void SetCameraDirection( Vector2 direction )
	{
		cameraDirection = direction;
	}

	public Vector3 GetDirection()
	{
		return m_MovementDirection;
	}

	public Vector2 GetCameraDirection()
	{
		return cameraDirection;
	}

	public void ResetCamera()
	{
		camera.GetComponent<CameraBehavior>().ResetCamera();
	}

	public Vector3 GetCharacterSpeed()
	{
		if (restrictX)
		{
			m_MovementDirection = new Vector3(0, m_MovementDirection.y, 0);
		}
		else if (restrictY)
		{
			m_MovementDirection = new Vector3(m_MovementDirection.x, 0, 0);
		}
		return characterSpeed * m_MovementDirection;
	}

	public Vector3 GetFacingDirection()
	{
		return m_FacingDirection;
	}

	public Vector3 GetFacingDirectionNoDiagonal()
	{
		Vector3 facingDirection = m_FacingDirection;
		if (facingDirection.x != 0 && facingDirection.y != 0)
			facingDirection.y = 0;
		return facingDirection;
	}

	public bool IsMoving()
	{
		return m_MovementDirection != Vector3.zero;
	}

	public void EnterMovingPlatform(Transform parent)
	{
		transform.parent = parent;
	}

	public void ExitMovingPlatform(Transform parent)
	{
        transform.parent = levelTransform;
	}

	public bool IsDead()
	{
		return isDead;
	}

	public void SetIsDead(bool isPlayerDead)
	{
		this.isDead = isPlayerDead;
	}

	public bool IsPushing()
	{

		return isPushing;
	}

	public void SetIsPushing(bool isPlayerPushing)
	{
		isPushing = isPlayerPushing;
	}

	public Vector3 GetPushingDirection()
	{
		return pushingDirection;
	}

	public void SetPushingDirection(Vector3 _pushingDirection)
	{
		pushingDirection = _pushingDirection;
	}

	public void RestrictXAxis(bool toggle)
	{
		restrictX = toggle;
	}

	public void RestrictYAxis(bool toggle)
	{
		restrictY = toggle;
	}

    public void SetFrozen(bool isFrozen)
    {
        m_IsFrozen = isFrozen;

        if (isFrozen == true)
        {
            //StartCoroutine(FreezeTimeRoutine());
        }
        else
        {
            //Time.timeScale = 1;
        }
    }

    IEnumerator FreezeTimeRoutine()
    {
        yield return null;

        Time.timeScale = 0;
    }

    public bool IsFrozen()
    {
        return m_IsFrozen;
    }

}