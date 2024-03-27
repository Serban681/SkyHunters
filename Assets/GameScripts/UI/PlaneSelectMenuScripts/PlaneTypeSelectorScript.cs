using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations.Selector;

public class PlaneTypeSelectorScript : MonoBehaviour
{
	public GameObject selectorCam;
	public Transform[] children;
	private int focusedItem;
	private Color bgColor;

	void Start()
	{
		//for (int i = 0; i < children.Length; i++)
		//Debug.Log(children[i]);
		if (StaticVariables.playerPlaneType == 0)
		{
			selectorCam.transform.GetComponent<Camera>().backgroundColor = children[0].transform.GetComponent<PlaneTypeButtonScript>().backgroundColour;
		}
		else if(StaticVariables.playerPlaneType == 1)
		{
			selectorCam.transform.GetComponent<Camera>().backgroundColor = children[1].transform.GetComponent<PlaneTypeButtonScript>().backgroundColour;
		}
	}

	void Update()
	{
		focusedItem = transform.GetComponent<SelectorController>().focusedItemIndex;

		selectorCam.transform.GetComponent<Camera>().backgroundColor = Color.Lerp(selectorCam.transform.GetComponent<Camera>().backgroundColor, bgColor, 10f * Time.deltaTime);

		if(focusedItem == 0)
		{
			StaticVariables.playerPlaneType = 0;
			bgColor = children[0].transform.GetComponent<PlaneTypeButtonScript>().backgroundColour;
			children[0].transform.GetComponent<PlaneTypeButtonScript>().Focussed();
			children[1].transform.GetComponent<PlaneTypeButtonScript>().NotFocussed();
		}
		else if(focusedItem == 1)
		{
			StaticVariables.playerPlaneType = 1;
            bgColor = children[1].transform.GetComponent<PlaneTypeButtonScript>().backgroundColour;
			children[1].transform.GetComponent<PlaneTypeButtonScript>().Focussed();
			children[0].transform.GetComponent<PlaneTypeButtonScript>().NotFocussed();
		}		
	}	
}