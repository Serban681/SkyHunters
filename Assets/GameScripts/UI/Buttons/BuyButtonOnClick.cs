using UnityEngine;

public class BuyButtonOnClick : MonoBehaviour
{
	public Animator backButton;
	private Animator anim;
	private float timer = 0f;
	private bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
    }

	private void Update()
	{
		if (clicked)
		{
			if (timer > 1f)
			{
				backButton.gameObject.SetActive(false);
				gameObject.SetActive(false);
			}

			if(timer <= 1f)
				timer += Time.deltaTime;
		}
	}

	public void OnClick()
	{
		backButton.SetTrigger("FadeOut");
		anim.SetTrigger("FadeOut");
		clicked = true;
	}
}
