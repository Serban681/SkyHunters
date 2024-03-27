using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsEndButton : MonoBehaviour, IUnityAdsListener
{
	private Text text;
	public Animator coinSystemAnim;

    string gameId;
    string myPlacementId = "rewardedVideo";
    private bool testMode = true;

    // Initialize the Ads listener and service:
    void Start()
    {
#if UNITY_ANDROID

        gameId = "3566767";

#endif

#if UNITY_IOS

        gameId = "3566766";

#endif

		text = transform.parent.Find("Text").transform.GetComponent<Text>();

		InitializeAdvertisment();
    }

    private void InitializeAdvertisment()
	{
		Advertisement.AddListener(this);
		Advertisement.Initialize(gameId, testMode);
	}

	/*
	public void PlayInterstitialAd()
	{
		if (!Advertisement.IsReady(myPlacementId))
		{
			return;
		}

		Advertisement.Show(myPlacementId);
	}
	*/

	public void PlayRewardedVideoAd()
	{
		if (!Advertisement.IsReady(myPlacementId))
		{
			return;
		}

		Advertisement.Show(myPlacementId);
	}

	public void OnUnityAdsReady(string placementId)
	{
		//throw new System.NotImplementedException();
	}

	public void OnUnityAdsDidError(string message)
	{
		//throw new System.NotImplementedException();

		text.transform.localPosition = new Vector3(0, text.transform.localPosition.y, text.transform.localPosition.z);
		text.text = "Error";
		transform.gameObject.SetActive(false);
	}

	public void OnUnityAdsDidStart(string placementId)
	{
		//throw new System.NotImplementedException();

		// Stop the music in game
	}

	public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
	{
		switch (showResult)
		{
			
			case ShowResult.Failed:
				if (text != null)
				{
					text.transform.localPosition = new Vector3(0, text.transform.localPosition.y, text.transform.localPosition.z);
					text.text = "5 Coins";
					StaticVariables.coins += 5;
					ES3.Save<int>("coins", StaticVariables.coins);
					coinSystemAnim.SetBool("Changed", true);
                    FindObjectOfType<CoinSystemScript>().AddCoins();
                    transform.gameObject.SetActive(false);
				}
			break;

			case ShowResult.Skipped:
				break;

			case ShowResult.Finished:
				if(placementId == myPlacementId)
				{
					int x = Random.Range(25, 35);
					if (text != null)
					{
						text.transform.localPosition = new Vector3(0, text.transform.localPosition.y, text.transform.localPosition.z);
						text.text = x.ToString() + " Coins";
						StaticVariables.coins += x;
						coinSystemAnim.SetBool("Changed", true);
						ES3.Save<int>("coins", StaticVariables.coins);
                        FindObjectOfType<CoinSystemScript>().AddCoins();
						transform.gameObject.SetActive(false);
					}
				}
				break;
		}
	}
}