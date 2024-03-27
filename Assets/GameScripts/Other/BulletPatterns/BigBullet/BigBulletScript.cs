using UnityEngine;

public class BigBulletScript : MonoBehaviour, IPooledObject
{
	public float speed;
	private Rigidbody2D rb;

	public float radius;
	public float damage = 40;
	public float instanceDamage = 20;

	private float travelledDistance = 0f;
	private Vector3 lastPos;
	public float maxDistance;
	[HideInInspector]
	public int layer;

	public ShootRelatedClass.SplitType splitType;

	private delegate void _SplitType();
	_SplitType SplitType;

	public int nrOfBulletsPerShoot = 5;

	private ObjectPooler objectPooler;

	private void Awake()
	{
		objectPooler = ObjectPooler.instance;
	}

	// Start is called before the first frame update
	public void OnObjectSpawn()
	{
		travelledDistance = 0;

		if(splitType == ShootRelatedClass.SplitType.ArcFormationType)
		{
			SplitType = ArcFormationType;
		}
		else if(splitType == ShootRelatedClass.SplitType.CircleFormationType)
		{
			SplitType = CircleFormationType;
		}
		else if(splitType == ShootRelatedClass.SplitType.RayFormationType)
		{
			SplitType = RayFormationType;
		}

		lastPos = transform.position;
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;
	}

	private void Update()
	{
		travelledDistance += Vector3.Distance(transform.position, lastPos);

		if (travelledDistance >= maxDistance)
		{
			SplitType();
			objectPooler.SpawnFromPool("BigSpark", transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}

		lastPos = transform.position;
	}

	private void ArcFormationType()
	{
		GameObject instance = objectPooler.SpawnFromPool("ArcBulletFormation", transform.position, transform.rotation);
        ArcBulletFormationScript script = instance.GetComponent<ArcBulletFormationScript>();
        script.gameObject.layer = layer;
		script.circleRadius = radius;
		script.nrOfBulletsPerShoot = nrOfBulletsPerShoot;
		script.damage = instanceDamage;
		script.speed = 40;
		script.maxDistance = 80;
		script.OnObjectSpawn();
	}

	private void CircleFormationType()
	{
		GameObject instance = objectPooler.SpawnFromPool("CircleBulletFormation", transform.position, transform.rotation);
        CircleBulletFormationScript script = instance.GetComponent<CircleBulletFormationScript>();
        script.gameObject.layer = layer;
		script.circleRadius = radius;
		script.nrOfBulletsPerShoot = nrOfBulletsPerShoot;
		script.damage = instanceDamage;
		script.speed = 40;
		script.maxDistance = 80;
		script.OnObjectSpawn();
	}

	private void RayFormationType()
	{
		int initialAngle = nrOfBulletsPerShoot / 2;
		for (int i = 0; i < nrOfBulletsPerShoot; i++)
		{
			GameObject instance = objectPooler.SpawnFromPool("Bullet", transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + initialAngle * 5));
            BulletScript script = instance.GetComponent<BulletScript>();
            script.damage = instanceDamage;
			script.maxDistance = 80;
			script.speed = 40;
			script.gameObject.layer = layer;
			script.OnObjectSpawn();
			initialAngle--;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		objectPooler.SpawnFromPool("BigSpark", other.Distance(transform.GetComponent<Collider2D>()).pointB, Quaternion.identity);
		other.GetComponent<Health>().TakeDamage(damage);
		gameObject.SetActive(false);
	}
}
