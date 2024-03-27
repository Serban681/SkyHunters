using UnityEngine;
using System;
using UnityEngine.UI;

public class GiftButton : MonoBehaviour
{
	public GiftScript giftScript;

    //protected float msToWait = 1.08e+07;
	public float msToWait = 5000.0f;
	public Button button;
	public Text giftTimer;
	private ulong lastClick;

	public Transform giftButton;

	private void Start()
	{
        lastClick = ulong.Parse(ES3.Load<string>("LastGifted", "0"));//SaveGame.Load<string>("LastGifted"));

		if (!IsClickable())
		{
			button.interactable = false;
            giftTimer.GetComponent<RectTransform>().localPosition = new Vector3(0, 5, 0);
            //button.gameObject.transform.GetComponent<Image>().enabled = false;
            Image[] imgs = button.gameObject.transform.GetComponentsInChildren<Image>();

            foreach (Image img in imgs)
            {
                img.enabled = false;
            }
        }
	}

	private void Update()
	{
		if (!button.IsInteractable())
		{
			if (IsClickable())
			{
				button.interactable = true;
				return;
			}

			ulong diff = ((ulong)DateTime.Now.Ticks - lastClick);
			ulong m = diff / TimeSpan.TicksPerMillisecond;

			float secondsLeft = (float)(msToWait - m) / 1000.0f;

			string r = "";

			r += ((int)secondsLeft / 3600).ToString() + "h ";

			secondsLeft -= ((int)secondsLeft / 3600) * 3600;

			r += ((int)secondsLeft / 60).ToString("00") + "m ";

			r += (secondsLeft % 60).ToString("00") + "s";

			giftTimer.text = "Free Gift In " + r;
		}
	}

	public void OnClick()
	{
		giftButton.gameObject.SetActive(true);
		giftScript.SetSprites();
		giftTimer.GetComponent<RectTransform>().localPosition = new Vector3(0, 5, 0);
        //button.gameObject.transform.GetComponent<Image>().enabled = false;
        Image[] imgs = button.gameObject.transform.GetComponentsInChildren<Image>();

        foreach (Image img in imgs)
        {
            img.enabled = false;
        }

        lastClick = (ulong)DateTime.Now.Ticks;
        ES3.Save<string>("LastGifted", lastClick.ToString());
        //SaveGame.Save<string>("LastGifted", lastClick.ToString());
        button.interactable = false;
	}

	private bool IsClickable()
	{
		ulong diff = ((ulong)DateTime.Now.Ticks - lastClick);
		ulong m = diff / TimeSpan.TicksPerMillisecond;

		float secondsLeft = (float)(msToWait - m) / 1000.0f;

		if (secondsLeft < 0)
		{
			giftTimer.text = "Free Gift";
            giftTimer.GetComponent<RectTransform>().localPosition = new Vector3(-40, 5, 0);

            Image[] imgs = button.gameObject.transform.GetComponentsInChildren<Image>();

            foreach (Image img in imgs)
            {
                img.enabled = true;
            }

            return true;
		}
		else
			return false;
	}
}
