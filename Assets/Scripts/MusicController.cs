using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource bg;
    public AudioSource fx;
    public static MusicController instance;
    public float mastervolume = 1.0f;
    public float bgvolume = 1.0f;
    public float fxvolume = 1.0f;
    void Start() 
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        mastervolume = .5f;
        UpdateAudioSources();
    }
    public void UpdateAudioSources()
    {
        instance.bg = GameObject.Find("BGSource").GetComponent<AudioSource>();
        instance.fx = GameObject.Find("FXSource").GetComponent<AudioSource>();

        //Set the volumes from the static instance
        bg.volume = mastervolume * bgvolume;
        fx.volume = mastervolume * fxvolume;
    }
    public void ChangeMasterVolume(float vol)
    {
        instance.mastervolume = vol/10;
        bg.volume = mastervolume * bgvolume;
        fx.volume = mastervolume * fxvolume;

    }
    public void ChangeBGVolume(float vol)
    {
        instance.bgvolume = vol/10;
        bg.volume = mastervolume * bgvolume;
    }
    public void ChangeFXVolume(float vol)
    {
        instance.fxvolume = vol/10;
        fx.volume = mastervolume * fxvolume;
        PlaySFX("Slice");
    }
    public void PlaySFX(string audioFile)
    {
        fx.clip = Resources.Load<AudioClip>("SFX/" + audioFile);
        fx.Play();
    }
}