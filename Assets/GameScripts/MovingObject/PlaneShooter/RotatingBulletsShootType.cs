using UnityEngine;

public class RotatingBulletsShootType : PlaneShooter
{
	private int rotDir = 1;
	public float circleRadius = 3f;
	public int nrOfBulletsPerShot = 4;

	protected override void ShootType()
	{
		nextTimeToFire = Time.time + 1 / fireRate;
		rotDir *= -1;
		GameObject instance = objectPooler.SpawnFromPool("RotatingBulletsFormation", firePoint.position, transform.rotation);

		RotatingBulletFormationScript script = instance.GetComponent<RotatingBulletFormationScript>();

		script.tagName = transform.tag;
		script.damage = damage;
		script.nrOfBulletsPerShoot = nrOfBulletsPerShot;
		script.circleRadius = circleRadius;
		script.rotDir = rotDir;
		script.speed = 40;
		script.maxDistance = 100;
		script.OnObjectSpawn();
	}
}
