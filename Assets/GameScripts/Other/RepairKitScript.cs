using UnityEngine;

public class RepairKitScript : MonoBehaviour
{
	private Rigidbody2D rb;
	public float speed;
	public GameObject greyArrow;
	private GameObject player;

	[HideInInspector]
	public GameObject arrowInstance;

	private float timer = 0f;
	private float delay = 3f;

	// Start is called before the first frame update
	void Start()
    {
		rb = transform.GetComponent<Rigidbody2D>();
		rb.velocity = -Vector2.up * speed;

		player = PlayerController.instance.transform.gameObject;
		arrowInstance = Instantiate(greyArrow, player.transform);
		arrowInstance.transform.GetComponent<PointingArrowScript>().AssignTarget(transform);
	}

	void Update()
	{
		if (timer >= delay)
		{
			if (transform.position.y <= -800 || transform.position.x <= -800 || transform.position.x >= 800)
			{
				//EnemySpawner.instance.curRepairKits--;
				Destroy(arrowInstance);
				Destroy(gameObject);
			}
			timer = 0f;
		}

		timer += Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        AudioManagerScript.instance.Play("RepairKitPickSound");

		//collision.transform.GetComponent<PlayerHealth>().RestoreHealth();
		//EnemySpawner.instance.curRepairKits--;
		Destroy(arrowInstance);
		Destroy(gameObject);
	}
}
