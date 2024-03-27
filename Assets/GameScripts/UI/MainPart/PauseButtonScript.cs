using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonScript : MonoBehaviour
{
	public static PauseButtonScript instance;

	private Animator anim;
	private bool clicked = false;
	private Canvas otherCanvas;
	private Animator camAnim;
	private Canvas thisCanvas;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		anim = GetComponent<Animator>();
		otherCanvas = FindObjectOfType<CanvasScript>().GetComponent<Canvas>();
		camAnim = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<Animator>();
		thisCanvas = transform.parent.GetComponent<Canvas>();
		StartCoroutine(Entry());
	}

	public void OnClick()
	{
		if (clicked == false)
		{
			otherCanvas.renderMode = RenderMode.ScreenSpaceCamera;
			anim.SetBool("GetBigger", true);
			camAnim.SetBool("PixelsOut", false);
			Time.timeScale = 0f;
			clicked = true;
		}
		else if(clicked == true)
		{
			StartCoroutine(UnPauseClick());
		}

		TimeManagerScript.instance.TogglePause();
	}

	private IEnumerator UnPauseClick()
	{
		anim.SetBool("GetBigger", false);
		camAnim.SetBool("PixelsOut", true);
		Time.timeScale = 1f;
		yield return new WaitForSeconds(1f);
		otherCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
		clicked = false;
	}

	private IEnumerator Entry()
	{
		thisCanvas.renderMode = RenderMode.ScreenSpaceCamera;
		yield return new WaitForSeconds(1f);
		thisCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
	}

	public IEnumerator Dissappear()
	{
		anim.SetTrigger("Dissappear");
		yield return new WaitForSeconds(1f);
		transform.gameObject.SetActive(false);
	}
}
