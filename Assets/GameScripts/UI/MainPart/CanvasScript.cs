using System.Collections;
using UnityEngine;
using System;

public class CanvasScript : MonoBehaviour
{
	public Animator moveJoystick;
	public Animator comeBackText;
	public Animator scoreText;
	public Animator highScoreText;
	public Animator buttons;
	public Animator coinSystem;



	// Start is called before the first frame update
	void Awake()
    {
        StartCoroutine(Beginning());
    }

	private void Start()
	{
		PlayerHealth.instance.OnPlayerDeath += _OnPlayerDeath;
	}

	/*
	// Update is called once per frame
	void Update()
    {
		if(StaticVariables.playerHealth <= 0)
		{
			StartCoroutine(FadeOutAll());

            transform.GetComponent<EndBarsManager>().ShowBars();
		}		
	}
	*/

	public IEnumerator FadeOutAll()
	{
		moveJoystick.SetBool("FadeOut", true);
		comeBackText.SetBool("FadeOut", true);
		scoreText.SetBool("Grow", true);
		buttons.SetBool("Appear", true);
		coinSystem.SetBool("FadeIn", true);

		yield return new WaitForSeconds(1f); 

		moveJoystick.transform.gameObject.SetActive(false);
		comeBackText.transform.gameObject.SetActive(false);
		highScoreText.SetBool("Appear", true);

		if (StaticVariables.currentScore > StaticVariables.highScore)
		{
			StaticVariables.highScore = StaticVariables.currentScore;
			
		}
	}

	private IEnumerator Beginning()
	{
		transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
		yield return new WaitForSeconds(1f);
		transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
	}

	private void _OnPlayerDeath(object sender, EventArgs e)
	{
		StartCoroutine(FadeOutAll());

		transform.GetComponent<EndBarsManager>().ShowBars();
	}
}
