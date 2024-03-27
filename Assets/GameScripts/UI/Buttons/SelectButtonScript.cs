using UnityEngine;
using UnityEngine.UI;
using MalbersAnimations.Selector;

public class SelectButtonScript : MonoBehaviour
{
    public Animator camAnim;
    private Canvas UI;

    public GameObject playImg;
    public GameObject priceText;

    private Button button;

	public SelectorController items;
	public SelectorController planeTypeSelector;

    [HideInInspector]
    public bool objectLocked = false;

    private delegate void _ClickType();
	_ClickType ClickType;

    private void Start()
    {
        UI = transform.parent.parent.transform.GetComponent<Canvas>();
        button = GetComponent<Button>();
    }

    public void OnClick()
    {
		ClickType();
    }

    public void ObjectOwned()
    {
        playImg.SetActive(true);
        priceText.SetActive(false);

		ClickType = OnUnlockedClick;
		button.interactable = true;
	}

    public void ObjectLocked()
    {
        playImg.SetActive(false);
        priceText.SetActive(true);

		//ClickType = OnLockedClick;
        button.interactable = false;

		//IAPManagerScript.instance.BuyPlane(planeTypeSelector.focusedItemIndex, items.focusedItemIndex);
    }

	private void OnLockedClick()
	{
		IAPManagerScript.instance.BuyPlane(planeTypeSelector.focusedItemIndex, items.focusedItemIndex);

		items.transform.GetComponent<ItemsControl>().AnimatorStateChanger(items.focusedItemIndex);
		items.transform.GetComponent<ItemsControl>().Unlock(items.focusedItemIndex);
	}

	private void OnUnlockedClick()
	{	
		UI.renderMode = RenderMode.ScreenSpaceCamera;
		camAnim.SetBool("PixelsOut", false);
	}
}
