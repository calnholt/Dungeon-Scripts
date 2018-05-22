using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEmitterBase : MonoBehaviour 
{
	[SerializeField]
	protected float waitTime;
	[SerializeField]
	protected Vector3 projectileDirection;
	[SerializeField]
	protected float projectileSpeed;
	[SerializeField]
	protected string parentName;

	virtual public void SpawnProjectile()
	{
		Debug.LogWarning( "SpawnProjectile() is not implemented" );
	}

	public void SetProjectile(GameObject projectile)
	{
		ProjectileBase pb = projectile.GetComponent<ProjectileBase>();
		pb.SetDirection(projectileDirection);
		pb.SetSpeed(projectileSpeed);
	}


}
