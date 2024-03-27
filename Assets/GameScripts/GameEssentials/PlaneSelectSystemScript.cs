using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MalbersAnimations.Selector;

public class PlaneSelectSystemScript : MonoBehaviour
{
	public int[][] planeStats;
	private Transform[] ww1Planes;
	private Transform[] ww2Planes;

	public Transform ww1PlanesChart;
	public Transform ww2PlanesChart;

	//public bool[] defaultValues1 = new bool[7];
	//public bool[] defaultValues2 = new bool[7];

    public SelectorController planeTypeSelector;
    public SelectorController ww1PlanesSelector;
    public SelectorController ww2PlanesSelector;

    public GameObject ww1PlaneList;
    public GameObject ww2PlaneList;
    public GameObject planeTypeList;

    void Start()
    {
		ww1Planes = ww1PlanesChart.GetComponentsInChildren<Transform>();
		ww2Planes = ww2PlanesChart.GetComponentsInChildren<Transform>();

		for (int i = 1; i <= StaticVariables.ww1PlanesStats.Length; i++)
		{
			ww1Planes[i].GetComponent<IndividualPlaneScript>().unlocked = StaticVariables.ww1PlanesStats[i-1];
		}

		for (int i = 1; i <= StaticVariables.ww1PlanesStats.Length; i++)
		{
			ww2Planes[i].GetComponent<IndividualPlaneScript>().unlocked = StaticVariables.ww2PlanesStats[i-1];
		}

        planeTypeSelector.focusedItemIndex = StaticVariables.playerPlaneType;

        planeTypeSelector.transform.position = new Vector3(-StaticVariables.playerPlaneType * 10, planeTypeSelector.transform.position.y, planeTypeSelector.transform.position.z);

        if(StaticVariables.playerPlaneType == 0)
        {          
            ww1PlaneList.SetActive(true);
            ww1PlanesSelector.focusedItemIndex = StaticVariables.focussedPlane;
            ww1PlanesSelector.transform.position = new Vector3(-StaticVariables.focussedPlane * 5, ww1PlanesSelector.transform.position.y, ww1PlanesSelector.transform.position.z);
        }
        else if (StaticVariables.playerPlaneType == 1)
        {         
            ww2PlaneList.SetActive(true);
            ww2PlanesSelector.focusedItemIndex = StaticVariables.focussedPlane;
            ww2PlanesSelector.transform.position = new Vector3(-StaticVariables.focussedPlane * 5, ww2PlanesSelector.transform.position.y, ww2PlanesSelector.transform.position.z);
        }		
	}

	private void Update()
	{
		ww1PlanesSelector.transform.GetComponent<ItemsControl>().SetAnims();
		ww2PlanesSelector.transform.GetComponent<ItemsControl>().SetAnims();
	}
}
