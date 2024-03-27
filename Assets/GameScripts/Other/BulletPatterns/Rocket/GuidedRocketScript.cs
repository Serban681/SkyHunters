using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedRocketScript : MonoBehaviour
{
	public float damage = 100;
	public GameObject explosion;

	public float currentSpeed;
	public float finalSpeed;

	private Rigidbody2D rb;

	public float maxDistance = 10;
	float travelledDistance = 0f;
	Vector3 lastPos;

	public float maxIncrementation = 25;

	public Transform target;

	// Start is called before the first frame update
	void Start()
	{
		lastPos = transform.position;

		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		rb.velocity = transform.right * currentSpeed;

		travelledDistance += Vector3.Distance(transform.position, lastPos);

		if (travelledDistance >= maxDistance)
		{
			FindObjectOfType<CameraShake>().Shake(3f, 3f, 2f);
			Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

		lastPos = transform.position;

		Vector2 direction = target.position - transform.position;
		direction.Normalize();

		float finalRotZ = (float)((Mathf.Atan2(direction.x, -direction.y) / Mathf.PI) * 180f) - 90;

		if (finalRotZ < 0)
			finalRotZ += 360f;

		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, finalRotZ), 3f);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		FindObjectOfType<CameraShake>().Shake(3f, 3f, 2f);
		Instantiate(explosion, other.Distance(transform.GetComponent<Collider2D>()).pointB, Quaternion.identity);
		other.GetComponent<Health>().TakeDamage(damage);
		Destroy(gameObject);
	}

	private IEnumerator SpeedRegulator()
	{
		yield return new WaitForSeconds(1f);
		currentSpeed = Mathf.Lerp(currentSpeed, finalSpeed, 1f);
		yield return new WaitForSeconds(3f);
	}
}
