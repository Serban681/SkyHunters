using System.Collections;
using UnityEngine;

public class RocketScript : MonoBehaviour, IPooledObject
{
	public float damage = 100;
	public GameObject explosion;

	public float currentSpeed;
	public float finalSpeed;

	private float timer = 0f;
	private float incrementer;
	private float initialRotationZ;

	private Rigidbody2D rb;

	public float maxDistance = 10;
	float travelledDistance = 0f;
	Vector3 lastPos;

	public float maxIncrementation = 25;

	private Transform smokeTrail;

    public GameObject smoke;

	// Start is called before the first frame update
	public void OnObjectSpawn()
    {
        smokeTrail = Instantiate(smoke, transform.position, Quaternion.identity, transform).transform;

        travelledDistance = 0;
		lastPos = transform.position;

		initialRotationZ = transform.rotation.eulerAngles.z;
		rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
		rb.velocity = transform.right * currentSpeed;

		travelledDistance += Vector3.Distance(transform.position, lastPos);

		if (travelledDistance >= maxDistance)
		{
			FindObjectOfType<AudioManagerScript>().Play("ExplosionSound");
			FindObjectOfType<CameraShake>().Shake(3f, 3f, 2f);
			Instantiate(explosion, transform.position, Quaternion.identity);
			smokeTrail.parent = null;
			smokeTrail.GetComponent<SmokeTrailScript>().PlaneDestroyed();

			gameObject.SetActive(false);
		}

		lastPos = transform.position;

		timer += Time.deltaTime;

		if (timer >= 0.25f)
		{
			incrementer = Random.Range(-maxIncrementation, maxIncrementation);
			timer = 0f;
		}

		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, initialRotationZ + incrementer), 1f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		FindObjectOfType<AudioManagerScript>().Play("ExplosionSound");

		FindObjectOfType<CameraShake>().Shake(3f, 3f, 2f);
		Instantiate(explosion, collision.Distance(transform.GetComponent<Collider2D>()).pointB, Quaternion.identity);
		collision.GetComponent<Health>().TakeDamage(damage);
		smokeTrail.parent = null;
		smokeTrail.GetComponent<SmokeTrailScript>().PlaneDestroyed();

		gameObject.SetActive(false);
	}

	private IEnumerator SpeedRegulator()
	{
		yield return new WaitForSeconds(1f);
		currentSpeed = Mathf.Lerp(currentSpeed, finalSpeed, 1f);
		yield return new WaitForSeconds(3f);
	}
}
