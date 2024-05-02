using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance { get { return instance; } }





    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private AudioSource soundMusic;
    [SerializeField] private bool IsMute = false;
    [SerializeField] private float Volume = 1f;
    [SerializeField] private SoundType[] sounds;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetVolume(0.3f);
        PlayMusic(SoundManager.Sounds.Music);    
    }

    public void Mute(bool status)
    {
        IsMute = status;
    }

    public void SetVolume(float volume)
    {
        soundMusic.volume = volume;
    }

    public void PlayMusic(Sounds sound)
    {
        if (IsMute)
            return;

        AudioClip clip = getSoundClip(sound);
        if(clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Play();
        }
        else
        {
            Debug.LogError("Sound Clip Not Found "+sound); 
        }
    }

    public void Play(Sounds sound)
    {
        if (IsMute)
            return;

        AudioClip clip = getSoundClip(sound);
        if(clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Sound Clip Not Found "+sound); 
        }
    }

    private AudioClip getSoundClip(Sounds sound)
    {
        SoundType returnSound =  Array.Find(sounds, item => item.soundType == sound);
        return returnSound.soundClip;
    }

    [Serializable] 
    public class SoundType
    {
        public Sounds soundType;
        public AudioClip soundClip;
    }



    public enum Sounds
    {
        onclick,
        Music,
        SnakeDeath,
        Massgainer,
        MassBurner,
        SpeedPowerUps,
        PowerUps,       
        
    }








}
