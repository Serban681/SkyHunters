using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SoundButtonToggle : MonoBehaviour
{
    private Image img;
    public Sprite turnedOnImg;
    public Sprite turnedOffImg;

    private bool turnedOn = true;

    public string fileName;

    private void Start()
    {
        turnedOn = ES3.Load<bool>(fileName, true);
        img = transform.Find("Img").transform.GetComponent<Image>();

        if (fileName == "MusicOn")
            StaticVariables.musicOn = turnedOn;
        if (fileName == "SFXOn")
            StaticVariables.sfxOn = turnedOn;

        if (turnedOn == true)
        {
            img.sprite = turnedOnImg;
        }
        else
        {
            img.sprite = turnedOffImg;
        }

        FindObjectOfType<AudioManagerScript>().OnSettingChanged();
    }

    public void OnClick()
    {
        turnedOn = !turnedOn;

        if(turnedOn == true)
        {
            img.sprite = turnedOnImg;
        }
        else
        {
            img.sprite = turnedOffImg;
        }

        if (fileName == "MusicOn")
            StaticVariables.musicOn = turnedOn;
        if (fileName == "SFXOn")
            StaticVariables.sfxOn = turnedOn;

        FindObjectOfType<AudioManagerScript>().OnSettingChanged();

        ES3.Save<bool>(fileName, turnedOn);
    }
}
