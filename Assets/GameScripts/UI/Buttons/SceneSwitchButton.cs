using UnityEngine;

public class SceneSwitchButton : MonoBehaviour
{
	public string sceneName;
	private Animator camAnim;
	public Canvas canvas;
    public bool tapToPlayButton = false;

    void Start()
    {
		camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
	}

	public void FadeToLevel()
	{
        if (tapToPlayButton == false)
            FindObjectOfType<AudioManagerScript>().Play("ClickSound");
        else
            FindObjectOfType<AudioManagerScript>().Play("GameStartSound");

		camAnim.transform.GetComponent<SceneChanger>().sceneName = sceneName;
		camAnim.SetBool("FadeGrayScaleIn", false);
		canvas.renderMode = RenderMode.ScreenSpaceCamera;
		camAnim.SetBool("PixelsOut", false);
	}
}
