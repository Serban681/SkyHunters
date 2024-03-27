using UnityEngine;

public class StarGenerator : MonoBehaviour
{
	public GameObject star;

	private void Start()
	{
		for (int i = -15; i < 15; i++)
			for (int j = -15; j < 15; j++)
			{
				GameObject instance = Instantiate(star, new Vector3(transform.position.x + i * 6 + Random.Range(-20, 20), transform.position.y + j * 60 + Random.Range(-20, 20), 0), Quaternion.identity);
				instance.transform.parent = transform;
			}
	}
}
