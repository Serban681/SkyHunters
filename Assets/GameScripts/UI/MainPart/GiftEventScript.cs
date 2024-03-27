using UnityEngine;
using UnityEngine.UI;

public class GiftEventScript : MonoBehaviour
{
    public Transform giftOpened;
    public Transform giftUnopened;
	public Animator flashAnim;
	public Animator coinAmountAnim;
	public Animator glitterAnim;

    public void ChangeSprites()
    {
		glitterAnim.SetBool("Show", true);
		coinAmountAnim.SetBool("Show", true);
        transform.parent.transform.GetComponent<Button>().interactable = true;
        giftOpened.gameObject.SetActive(true);
        giftUnopened.gameObject.SetActive(false);
    }

	public void Flash()
	{
		flashAnim.SetTrigger("Flash");
	}
}
