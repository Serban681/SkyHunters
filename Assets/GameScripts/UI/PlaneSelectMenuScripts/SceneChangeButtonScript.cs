using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangeButtonScript : MonoBehaviour
{
	public Animator camAnim;
	private Canvas UI;

	private void Start()
	{
		UI = transform.parent.parent.transform.GetComponent<Canvas>();
	}

	public void OnClick()
	{
		UI.renderMode = RenderMode.ScreenSpaceCamera;
		camAnim.SetBool("PixelsOut", false);
	}
}
