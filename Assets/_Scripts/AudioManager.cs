using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource vox;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioClip bgmInitMusic;

    [SerializeField] float voxVol = 1.0f;
    [SerializeField] float sfxVol = 1.0f;
    [SerializeField] float bgmVol = 1.0f;

    public static AudioManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (!Instance)
            Instance = this;
        
        else
            Destroy(this.gameObject);


        vox = this.gameObject.AddComponent<AudioSource>();
        vox.playOnAwake = false;

        sfx = this.gameObject.AddComponent<AudioSource>();
        sfx.playOnAwake = false;

        bgm = this.gameObject.AddComponent<AudioSource>();
        PlayBGM(bgmInitMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayVox(AudioClip clip)
    {
        vox.PlayOneShot(clip);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.clip = clip;
        sfx.Play();
    }

    public void SetVoxVolume(float volume)
    {
        vox.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        sfx.volume = volume;
    }
    public void SetBGMVolume(float volume)
    {
        bgm.volume = volume;
    }
}
