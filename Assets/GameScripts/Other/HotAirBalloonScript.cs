using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotAirBalloonScript : MonoBehaviour
{
	public GameObject replacement;
	public GameObject explosion;
	public Transform explosionPoint;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		collision.transform.GetComponent<Health>().TakeDamage(5);
		collision.transform.GetComponent<Health>().TakeDamage(3000);

		GameObject instance = Instantiate(replacement, transform.position, Quaternion.identity);

		//Instantiate(explosion, explosionPoint.position, Quaternion.identity);

		Destroy(gameObject);
		/*
		Rigidbody2D[] rbs = instance.GetComponentsInChildren<Rigidbody2D>();
		foreach (Rigidbody2D rb in rbs)
		{
			Vector2 dir = new Vector2(rb.position.x - transform.position.x, rb.position.y - transform.position.y);
			dir.Normalize();
			rb.AddForce(dir * 2, ForceMode2D.Impulse);
		}
		instance.transform.localScale = transform.localScale;
		FindObjectOfType<EnemySpawner>().currentNumberOfBirds--;
		*/
	}
}
