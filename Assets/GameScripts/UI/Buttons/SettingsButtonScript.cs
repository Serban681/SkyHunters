using UnityEngine;

public class SettingsButtonScript : MonoBehaviour
{
    public Animator anim;
	/*
    private void Start()
    {
        anim = transform.parent.Find("SettingsBackGround").transform.GetComponent<Animator>();
    }
	*/
    public void OnClick()
    {
        anim.SetBool("Open", true);
    }
}
