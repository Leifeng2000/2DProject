using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("LeiFeng/AudioManager")]

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get => instance;
    }
    private static AudioManager instance;
    // Start is called before the first frame update
    #region public
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip backgroundMusic;
    #endregion

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        PlayBackGroundMusic(backgroundMusic);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void PlayBackGroundMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
