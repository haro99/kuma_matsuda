using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundData 
{
    public float SoundVolume;
    public float BgmVolume;

    public SoundData()
    {
        this.SoundVolume = 1f;
        this.BgmVolume = 0.3f;
    }

    public void PlaySound(AudioSource source)
    {
        source.volume = this.SoundVolume;
        source.Play();
    }

    public void PlayBGM(AudioSource source)
    {
        source.volume = this.BgmVolume;
        source.Play();
    }

}
