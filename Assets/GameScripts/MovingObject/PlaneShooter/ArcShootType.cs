using UnityEngine;

public class ArcShootType : PlaneShooter
{
	public int nrOfBulletsPerShot;
	public float circleRadius;

	protected override void ShootType()
	{
		nextTimeToFire = Time.time + 1 / fireRate;
		GameObject instance = objectPooler.SpawnFromPool("ArcBulletFormation", firePoint.position, transform.rotation);
		ArcBulletFormationScript script = instance.GetComponent<ArcBulletFormationScript>();
		script.layer = layer;
		script.damage = damage;
		script.nrOfBulletsPerShoot = nrOfBulletsPerShot;
		script.circleRadius = circleRadius;
		script.speed = 40;
		script.maxDistance = 100;
		script.OnObjectSpawn();
	}
}
