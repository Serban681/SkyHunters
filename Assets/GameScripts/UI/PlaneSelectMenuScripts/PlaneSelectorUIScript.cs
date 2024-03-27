using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSelectorUIScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(Beginning());
    }


	private IEnumerator Beginning()
	{
		transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
		yield return new WaitForSeconds(1f);
		transform.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
	}
}
