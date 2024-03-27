using UnityEngine;

public class ShotgunShootType : PlaneShooter
{
	public int nrOfBulletsPerShot;

	protected override void ShootType()
	{
		float initialAngle = nrOfBulletsPerShot / 2;

		if (initialAngle % 2 == 0)
			initialAngle -= 0.5f;

		nextTimeToFire = Time.time + 1 / fireRate;
		for (int i = 0; i < nrOfBulletsPerShot; i++)
		{
			firePoint.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + initialAngle * 5);
			GameObject instance = objectPooler.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);
			BulletScript script = instance.GetComponent<BulletScript>();
			script.damage = damage;
			script.speed = 40;
			script.maxDistance = 100;

			instance.gameObject.layer = layer;

			script.OnObjectSpawn();

			initialAngle--;
		}
	}
}
