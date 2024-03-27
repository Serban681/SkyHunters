using UnityEngine;

public class PlaneShooter : MonoBehaviour
{
	protected Transform firePoint;

	public float damage;
	protected float nextTimeToFire;
	public float fireRate;
	protected ObjectPooler objectPooler;

	protected int layer;

	// Start is called before the first frame update
	protected virtual void Start()
	{
		firePoint = transform.Find("FirePoint").transform;

		objectPooler = ObjectPooler.instance;

		InitializeBulletLayer();
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.P) && transform.tag == "Player")
			Shoot();
	}

	public virtual void Shoot()
	{
		if (Time.time >= nextTimeToFire)
		{
			if (transform.CompareTag("Player"))
			{
				FindObjectOfType<AudioManagerScript>().Play("ShootSound");

				FindObjectOfType<FancyShootButtonScript>().Flash();

				ShootType();
			}
			else
			{
				FindObjectOfType<AudioManagerScript>().Play("ShootSound");

				ShootType();
			}
		}
	}

	private void InitializeBulletLayer()
	{
		if (gameObject.GetComponent<Ally>() != null)
		{
			layer = 10;
		}
		else if (gameObject.GetComponent<Enemy>() != null)
		{
			layer = 11;
		}
		else if (gameObject.GetComponent<PlayerSide>() != null)
		{
			layer = 16;
		}
		else if (gameObject.GetComponent<EnemySide>() != null)
		{
			layer = 17;
		}
	}

	protected virtual void ShootType()
	{
		return;
	}
}
