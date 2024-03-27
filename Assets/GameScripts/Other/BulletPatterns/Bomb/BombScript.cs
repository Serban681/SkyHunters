using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
	private Rigidbody2D rb;
	public float speed = 1f;

	private float timer = 0f;
	public float timeToExplode = 3f;

	public GameObject explosion;

	public float damage = 70;
	private float radius;

	[HideInInspector]
	public Vector2 right;

	// Start is called before the first frame update
	void Start()
    {
		rb = transform.GetComponent<Rigidbody2D>();
		//rb.velocity = -Vector2.up * speed;

		radius = transform.GetComponent<CircleCollider2D>().radius;

		rb.AddForce(right * 500);
	}

    // Update is called once per frame
    void Update()
    {
		if(timer >= timeToExplode)
		{
			FindObjectOfType<CameraShake>().Shake(3f, 3f, 2f);
			Instantiate(explosion, transform.position, Quaternion.identity);
			FindObjectOfType<AudioManagerScript>().Play("ExplosionSound");
			Destroy(gameObject);
		}

		timer += Time.deltaTime;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//collision.transform.GetComponent<Health>().TakeDamage(damage);

		StartCoroutine(Explode(collision));
	}

	private IEnumerator Explode(Collider2D col)
	{
		//Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);

		yield return new WaitForSeconds(0.3f);		

		//foreach(Collider2D col in collisions)
		//{
			col.transform.GetComponent<Health>().TakeDamage(damage);
		//}

		FindObjectOfType<CameraShake>().Shake(3f, 3f, 2f);
		FindObjectOfType<AudioManagerScript>().Play("ExplosionSound");
		Instantiate(explosion, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}
}
