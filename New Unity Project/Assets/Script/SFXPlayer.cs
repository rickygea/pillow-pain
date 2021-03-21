using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _sfxList = new AudioClip[0];
    private AudioSource _audioSource = null;
    public AudioSource AudioSource
    {
        get
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();

            return _audioSource;
        }
    }

    public void PlaySFX(int index)
    {
        AudioSource.PlayOneShot(_sfxList[index]);
    }
}
