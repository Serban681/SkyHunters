using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialAssetsScript : MonoBehaviour
{
	public GameObject[] movementAssets;
	public GameObject continueButton;
	public FancyShootButtonScript shooterScript;

	private int timesClicked = 0;

	private bool hasSeenTutorial;

    // Start is called before the first frame update
    void Start()
    {
		hasSeenTutorial = ES3.Load("hasSeenTutorial", false);

		if(hasSeenTutorial == false)
		{
			StartCoroutine(ShowTutorial());
		}
    }

    // Update is called once per frame
    public void OnClick()
    {
		if (timesClicked == 0)
		{
			TurnGameObjects(movementAssets, false);
			continueButton.SetActive(false);
			shooterScript.enabled = true;
			TimeManagerScript.instance.TogglePause();
			ES3.Save("hasSeenTutorial", true);
		}

		timesClicked++;
    }

	private void TurnGameObjects(GameObject[] gos, bool status)
	{
		foreach(GameObject go in gos)
		{
			go.SetActive(status);
		}
	}

	private IEnumerator ShowTutorial()
	{
		shooterScript.enabled = false;
		continueButton.SetActive(true);
		continueButton.GetComponent<Button>().interactable = false;

		yield return new WaitForSeconds(1);

		continueButton.GetComponent<Button>().interactable = true;
		TurnGameObjects(movementAssets, true);
		TimeManagerScript.instance.TogglePause();
	}
}
