using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
	private Rigidbody2D rb;
	public float speed = 10;
	public float damage = 10f;

	public GameObject replacement;

	private float distance;
	private Vector3 subtract;

	private Transform player;

	private float timer = 0f;
	private float delay = 2f;

    // Start is called before the first frame update
    void Start()
    {
		player = PlayerController.instance.transform;

		rb = GetComponent<Rigidbody2D>();

		if(transform.localScale.x == 1)
			rb.velocity = -transform.right * speed;
		else
			rb.velocity = transform.right * speed;
	}

    // Update is called once per frame
    void Update()
    {
		if (timer >= delay)
		{
			if (StaticVariables.playerHealth > 0)
			{
				subtract = transform.position - player.position;
				distance = Vector3.Magnitude(subtract);
			}
			if (distance >= 300)
			{
				EnemySpawner.instance.currentNumberOfBirds--;
				Destroy(gameObject);
			}

			timer = 0f;
		}
		timer += Time.deltaTime;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        AudioManagerScript.instance.Play("BirdHitSound");

		collision.GetComponent<Health>().TakeDamage(damage);
		GameObject instance = Instantiate(replacement, transform.position, Quaternion.identity);
		Rigidbody2D[] rbs = instance.GetComponentsInChildren<Rigidbody2D>();
		foreach (Rigidbody2D rb in rbs)
		{
			Vector2 dir = new Vector2(rb.position.x - transform.position.x, rb.position.y - transform.position.y);
			dir.Normalize();
			rb.AddForce(dir * 2, ForceMode2D.Impulse);
		}
		instance.transform.localScale = transform.localScale;
		EnemySpawner.instance.currentNumberOfBirds--;
		Destroy(gameObject);
	}
}
