using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorProjectileEmitter : ProjectileEmitterBase 
{
	[SerializeField]
	private GameObject projectilePrefab;
	[SerializeField]
	private Transform emitterTransform;
	[SerializeField]
	private int colorIndex;


	void Start () 
	{
		SpawnProjectile();
	}

	public override void SpawnProjectile()
	{
		StartCoroutine(_SpawnProjectile());
	}

	IEnumerator _SpawnProjectile()
	{
		while (true)
		{
			yield return new WaitForSeconds(waitTime);
			GameObject projectile = Instantiate(projectilePrefab);
			SetProjectile(projectile);
			projectile.GetComponent<ProjectileColor>().SetColor(colorIndex);
			projectile.transform.position = gameObject.transform.position + (projectile.GetComponent<ProjectileBase>().GetDirection()/3);
//			projectile.transform.position = emitterTransform.position;
		}
	}
}
