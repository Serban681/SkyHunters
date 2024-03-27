using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonScript : MonoBehaviour
{
	private Animator anim;
	private Image img;
	private Text text;
    public UnlockedPlaneModelScript unlockedPlane;
    public Sprite[] ww1Sprites;
    public Sprite[] ww2Sprites;

	public string[] ww1PlaneNames;
	public string[] ww2PlaneNames;

	// Start is called before the first frame update
	void Awake()
    {
		anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
		anim.SetBool("FadeIn", true);
    }

	public void OnClick()
	{
        anim.SetBool("FadeOut", true);
        anim.SetBool("FadeIn", false);
        unlockedPlane.TakeOff();
        anim.SetBool("SetNormal", true);
        gameObject.SetActive(false);
	}

    private void OnDisable()
    {
        anim.SetBool("FadeOut", false);
        anim.SetBool("SetNormal", false);
    }

    public void setImg(int planeType, int spriteIndex)
    {
        img = transform.Find("PlaneImage").transform.GetComponent<Image>();
		text = transform.Find("PlaneName").transform.GetComponent<Text>();

        if (planeType == 1)
        {
            img.sprite = ww1Sprites[spriteIndex];
			text.text = ww1PlaneNames[spriteIndex];
        }
        else if (planeType == 2)
        {
            img.sprite = ww2Sprites[spriteIndex];
			text.text = ww2PlaneNames[spriteIndex];
		}

        img.SetNativeSize();
    }
}
