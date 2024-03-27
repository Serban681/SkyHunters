using System;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManagerScript instance;

    // Start is called before the first frame update
    void Awake()
    {
		/*
        if(ReferenceEquals(instance, null))
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
		*/
		instance = this;

        //DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.aux = s.volume;
        }
    }

    public void Play (string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

	public void Play(string name, float pitchM, float volumeM)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.pitch *= pitchM;
		s.source.volume *= volumeM;

		s.source.Play();

		s.source.pitch /= pitchM;
		s.source.volume /= volumeM;
	}

    public void OnSettingChanged()
    {
        foreach(Sound s in sounds)
        {
            if (s.name == "TitleTheme" || s.name == "GameplayTheme")
            {
                if (StaticVariables.musicOn == true)
                    s.source.volume = s.aux;
                else
                    s.source.volume = 0;
            }
            else if(s.name != "TitleTheme" || s.name != "GameplayTheme")
            {
                if (StaticVariables.sfxOn == true)
                    s.source.volume = s.aux;
                else
                    s.source.volume = 0;
            }
        }
    }
}
