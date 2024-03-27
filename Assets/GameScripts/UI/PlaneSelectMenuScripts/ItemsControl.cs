using UnityEngine;
using MalbersAnimations.Selector;

public class ItemsControl : MonoBehaviour
{
	public GameObject priceText;
	public GameObject playImg;

	public Animator planeName;

	[HideInInspector]
	public bool isChangingPlane = false;

	private Transform[] children = new Transform[100];
	private int focusedItem;
	public int planeType = 0;

    public SelectButtonScript selectButton;

    // Start is called before the first frame update
    void Awake()
    {
		children = transform.GetComponentsInChildren<Transform>();
	}

    // Update is called once per frame
    void Update()
    {
		focusedItem = transform.GetComponent<SelectorController>().focusedItemIndex;

		for(int i = 0; i < children.Length; i++)
		{
			if(i == focusedItem)
			{
				children[i + 1].transform.GetComponent<IndividualPlaneScript>().Focussed();

				if (planeType == 0)
				{
					if (StaticVariables.ww1PlanesStats[i] == false)
					{
						selectButton.ObjectLocked();
					}
					else
					{
						selectButton.ObjectOwned();
					}
				}
				else if (planeType == 1)
				{
					if (StaticVariables.ww2PlanesStats[i] == false)
					{
						selectButton.ObjectLocked();
					}
					else
					{
						selectButton.ObjectOwned();
					}
				}
			}
			else if(i < children.Length -1)
			{
				children[i + 1].transform.GetComponent<IndividualPlaneScript>().Unfocussed();
			}
		}
		
    }

	public void SelectButtonClick()
	{
		StaticVariables.focussedPlane = focusedItem;
	}

	public void AnimatorStateChanger(int planeIndex)
	{
		children[planeIndex + 1].GetComponent<Animator>().SetBool("Unlocking", true);
	}

	public void SetAnims()
	{
		if(planeType == 0)
		{
			for(int i = 1; i < children.Length; i++)
			{
				children[i].GetComponent<Animator>().SetBool("Locked", !StaticVariables.ww1PlanesStats[i - 1]);
			}
		}
		else if(planeType == 1)
		{
			for (int i = 1; i < children.Length; i++)
			{
				children[i].GetComponent<Animator>().SetBool("Locked", !StaticVariables.ww2PlanesStats[i - 1]);
			}
		}
	}

	public void Unlock(int index)
	{
		children[index + 1].GetComponent<IndividualPlaneScript>().unlocked = true;
	}
}
