using UnityEngine;

public class CircleShootType : PlaneShooter
{
	public int nrOfBulletsPerShot;
	public float circleRadius;

	protected override void ShootType()
	{
		nextTimeToFire = Time.time + 1 / fireRate;
		GameObject instance = objectPooler.SpawnFromPool("CircleBulletFormation", firePoint.position, transform.rotation);

		CircleBulletFormationScript script = instance.GetComponent<CircleBulletFormationScript>();

		script.tagName = transform.tag;
		script.damage = damage;
		script.nrOfBulletsPerShoot = nrOfBulletsPerShot;
		script.circleRadius = circleRadius;
		script.speed = 40;
		script.maxDistance = 100;
		script.OnObjectSpawn();
	}
}
