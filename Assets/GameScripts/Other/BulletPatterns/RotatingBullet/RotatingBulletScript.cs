using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBulletScript : MonoBehaviour
{
	public float circleRadius;
	public float damage;

	public float givenRotZ;
	public Vector2 direction;

	private Vector3 lastPos;
	[HideInInspector]
	public float maxDistance;

	private ObjectPooler objectPooler;

	private void Awake()
	{
		objectPooler = ObjectPooler.instance;
	}

	private void Start()
	{
		transform.rotation = Quaternion.Euler(0, 0, givenRotZ);
	}

	void Update()
    {
		transform.localPosition = Vector3.Lerp(transform.localPosition, transform.right * circleRadius, 1f * Time.deltaTime);
		//transform.RotateAround(transform.parent.position, new Vector3(0, 0, 5), 180 * rotDir * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		objectPooler.SpawnFromPool("Spark", other.Distance(transform.GetComponent<Collider2D>()).pointB, Quaternion.identity);
		other.GetComponent<Health>().TakeDamage(damage);
		transform.parent = transform;
		gameObject.SetActive(false);
	}
}
