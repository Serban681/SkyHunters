using UnityEngine;

public class HeartScript : MonoBehaviour
{
	private float timer = 0f;
	private float delay = 3f;

	private float distance;
	private Vector3 subtract;

	private Transform player;

	private void Start()
	{
		player = PlayerController.instance.transform;

		if (transform.position.y <= -800 || transform.position.x <= -800 || transform.position.x >= 800)
		{
			EnemySpawner.instance.curHearts--;
			Destroy(gameObject);
		}
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
				EnemySpawner.instance.curHearts--;
				Destroy(gameObject);
			}

			timer = 0f;
		}
		timer += Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		AudioManagerScript.instance.Play("RepairKitPickSound");

		collision.transform.GetComponent<PlayerHealth>().RestoreHealth(20);
		EnemySpawner.instance.curHearts--;
		Destroy(gameObject);
	}
}
