using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField]
    private AudioSource audioSource;

    public float sfxVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }
    public bool sfxMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    [SerializeField]
    private AudioSource music1Source;
    [SerializeField]
    private string introBackgroundMusic;
    [SerializeField]
    private string levelBackgroundMusic;

    private float _musicVolume;
    public float musicVolume
    {
        get
        {
            return _musicVolume;
        }
        set
        {
            _musicVolume = value;
            if (music1Source != null)
            {
                music1Source.volume = _musicVolume;
            }
        }
    }
    public bool musicMute
    {
        get
        {
            if (music1Source != null)
            {
                return music1Source.mute;
            }
            return false;
        }
        set
        {
            if (music1Source != null)
            {
                music1Source.mute = value;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            musicMute = !musicMute;
            sfxMute = !sfxMute;
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            musicVolume -= 0.1f;
            sfxVolume -= 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.Period))
        {
            if (musicVolume < 1.0f && sfxVolume < 1.0f)
            {
                musicVolume += 0.1f;
                sfxVolume += 0.1f;
            }
        }
    }

    public void Initialize()
    {
        status = ManagerStatus.Initializing;

        music1Source.ignoreListenerVolume = true;
        music1Source.ignoreListenerPause = true;

        sfxVolume = 1.0f;
        musicVolume = 1.0f;

        PlayLevelMusic();

        status = ManagerStatus.Ready;
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayIntroMusic()
    {
        PlayMusic(Resources.Load("Music/" + introBackgroundMusic) as AudioClip);
    }

    public void PlayLevelMusic()
    {
        PlayMusic(Resources.Load("Music/" + levelBackgroundMusic) as AudioClip);
    }

    private void PlayMusic(AudioClip clip)
    {
        music1Source.clip = clip;
        music1Source.Play();
    }

    public void StopMusic()
    {
        music1Source.Stop();
    }
}
