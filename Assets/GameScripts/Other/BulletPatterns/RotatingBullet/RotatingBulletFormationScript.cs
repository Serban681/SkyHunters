using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBulletFormationScript : MonoBehaviour, IPooledObject
{
	public int rotDir = 1;

	public float speed = 40;
	private Rigidbody2D rb;
	[HideInInspector]
	public Transform firePoint;

	private float travelledDistance = 0f;
	private Vector3 lastPos;
	public float maxDistance = 20;

	public float damage = 10;

	public int nrOfBulletsPerShoot = 5;
	//[HideInInspector]
	public float circleRadius = 2f;

	public string tagName = "Player";

	public GameObject bullet;

	private float initialRotZ;

	private float angleIncrementer;

	private Transform[] children;

	private ObjectPooler objectPooler;

	private void Awake()
	{
		objectPooler = ObjectPooler.instance;
	}

	// Start is called before the first frame update
	public void OnObjectSpawn()
	{
		travelledDistance = 0;
		initialRotZ = transform.eulerAngles.z;

		angleIncrementer = 360 / nrOfBulletsPerShoot;

		for (int i = 0; i < nrOfBulletsPerShoot; i++)
		{
			transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + i * angleIncrementer);
			GameObject instance = objectPooler.SpawnFromPool("RotatingBullet", transform.position, Quaternion.identity);
            RotatingBulletScript script = instance.GetComponent<RotatingBulletScript>();
			script.circleRadius = circleRadius;
			script.damage = damage;
			script.direction = transform.right;
			script.givenRotZ = transform.eulerAngles.z;
			script.maxDistance = maxDistance;
			instance.transform.SetParent(transform);
			if (tagName == "Player")
				instance.gameObject.layer = 10;
			else
				instance.gameObject.layer = 11;
			instance.transform.gameObject.SetActive(true);
		}

		transform.rotation = Quaternion.Euler(0, 0, initialRotZ);

		lastPos = transform.position;
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;

		children = transform.GetComponentsInChildren<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
        transform.Rotate(new Vector3(0, 0, 60 * Time.deltaTime * rotDir));

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
