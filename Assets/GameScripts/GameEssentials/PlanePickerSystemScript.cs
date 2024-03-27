using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanePickerSystemScript : MonoBehaviour
{
	public GameObject continueButton;

	private int planeType;
	private int planeIndex;

	private List<int> unlockablePlaneIndexes = new List<int>();
	private List<bool> theModifiedPlaneList = new List<bool>();

    //For Plane Types
    private List<int> selectableTypes = new List<int>();
    private List<bool> planes = new List<bool>();

	public Text coinText;
	private int finalCoinAmount;

    public GameObject unlockedPlaneModel;
    public Transform spawnPoint;

    private int selectedPlaneType;
    private int selectedPlaneIndex;

    private bool firstTime = true;

	private void Awake()
	{
		coinText.text = StaticVariables.coins.ToString();
        finalCoinAmount = StaticVariables.coins;
	}

	private void Update()
	{
		StaticVariables.coins = (int)Mathf.Lerp(StaticVariables.coins, finalCoinAmount, 1f * Time.deltaTime);
		coinText.text = StaticVariables.coins.ToString();
	}

	public void OnClick()
	{
		continueButton.transform.GetComponent<Button>().interactable = false;
		SelectPlane();
	}

	private void FillList(bool[] planeType)
	{
		for(int i = 0; i < planeType.Length; i++)
			theModifiedPlaneList.Add(planeType[i]);
	}

	private void PickPlane()
	{
		for(int i = 0; i < theModifiedPlaneList.Count; i++)
		{
			if(theModifiedPlaneList[i] == false)
			{
				unlockablePlaneIndexes.Add(i);
			}
		}

		int x = Random.Range(0, unlockablePlaneIndexes.Count);
		theModifiedPlaneList[unlockablePlaneIndexes[x]] = true;

        selectedPlaneIndex = unlockablePlaneIndexes[x];
	}

	private void SelectPlane()
	{
        if (finalCoinAmount >= 100)
        {
            unlockedPlaneModel.transform.GetComponent<Animator>().SetBool("SecondPhase", false);

            ChoosableTypes();

            planeType = (int)Random.Range(0, selectableTypes.Count);

            selectedPlaneType = selectableTypes[planeType];

            if (selectableTypes[planeType] == 1)
            {
                FillList(StaticVariables.ww1PlanesStats);
            }
            else if (selectableTypes[planeType] == 2)
            {
                FillList(StaticVariables.ww2PlanesStats);
            }

            PickPlane();

            for (int i = 0; i < theModifiedPlaneList.Count; i++)
            {
                if (selectableTypes[planeType] == 1)
                {
					StaticVariables.ww1PlanesStats[i] = theModifiedPlaneList[i];
                }
                else if (selectableTypes[planeType] == 2)
                {
					StaticVariables.ww2PlanesStats[i] = theModifiedPlaneList[i];
                }
            }           

            theModifiedPlaneList.Clear();
			unlockablePlaneIndexes.Clear();

            if (firstTime == true)
            {
                unlockedPlaneModel.SetActive(true);
                unlockedPlaneModel.transform.GetComponent<Animator>().SetBool("FirstPhase", true);
                firstTime = false;
            }

            unlockedPlaneModel.GetComponent<UnlockedPlaneModelScript>().ChangeSprites(selectedPlaneType, selectedPlaneIndex);

            continueButton.transform.GetComponent<ContinueButtonScript>().setImg(selectedPlaneType, selectedPlaneIndex);

            selectableTypes.Clear();
            finalCoinAmount -= 100;
			/*
            if (planeType + 1 == 1)
            {
                ES3.Save("ww1Planes", ww1Planes);
            }
            else if (planeType + 1 == 2)
            {
                ES3.Save("ww2Planes", ww2Planes);
            }
			*/
            Debug.Log(planeType + " plane type");

            ES3.Save<int>("coins", finalCoinAmount);

            ShowContinueButton();
        }
        else {
            return;
        }
	}

	private void ShowContinueButton()
	{
		continueButton.SetActive(true);
	}

    private bool VerifyAvaility(bool[] planeType)
    {
        for (int i = 0; i < planeType.Length; i++)
            planes.Add(planeType[i]);

        for (int i = 0; i < planes.Count; i++)
        {
            if (planes[i] == false)
                return true;
        }

        return false;
    }

    private void ChoosableTypes()
    {
        if (VerifyAvaility(StaticVariables.ww1PlanesStats) == true)
        {
            selectableTypes.Add(1);
            planes.Clear();
        }
        if (VerifyAvaility(StaticVariables.ww2PlanesStats) == true)
        {
            selectableTypes.Add(2);
            planes.Clear();
        }
    }
}
