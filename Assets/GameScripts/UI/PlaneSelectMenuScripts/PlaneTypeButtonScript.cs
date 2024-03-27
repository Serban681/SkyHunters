using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations.Selector;

public class PlaneTypeButtonScript : MonoBehaviour
{
	public Color backgroundColour;
	public GameObject planeSelector;

    public void Focussed()
	{
		IncreaseScale();
		planeSelector.SetActive(true);
	}

	public void NotFocussed()
	{
		planeSelector.transform.Find("Items").GetComponent<SelectorController>().focusedItemIndex = 0;
		DecreaseScale();
		planeSelector.SetActive(false);
	}

	private void IncreaseScale()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), 10f * Time.deltaTime);
	}

	private void DecreaseScale()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.7f, 0.7f, 1), 10f * Time.deltaTime);
	}
}