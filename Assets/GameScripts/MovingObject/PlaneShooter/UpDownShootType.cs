using UnityEngine;

public class UpDownShootType : PlaneShooter
{
	protected override void ShootType()
	{
		nextTimeToFire = Time.time + 1 / fireRate;
		GameObject instance = objectPooler.SpawnFromPool("UpDownBullet", firePoint.position, firePoint.rotation);
		BulletFormationScript script = instance.GetComponent<BulletFormationScript>();
		script.damage = damage;
		script.speed = 40;
		script.maxDistance = 100;
		instance.gameObject.layer = layer;
		script.OnObjectSpawn();
	}
}
