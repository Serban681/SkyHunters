using UnityEngine;

public class Gunner : MonoBehaviour
{
	private int layer;

	private ObjectPooler objectPooler;

	public enum BulletType
	{
		SimpleShootType,
		UpDownShootType,
		ShotgunShootType,
		CircleShootType,
		ArcShootType
	}

	public BulletType bulletType;

	public int nrOfBulletsPerShoot;
	public float circleRadius;

	public float rotationSpeed = 4;
	private float finalRotZ;
	private Transform firePoint;

	public float damage;
	private float nextTimeToFire;
	public float fireRate;
	public GameObject bullet;
	private RaycastHit2D ray;
	private Transform enemy;
	private Vector3 direction;
	private Vector3 subtract;
	public float maxDistance = 30;

	private LayerMask layerMask;

	private bool rotatedToRight = true;

	public int shooterType = 1;

	private float lastRotZ;

	private Quaternion q;

	private string tagName;

	private float shootRange = 50f;

	private float timerBetweenShots = 0f;
	private float timer2 = 0f;
	private float timer = 0f;

	private float delay = 0f;
	private float delayTimer = 1f;

	private delegate void _ShootType();
	_ShootType ShootType;

	private PlaneMovement planeMovement;

	void Start()
    {
		planeMovement = transform.parent.parent.GetComponent<PlaneMovement>();

		planeMovement.OnPlaneRotate += RotatedToRight;

		objectPooler = ObjectPooler.instance;

		tagName = transform.parent.transform.parent.transform.tag;

		if(shooterType == 1)
		{
			lastRotZ = 150;
		}
		else if(shooterType == 2)
		{
			lastRotZ = 30;
		}
		firePoint = transform.Find("FirePoint").transform;

		if (tagName == "Player")
		{
			layerMask = (1 << 9);
			shootRange = 50;
			layer = 10;
		}
		else
		{
			layerMask = (1 << 8);
			shootRange = maxDistance;
			layer = 11;
		}

		if (bulletType == BulletType.SimpleShootType)
		{
			ShootType = SimpleShootType;
		}
		else if (bulletType == BulletType.UpDownShootType)
		{
			ShootType = UpDownShootType;
		}
		else if(bulletType == BulletType.ShotgunShootType)
		{
			ShootType = ShotgunShootType;
		}
		else if(bulletType == BulletType.CircleShootType)
		{
			ShootType = CircleShootType;
		}
		else if(bulletType == BulletType.ArcShootType)
		{
			ShootType = ArcShootType;
		}
	}

	void Update()
    {
		if (enemy)
		{
			subtract = enemy.transform.position - transform.position;
			direction = subtract.normalized;
		}

		ShootEnemies();
	}

	private void RotatedToRight(object sender,  System.EventArgs e)//PlaneMovement.OnPlaneRotateEventArgs e)
	{
		rotatedToRight = planeMovement.rotateToRight;//transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);//rotatedToRight = e.rotateToRight;
	}

	private void ShootEnemies()
	{
		if (delayTimer >= delay)
		{
			if (tagName == "Player" && EnemySpawner.instance.curEnemies > 0)
				enemy = FindClosestEnemy(transform.position);
			else
			{
				if (StaticVariables.playerHealth > 0)
					enemy = FindClosestAlly(transform.position);
			}

			delayTimer = 0f;
		}
		Rotate(direction.x, direction.y);		
		
		if(Physics2D.Raycast(firePoint.position, transform.right, shootRange, layerMask))
		{
			ray = Physics2D.Raycast(firePoint.position, transform.right, shootRange, layerMask);
			if (tagName == "Enemy")
			{
				if (timer <= timerBetweenShots)
				{
					ShootType();
					timer2 = 0f;
				}
				else
				{
					if (timer2 <= Random.Range(0.25f, 0.75f))
					{
						timerBetweenShots = Random.Range(1, 2);
						timer2 += Time.deltaTime;
					}
					else
					{
						timer = 0f;
					}
				}

				timer += Time.deltaTime;
			}
			else
			{
				ShootType();
			}
		}

		delayTimer += Time.deltaTime;
	}

	private void Rotate(float xDir, float yDir)
	{
		if (xDir != 0 || yDir != 0)
		{
			float finalRotZ = (float)((Mathf.Atan2(xDir, -yDir) / Mathf.PI) * 180f) - 90;

			if (finalRotZ < 0)
				finalRotZ += 360f;

			q = Quaternion.FromToRotation(transform.parent.parent.right, direction);

			if (shooterType == 1)
			{
				if (rotatedToRight == true)
				{
					if (q.eulerAngles.z >= 100 && q.eulerAngles.z <= 180)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
				else
				{
					if (q.eulerAngles.z <= 260 && q.eulerAngles.z >= 180)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
			}
			else if(shooterType == 2)
			{
				if (rotatedToRight == true)
				{
					if ((q.eulerAngles.z <= 120 && q.eulerAngles.z >= 0) || q.eulerAngles.z >= 350)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
				else
				{
					if (q.eulerAngles.z >= 240 || q.eulerAngles.z <= 10)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
			}
			else if(shooterType == 3)
			{
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
			}
			else if(shooterType == 4)
			{
				if (rotatedToRight == true)
				{
					if (q.eulerAngles.z <= 80 && q.eulerAngles.z >= 0)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
				else
				{
					if (q.eulerAngles.z >= 280)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
			}
			else if(shooterType == 5)
			{
				if(rotatedToRight == true)
				{
					if (q.eulerAngles.z <= 220 && q.eulerAngles.z >= 140)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
				else
				{
					if (q.eulerAngles.z <= 220 && q.eulerAngles.z >= 140)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
			}
			else if(shooterType == 6)
			{
				if (rotatedToRight == true)
				{
					if (q.eulerAngles.z >= 180 && q.eulerAngles.z <= 290)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
				else
				{
					if (q.eulerAngles.z >= 70 && q.eulerAngles.z <= 180)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
			}
			else if(shooterType == 7)
			{
				if (rotatedToRight == true)
				{
					if (q.eulerAngles.z >= 220 && q.eulerAngles.z <= 360)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
				else
				{
					if (q.eulerAngles.z >= 0 && q.eulerAngles.z <= 140)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
			}
			else if(shooterType == 8)
			{
				if(rotatedToRight == true)
				{
					if(q.eulerAngles.z <= 30 || q.eulerAngles.z >= 330)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
				else
				{
					if (q.eulerAngles.z <= 30 || q.eulerAngles.z >= 330)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
			}
			else if(shooterType == 9)
			{
				if (rotatedToRight == true)
				{
					if (q.eulerAngles.z >= 330 && q.eulerAngles.z <= 360)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
				else
				{
					if (q.eulerAngles.z >= 0 && q.eulerAngles.z <= 30)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
			}
			else if(shooterType == 10)
			{
				if (rotatedToRight == true)
				{
					if (q.eulerAngles.z >= 180 && q.eulerAngles.z <= 210)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
				else
				{
					if (q.eulerAngles.z >= 150 && q.eulerAngles.z <= 180)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), rotationSpeed);
					}
				}
			}
		}
	}

	private void SimpleShootType()
	{
		if (Time.time >= nextTimeToFire)
		{
            FindObjectOfType<AudioManagerScript>().Play("ShootSound");

			nextTimeToFire = Time.time + 1 / fireRate;
			if(tagName == "Enemy")
				firePoint.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(-10, 10));
			else
				firePoint.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);

			GameObject instance = objectPooler.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);

            BulletScript script = instance.GetComponent<BulletScript>();

            if (tagName == "Player")
				instance.gameObject.layer = 10;
			else if(tagName == "Enemy")
				instance.gameObject.layer = 11;

			script.damage = damage;
		
			script.maxDistance = maxDistance;

			script.speed = 30;
			script.OnObjectSpawn();

			firePoint.rotation = Quaternion.identity;
		}
	}

	private void UpDownShootType()
	{
		if (Time.time >= nextTimeToFire)
		{
            FindObjectOfType<AudioManagerScript>().Play("ShootSound");

            nextTimeToFire = Time.time + 1 / fireRate;
			GameObject instance = objectPooler.SpawnFromPool("UpDownBullet", firePoint.position, firePoint.rotation);

            BulletFormationScript script = instance.GetComponent<BulletFormationScript>();

            script.damage = damage;
			if (tagName == "Player")
				instance.gameObject.layer = 10;
			else
			{
				instance.transform.gameObject.layer = 11;								
			}
			script.maxDistance = maxDistance;
			script.speed = 30;
			script.OnObjectSpawn();
		}
	}

	private void ShotgunShootType()
	{
		if (Time.time >= nextTimeToFire)
		{
            FindObjectOfType<AudioManagerScript>().Play("ShootSound");

            nextTimeToFire = Time.time + 1 / fireRate;
			int initialAngle = nrOfBulletsPerShoot / 2;
			for (int i = 0; i < nrOfBulletsPerShoot; i++)
			{
				firePoint.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + initialAngle * 5);
				GameObject instance = objectPooler.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);
                BulletScript script = instance.GetComponent<BulletScript>();
                script.damage = damage;
				script.speed = 30;
				script.maxDistance = maxDistance;
				script.OnObjectSpawn();
				if (tagName == "Player")
					instance.gameObject.layer = 10;
				else
					instance.gameObject.layer = 11;
				initialAngle--;
			}
		}
	}

	private void CircleShootType()
	{
		if (Time.time >= nextTimeToFire)
		{
            FindObjectOfType<AudioManagerScript>().Play("ShootSound");

            nextTimeToFire = Time.time + 1 / fireRate;
			GameObject instance = objectPooler.SpawnFromPool("CircleBulletFormation", firePoint.position, transform.rotation);
            CircleBulletFormationScript script = instance.GetComponent<CircleBulletFormationScript>();

            script.tagName = tagName;
			script.damage = damage;
			script.nrOfBulletsPerShoot = nrOfBulletsPerShoot;
			script.circleRadius = circleRadius;
			script.speed = 25;
			script.maxDistance = maxDistance;
			script.OnObjectSpawn();
		}
	}

	private void ArcShootType()
	{
		if (Time.time >= nextTimeToFire)
		{
            FindObjectOfType<AudioManagerScript>().Play("ShootSound");

            nextTimeToFire = Time.time + 1 / fireRate;
			GameObject instance = objectPooler.SpawnFromPool("ArcBulletFormation", firePoint.position, firePoint.rotation);

            ArcBulletFormationScript script = instance.GetComponent<ArcBulletFormationScript>();

			script.layer = layer;
			script.damage = damage;
			script.nrOfBulletsPerShoot = nrOfBulletsPerShoot;
			script.circleRadius = circleRadius;
			script.speed = 30;
			script.maxDistance = maxDistance;
			script.OnObjectSpawn();
		}
	}

	public static Transform FindClosestEnemy(Vector3 pos)
	{
		Enemy result = null;
		float dist = float.PositiveInfinity;
		var e = Enemy.Pool.GetEnumerator();
		while (e.MoveNext())
		{
			float d = (e.Current.transform.position - pos).sqrMagnitude;
			if (d < dist)
			{
				result = e.Current;
				dist = d;
			}
		}
		return result.transform;
	}

	public static Transform FindClosestAlly(Vector3 pos)
	{
		Ally result = null;
		float dist = float.PositiveInfinity;
		var e = Ally.Pool.GetEnumerator();
		while (e.MoveNext())
		{
			float d = (e.Current.transform.position - pos).sqrMagnitude;
			if (d < dist)
			{
				result = e.Current;
				dist = d;
			}
		}
		return result.transform;
	}
}