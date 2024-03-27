using UnityEngine;

public class GlitterScript : MonoBehaviour
{
	private float offset;

	private float speed = 1;

    void Start()
    {
		offset = Random.Range(0, 1f);
    }

    void Update()
    {
		float scaleInOut = Mathf.Abs(Mathf.Sin(Time.time * speed + offset)) / 2;

		transform.localScale = new Vector3(0.5f + scaleInOut, 0.5f + scaleInOut, 1);
    }
}