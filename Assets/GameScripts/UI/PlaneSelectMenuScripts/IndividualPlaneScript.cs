using UnityEngine;

public class IndividualPlaneScript : MonoBehaviour
{
	public bool unlocked = false;
	public Sprite initialSprite;
	public Sprite focussedSprite;
	private bool isChangingPlane;

	private Animator anim;

	private void Start()
	{
		initialSprite = transform.GetComponent<SpriteRenderer>().sprite;

		anim = GetComponent<Animator>();
	}

	public void Focussed()
	{
		if(unlocked == true)
			transform.GetComponent<SpriteRenderer>().sprite = focussedSprite;

		IncreaseScale();
	}
	public void Unfocussed()
	{
		transform.GetComponent<SpriteRenderer>().sprite = initialSprite;
		DecreaseScale();
	}

	private void IncreaseScale()
	{
		if(unlocked == true)
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.3f, 1.3f, 1), 10f * Time.deltaTime);
	}
	private void DecreaseScale()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), 10f * Time.deltaTime);
	}
}
