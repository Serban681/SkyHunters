using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour, IPooledObject
{
    public float speed;
    private Rigidbody2D rb;

    [HideInInspector]
    public float damage = 15;

	private float travelledDistance = 0f;
	private Vector3 lastPos;
	public float maxDistance;

	private ObjectPooler objectPooler;

	private void Awake()
	{
		objectPooler = ObjectPooler.instance;
	}

	public void OnObjectSpawn()
    {
		travelledDistance = 0f;
		lastPos = transform.position;
		rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
	}

	private void Update()
	{
		travelledDistance += Vector3.Distance(transform.position, lastPos);

		if (travelledDistance >= maxDistance)
		{
			GameObject instance = objectPooler.SpawnFromPool("Spark", transform.position, Quaternion.identity);
			gameObject.SetActive(false);
		}

		lastPos = transform.position;
	}

	private void OnTriggerEnter2D(Collider2D other)
    {
		objectPooler.SpawnFromPool("Spark", other.Distance(transform.GetComponent<Collider2D>()).pointB, Quaternion.identity);
        other.GetComponent<Health>().TakeDamage(damage);
		gameObject.SetActive(false);
    }
}
