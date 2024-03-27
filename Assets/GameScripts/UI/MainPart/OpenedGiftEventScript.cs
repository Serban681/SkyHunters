using UnityEngine;

public class OpenedGiftEventScript : MonoBehaviour
{
    public Animator coinAmountAnim;

    public void GiftOpened()
    {
        coinAmountAnim.SetBool("Show", true);
    }
}
