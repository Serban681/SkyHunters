using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftScript : MonoBehaviour
{
	public Gift[] gifts;

    public Transform giftUnopened;
    public Transform giftOpened;
	public Animator coinAmountAnim;
	public Transform coinSystem;
	public Text amountReceivedText;

	private int coinAmountReceived;
	private int timesClicked = 0;

	private Animator openedGiftAnim;
	private Animator unopenedGiftAnim;
	public Animator glitterAnim;

	public Image unopenedGiftRend;
	public Image openedBoxRend;
	public Image openedTopRend;
	private Image buttonImg;

	private void Awake()
	{
		openedGiftAnim = giftOpened.GetComponent<Animator>();
		unopenedGiftAnim = giftUnopened.GetComponent<Animator>();
		buttonImg = GetComponent<Image>();
	}

	public void OnClick()
    {
		timesClicked++;

		if (timesClicked == 1)
		{
			coinAmountReceived = Random.Range(86, 121);
			amountReceivedText.text = coinAmountReceived.ToString();
			transform.GetComponent<Button>().interactable = false;
			unopenedGiftAnim.SetBool("Open", true);

			StaticVariables.coins += coinAmountReceived;
		}
		else if(timesClicked == 2)
		{
			glitterAnim.SetBool("Show", false);
			transform.GetComponent<Animator>().SetBool("FadeIn", false);
			//openedGiftAnim.SetBool("FadeOut", true);
			coinAmountAnim.SetBool("FadeOut", true);

            FindObjectOfType<CoinSystemScript>().AddCoins();
        }
    }

	public void FadedOut()
	{
		coinSystem.transform.GetComponent<Animator>().SetBool("Changed", true);
		Reset();
		transform.gameObject.SetActive(false);
	}

	public void FadeOutComplete()
	{
		transform.gameObject.SetActive(false);
	}

	private void Reset()
	{
		timesClicked = 0;
		giftOpened.gameObject.SetActive(false);
		giftUnopened.gameObject.SetActive(true);
		unopenedGiftAnim.SetBool("Open", false);
		coinAmountAnim.SetBool("Show", false);
	}
	
	public void SetSprites()
	{
		int index = Random.Range(3, unopenedGiftAnim.layerCount);

		for(int i = 3; i < unopenedGiftAnim.layerCount; i++)
		{
			unopenedGiftAnim.SetLayerWeight(i, 0);
		}

		unopenedGiftAnim.SetLayerWeight(index, 1);

		//unopenedGiftRend.sprite = gifts[index].giftSprite;
		/*
		openedBoxRend.sprite = gifts[index].openedBox;
		openedTopRend.sprite = gifts[index].openedTop;
		*/
		buttonImg.color = gifts[index - 3].buttonColor;
	}
}
