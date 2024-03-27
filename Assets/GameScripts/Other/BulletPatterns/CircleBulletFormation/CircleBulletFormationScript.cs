using UnityEngine;

public class CircleBulletFormationScript : MonoBehaviour, IPooledObject
{
	public float speed = 40;
	private Rigidbody2D rb;
	[HideInInspector]
	public Transform firePoint;

	public float maxDistance = 20;
	private float travelledDistance = 0f;
	private Vector3 lastPos;

	public float damage = 10;

	public int nrOfBulletsPerShoot = 5;
	//[HideInInspector]
	public float circleRadius = 2f;

	public string tagName = "Player";

	private float initialRotZ;

	private float angleIncrementer;

	private Transform[] children;

	private ObjectPooler objectPooler;

	void Awake()
	{
		objectPooler = ObjectPooler.instance;
	}

	// Start is called before the first frame update
	public void OnObjectSpawn()
	{
		lastPos = transform.position;
		travelledDistance = 0;

		initialRotZ = transform.eulerAngles.z;

		angleIncrementer = 360 / nrOfBulletsPerShoot;

		for (int i = 0; i < nrOfBulletsPerShoot; i++)
		{
			transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + i * angleIncrementer);
			GameObject instance = objectPooler.SpawnFromPool("ArcBullet", transform.position, Quaternion.identity);
            ArcBulletScript script = instance.GetComponent<ArcBulletScript>();
            script.direction = transform.right;
			script.circleRadius = circleRadius;
			script.damage = damage;
			
			instance.gameObject.SetActive(true);

			instance.transform.SetParent(transform);
			if (tagName == "Player")
				instance.gameObject.layer = 10;
			else
				instance.gameObject.layer = 11;
		}

		transform.rotation = Quaternion.Euler(0, 0, initialRotZ);

		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;

		children = transform.GetComponentsInChildren<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		travelledDistance += Vector3.Distance(transform.position, lastPos);

		if (travelledDistance >= maxDistance)
		{
			for (int i = 1; i < children.Length; i++)
			{
				if (children[i].gameObject.activeSelf)
				{
					objectPooler.SpawnFromPool("Spark", children[i].position, Quaternion.identity);
					children[i] = children[i].parent;
				}
			}
			gameObject.SetActive(false);
		}

		lastPos = transform.position;
	}
}
