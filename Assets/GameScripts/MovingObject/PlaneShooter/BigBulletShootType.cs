using UnityEngine;

public class BigBulletShootType : PlaneShooter
{
	public ShootRelatedClass.SplitType splitType;
	public float circleRadius;
	public int nrOfBulletsPerShoot;
	public float instanceDamage;

	protected override void ShootType()
	{
		nextTimeToFire = Time.time + 1 / fireRate;
		GameObject instance = objectPooler.SpawnFromPool("BigBullet", firePoint.position, transform.rotation);

		BigBulletScript script = instance.GetComponent<BigBulletScript>();
		script.layer = layer;
		script.splitType = splitType;
		script.radius = circleRadius;
		instance.gameObject.layer = layer;
		script.nrOfBulletsPerShoot = nrOfBulletsPerShoot;
		script.instanceDamage = instanceDamage;
		script.maxDistance = 20;
		script.speed = 40;
		script.OnObjectSpawn();
	}
}
