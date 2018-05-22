using UnityEngine;
using System.Collections;

public class Helper 
{
	public static Vector3 UP {get{return new Vector3(0f, 1f, 0f);}}

	public static Vector3 RIGHT {get{return new Vector3(1f, 0f, 0f);}}

	public static Vector3 DOWN {get{return new Vector3(0f, -1f, 0f);}}

	public static Vector3 LEFT {get{return new Vector3(-1f, 0f, 0f);}}

	public static void SetSortingLayerForAllRenderers( Transform parent, string sortingLayerName )
	{
		SpriteRenderer[] renderers = parent.GetComponentsInChildren<SpriteRenderer>();

		foreach( SpriteRenderer spriteRenderer in renderers )
		{
			spriteRenderer.sortingLayerName = sortingLayerName;
		}
	}

	public static bool IsTooCloseToWall(Character character, float offset)
	{
		BoxCollider2D boxCollider = character.GetComponent<BoxCollider2D>();

		Vector3 facingDirection = character.Movement.GetFacingDirectionNoDiagonal();
		Vector3 pointA = Vector3.zero;
		Vector3 pointB = Vector3.zero;

		float widthOffset = 0.1f;

//		RIGHT
		if (facingDirection == new Vector3(1, 0, 0))
		{
			pointA =  new Vector2(boxCollider.bounds.max.x + offset, boxCollider.bounds.max.y - widthOffset);
			pointB =  new Vector2(boxCollider.bounds.max.x + offset, boxCollider.bounds.min.y + widthOffset);
		}
		//LEFT
		else if (facingDirection == new Vector3(-1, 0, 0))
		{
			pointA =  new Vector2(boxCollider.bounds.min.x - offset, boxCollider.bounds.max.y - widthOffset);
			pointB =  new Vector2(boxCollider.bounds.min.x - offset, boxCollider.bounds.min.y + widthOffset);
		}
		//UP
		else if (facingDirection == new Vector3(0, 1, 0))
		{
			pointA =  new Vector2(boxCollider.bounds.min.x + widthOffset, boxCollider.bounds.max.y + offset);
			pointB =  new Vector2(boxCollider.bounds.max.x - widthOffset, boxCollider.bounds.max.y + offset);
		}
		//DOWN
		else
		{
			pointA =  new Vector2(boxCollider.bounds.min.x + widthOffset, boxCollider.bounds.min.y - offset);
			pointB =  new Vector2(boxCollider.bounds.max.x - widthOffset, boxCollider.bounds.min.y - offset);

			//pointA = (Vector2)character.transform.position - boxCollider.offset - boxCollider.size * .5f;
		}



		Collider2D[] closeColliders = Physics2D.OverlapAreaAll(
			(Vector2)pointA, (Vector2)pointB );


		for( int i = 0; i < closeColliders.Length; ++i )
		{
			if (closeColliders[i].gameObject.CompareTag("Wall"))
			{
				return true;
			}
		}
		return false;
	}

	public static void OnDeath(Character character)
	{
		character.Movement.SetIsDead(true);
		CameraBehavior camera = character.gameObject.GetComponentInChildren<CameraBehavior>();
		camera.SetCameraPosition(character.gameObject.transform.position);
//		Debug.Log("camera.gameObject.transform.localPosition: " + camera.gameObject.transform.localPosition); 
		character.gameObject.transform.position = GameObject.Find("Checkpoint Manager").GetComponent<CheckpointManager>().GetCurrentCheckpointPosition();
		camera.ResetCamera();
//		character.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
	}

	public static Transform FindParentTransform(string parentName, GameObject child)
	{
		while (child.transform.parent.name != parentName)
		{
			child.transform.parent = child.transform.parent.parent;
		}
		return child.transform.parent;
	}

	public static SectionManager FindCurrentSection(string sectionName, Transform child)
	{	
		Transform childCopy = child;
		while (childCopy.name != sectionName)
		{
			childCopy = childCopy.parent;
		}
		return childCopy.GetComponent<SectionManager>();
	}

    public static bool NearlyEqual(float a, float b)
    {
        float epsilon = 0.00001f;
        float absA = Mathf.Abs(a);
        float absB = Mathf.Abs(b);
        float diff = Mathf.Abs(a - b);

        if (a == b)
        { // shortcut, handles infinities
            return true;
        }
        else if (a == 0 || b == 0 || diff < float.MinValue)
        {
            // a or b is zero or both are extremely close to it
            // relative error is less meaningful here
            return diff < (epsilon * float.MinValue);
        }
        else
        { // use relative error
            return diff / Mathf.Min((absA + absB), float.MaxValue) < epsilon;
        }
    }


}