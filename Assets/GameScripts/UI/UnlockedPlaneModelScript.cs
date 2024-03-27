using UnityEngine.UI;
using UnityEngine;

public class UnlockedPlaneModelScript : MonoBehaviour
{
	public Button continueButton;

    public Vector2[] ww1BladesPos;
    public Sprite[] ww1BladeSprites;

    public Vector2[] ww2BladePos;
    public Sprite[] ww2BladeSprites;

	private Animator animController;

    public Sprite[] ww1Sprites;
    public Sprite[] ww2Sprites;

    private SpriteRenderer spriteRend;

	public void TakeOff()
	{
		animController.SetTrigger("SecondPhase");
	}

    public void ChangeSprites(int planeType, int spriteIndex)
    {
        animController = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        if (planeType == 1)
        {
            spriteRend.sprite = ww1Sprites[spriteIndex];
            transform.Find("Blade").GetComponent<SpriteRenderer>().sprite = ww1BladeSprites[spriteIndex];
            transform.Find("Blade").GetComponent<Transform>().localPosition = new Vector3(ww1BladesPos[spriteIndex].x, ww1BladesPos[spriteIndex].y, 0);
        }
        else if (planeType == 2)
        {
            spriteRend.sprite = ww2Sprites[spriteIndex];
            transform.Find("Blade").GetComponent<SpriteRenderer>().sprite = ww2BladeSprites[spriteIndex];
            transform.Find("Blade").GetComponent<Transform>().localPosition = new Vector3(ww2BladePos[spriteIndex].x, ww2BladePos[spriteIndex].y, 0);
        }

		if ((spriteIndex == 3 && planeType == 1) || (spriteIndex == 3 && planeType == 2))
		{
			animController.SetLayerWeight(1, 0);
			animController.SetLayerWeight(2, 1);
		}
		else
		{
			animController.SetLayerWeight(1, 1);
			animController.SetLayerWeight(2, 0);
		}
    }

	public void FinishedFirstPhase()
	{
		continueButton.interactable = true;
	}

	public void FinishedSecondPhase()
	{
		FindObjectOfType<Camera>().transform.GetComponent<Animator>().SetBool("PixelsOut", false);
		FindObjectOfType<SceneChanger>().sceneName = "PlaneSelectScene";
	}
}
