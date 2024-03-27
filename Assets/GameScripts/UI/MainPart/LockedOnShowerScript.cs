using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedOnShowerScript : MonoBehaviour
{
	private float timer = 0f;
	public bool lockedOn = true;
	private Animator anim;

	private bool isRunning = false;//to make sure the couroutine doesn't stack and stops working
	private bool isRunningLO = false;

	private bool changeYellow = true;
	private bool changeRed = true;

	// Start is called before the first frame update
	void Start()
    {
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if (lockedOn == true)
		{
			anim.SetBool("LockedOn", true);

			if (timer >= 1f && timer < 2f)
			{
				if (isRunning == false && changeYellow == true)
				{
					StartCoroutine(changeState());
					changeYellow = false;
				}
				anim.SetInteger("State", 1);
			}
			else if(timer >= 2f)
			{
				if (isRunning == false && changeRed == true)
				{
					StartCoroutine(changeState());
					changeRed = false;
				}
				anim.SetInteger("State", 2);
			}

			timer += Time.deltaTime;
		}
		else
		{
			if (isRunningLO == false)
			{
				StartCoroutine(NotTargeted());
			}

			/*
			anim.SetBool("LockedOn", false);
			anim.SetInteger("State", 0);
			changeYellow = true;
			changeRed = true;
			timer = 0f;
			*/
		}
	}

	private IEnumerator changeState()
	{
		isRunning = true;
		anim.SetBool("ChangingState", true);
		yield return new WaitForSeconds(0.20f * 100 / 60);
		anim.SetBool("ChangingState", false);
		isRunning = false;
	}

	private IEnumerator NotTargeted()
	{
		isRunningLO = true;

		anim.SetBool("LockedOn", false);
		yield return new WaitForSeconds(0.30f * 100 / 60);
		anim.SetInteger("State", 0);
		changeYellow = true;
		changeRed = true;
		timer = 0f;

		isRunningLO = false;
	}
}
