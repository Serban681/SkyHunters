using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthSliderScript : MonoBehaviour
{
	private Animator anim;
    private Slider slider;

	private float initialHealth;

	private bool shouldLerp;

    // Start is called before the first frame update
    void Start()
    {
		anim = transform.Find("Fill Area").transform.Find("Fill").transform.GetComponent<Animator>();
        slider = GetComponent<Slider>();
		anim.SetBool("HasStarted", false);
		initialHealth = StaticVariables.playerHealth;
		slider.maxValue = initialHealth;
	}

	// Update is called once per frame
	void Update()
	{
		if (StaticVariables.playerHealth < initialHealth)
		{
			anim.SetBool("HasStarted", true);
		}

		

		if (slider.value <= StaticVariables.playerHealth - 1)
		{
			anim.SetBool("FadeIn", true);
			//Mathf.Lerp(slider.value, StaticVariables.playerHealth, 0.1f);
			transform.rotation = Quaternion.identity;
		}
		else if (slider.value >= StaticVariables.playerHealth + 1)
		{
			anim.SetBool("FadeIn", true);
			//Mathf.Lerp(slider.value, StaticVariables.playerHealth, 0.1f);
			transform.rotation = Quaternion.identity;
		}
		else
		{
			anim.SetBool("FadeIn", false);
		}

		slider.value = StaticFunctions.LerpValue(slider.value, StaticVariables.playerHealth, 0.5f);
		//Debug.Log(slider.value + "   ---   " + StaticVariables.playerHealth);
		
	}
	/*
	public IEnumerator HealthStatusModified()
	{
		slider.value = StaticFunctions.LerpValue(slider.value, StaticVariables.playerHealth, 0.5f);
		anim.SetBool("FadeIn", true);
		transform.rotation = Quaternion.identity;
		yield return new WaitForSeconds(2f);
		anim.SetBool("FadeIn", false);
	}
	*/
	public void StartAnimator()
	{
		anim.SetBool("HasStarted", true);
	}
}
