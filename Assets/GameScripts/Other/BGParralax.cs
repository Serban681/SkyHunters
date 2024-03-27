using UnityEngine;

public class BGParralax : MonoBehaviour
{
	public float parralaxEffect;
	private Vector2 startPos;
	public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
		startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		Vector2 dist = cam.transform.position * parralaxEffect;

		transform.position = new Vector2(startPos.x + dist.x, startPos.y + dist.y);
    }
}
