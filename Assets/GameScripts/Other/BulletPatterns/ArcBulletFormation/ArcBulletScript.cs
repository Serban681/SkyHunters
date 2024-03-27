using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcBulletScript : MonoBehaviour
{	
	[HideInInspector]
	public float damage = 10;

	public GameObject spark;

	[HideInInspector]
	public Vector3 direction;

	[HideInInspector]
	public float circleRadius;

	private ObjectPooler objectPooler;

	private void Awake()
	{
		objectPooler = ObjectPooler.instance;
	}

	// Update is called once per frame
	void Update()
    {
		transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(direction.x * circleRadius, direction.y * circleRadius, 0), 1f * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		objectPooler.SpawnFromPool("Spark", other.Distance(transform.GetComponent<Collider2D>()).pointB, Quaternion.identity);
		other.GetComponent<Health>().TakeDamage(damage);
		transform.parent = transform;
		gameObject.SetActive(false);
	}
}
