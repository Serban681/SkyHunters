using UnityEngine;

public class SimpleShootType : PlaneShooter
{
	protected override void ShootType()
	{
		nextTimeToFire = Time.time + 1 / fireRate;
		firePoint.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(-5, 5));
		GameObject instance = objectPooler.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);

		BulletScript script = instance.GetComponent<BulletScript>();

		script.damage = damage;
		script.speed = 40;
		script.maxDistance = 100;
		script.OnObjectSpawn();
	
		instance.gameObject.layer = layer;

		firePoint.rotation = Quaternion.identity;
	}
}
