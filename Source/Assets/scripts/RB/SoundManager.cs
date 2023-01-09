using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SMInstance;
    [SerializeField] AudioSource musicsrc, sfxsrc;

    private void Awake()
    {
        if(SMInstance == null)
        {
            SMInstance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        sfxsrc.PlayOneShot(clip);
    }

    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
    }

}
