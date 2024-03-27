using UnityEngine;

public class SettingsBackButton : MonoBehaviour
{
    private Animator settingsPageAnim;

    void Start()
    {
        settingsPageAnim = transform.parent.GetComponent<Animator>();
    }

    public void OnClick()
    {
        settingsPageAnim.SetBool("Open", false);
    }
}
