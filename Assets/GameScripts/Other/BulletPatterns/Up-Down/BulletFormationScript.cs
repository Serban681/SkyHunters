using UnityEngine;

public class BulletFormationScript : MonoBehaviour, IPooledObject
{
	public float speed;
	private Rigidbody2D rb;

	public float maxDistance;

	[HideInInspector]
	public float damage = 10;

	FlowerBullet[] children;

	private float travelledDistance = 0f;
	private Vector3 lastPos;

	private ObjectPooler objectPooler;

	private void Awake()
	{
		objectPooler = ObjectPooler.instance;
		children = transform.GetComponentsInChildren<FlowerBullet>();
	}

	// Start is called before the first frame update
	public void OnObjectSpawn()
    {
		lastPos = transform.position;
		travelledDistance = 0f;
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * speed;

		for(int i = 0; i < children.Length; i++)
		{
			//child.transform.gameObject.SetActive(true);
			children[i].OnObjectSpawn();
		}
	}

	public void Update()
	{
		travelledDistance += Vector3.Distance(transform.position, lastPos);

		if (travelledDistance >= maxDistance)
		{
			for (int i = 0; i < children.Length; i++)
			{
				if(children[i].transform.gameObject.activeSelf)
					objectPooler.SpawnFromPool("Spark", children[i].transform.position, Quaternion.identity);
			}
			transform.gameObject.SetActive(false);
		}

		lastPos = transform.position;
	}
}
