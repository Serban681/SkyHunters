using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarBackButtonScript : MonoBehaviour
{
    public Animator camAnim;
    private Canvas UI;
    public string sceneName;

    private void Start()
    {
        UI = transform.parent.transform.GetComponent<Canvas>();
    }

    public void OnClick()
    {
        UI.renderMode = RenderMode.ScreenSpaceCamera;
        camAnim.SetBool("PixelsOut", false);
        FindObjectOfType<SceneChanger>().sceneName = sceneName;
    }
}
