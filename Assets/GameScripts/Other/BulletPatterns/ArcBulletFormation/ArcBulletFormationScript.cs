using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcBulletFormationScript : MonoBehaviour, IPooledObject
{
	public float speed = 40;
	private Rigidbody2D rb;
	[HideInInspector]
	public Transform firePoint;

	[HideInInspector]
	public int layer;

	private float travelledDistance = 0f;
	private Vector3 lastPos;
	public float maxDistance = 20;

	public float damage = 10;

	public int nrOfBulletsPerShoot = 5;

	public float circleRadius = 2f;

	private float initialRotZ;

	private ObjectPooler objectPooler;
	private Transform[] children;

	private void Awake()
	{
		objectPooler = ObjectPooler.instance;
	}

	public void OnObjectSpawn()
	{
		travelledDistance = 0;

		initialRotZ = transform.eulerAngles.z;

		int initialAngle = nrOfBulletsPerShoot / 2;

		for (int i = 0; i < nrOfBulletsPerShoot; i++)
		{
			transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + initialAngle * 20);
			GameObject instance = objectPooler.SpawnFromPool("ArcBullet", transform.position, Quaternion.identity);
            ArcBulletScript script = instance.GetComponent<ArcBulletScript>();
            script.direction = transform.right;
			script.circleRadius = circleRadius;
			script.damage = damage;
			instance.transform.SetParent(transform);

			instance.gameObject.SetActive(true);
			instance.gameObject.layer = layer;
			initialAngle--;
		}

		transform.rotation = Quaternion.Euler(0, 0, initialRotZ);

		lastPos = transform.position;
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;

		children = transform.GetComponentsInChildren<Transform>();
	}

	private void Update()
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
