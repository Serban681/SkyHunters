using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBulletScript : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb;
	[HideInInspector]
	public Transform firePoint;

	[HideInInspector]
	public float damage = 15;
	public float radius = 1f;

	private float travelledDistance = 0f;
	private Vector3 lastPos;
	public float maxDistance;

	public GameObject explosion;

	// Start is called before the first frame update
	void Start()
    {
		lastPos = transform.position;
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;
	}

    // Update is called once per frame
    void Update()
    {
		travelledDistance += Vector3.Distance(transform.position, lastPos);
		
		if (travelledDistance >= maxDistance)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

			foreach(Collider2D col in colliders)
			{
				Debug.Log(col);
				if(col.transform.GetComponent<Health>() != null)
					col.transform.GetComponent<Health>().TakeDamage(damage);
			}

			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		
		lastPos = transform.position;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

		foreach (Collider2D col in colliders)
		{
			if(col.transform.GetComponent<Health>() != null)
				col.transform.GetComponent<Health>().TakeDamage(damage);
		}
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
