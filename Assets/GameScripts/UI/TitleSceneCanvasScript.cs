using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneCanvasScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		AudioManagerScript.instance.Play("TitleTheme");

		StartCoroutine(Beginning());
	}

	private IEnumerator Beginning()
	{
		transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
		yield return new WaitForSeconds(1f);
		transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
	}
}
