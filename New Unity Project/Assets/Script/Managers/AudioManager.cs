using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public enum AudioVolumeType
{
    Master,
    SFX,
    BGM
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private List<AudioData> _listAudioData = new List<AudioData>();
    public List<AudioData> ListAudioData
    {
        get { return _listAudioData; }
    }

    [Space]
    [SerializeField] private AudioSource _audioSourceSFX = null;
    public AudioSource AudioSourceSFX
    {
        get { return _audioSourceSFX; }
    }
    [SerializeField] private AudioSource _audioSourceBGM = null;
    public AudioSource AudioSourceBGM
    {
        get { return _audioSourceBGM; }
    }

    //Audio Mixer
    [SerializeField] private AudioMixer _audioMixer = null;
    public AudioMixer AudioMixer
    {
        get { return _audioMixer; }
    }
    private const string MASTER_VOLUME = "MasterVol";
    private const float MAX_MASTER_VOLUME = 80f;
    private const string SFX_VOLUME = "SFXVol";
    private const float MAX_SFX_VOLUME = 80f;
    private const string BGM_VOLUME = "BGMVol";
    private const float MAX_BGM_VOLUME = 60f;
    private const float BGM_TRANSITION_DELAY = 0f; //0 = Instant Transition

    private bool _isSFXOn = true;
    /// <summary>
    /// True maka sfx nyala, false maka sfx mati
    /// </summary>
    public bool IsSFXOn
    {
        get { return _isSFXOn; }
        set
        {
            _isSFXOn = value;
            MuteUnMuteSound(AudioVolumeType.SFX);
        }
    }
    private bool _isBGMOn = true;
    /// <summary>
    /// True maka bgm nyala, false maka bgm mati
    /// </summary>
    public bool IsBGMOn
    {
        get { return _isBGMOn; }
        set
        {
            _isBGMOn = value;
            MuteUnMuteSound(AudioVolumeType.BGM);
        }
    }
    private bool _isMasterOn = true;
    /// <summary>
    /// True maka master nyala, false maka master berhenti
    /// </summary>
    public bool IsMasterOn
    {
        get { return _isMasterOn; }
        set
        {
            _isMasterOn = value;
            MuteUnMuteSound(AudioVolumeType.Master);
        }
    }

    private LTDescr _ltBGMCache = null;

    /// <summary>
    /// Play's the audio with the name that is set on ListAudioData. To know if the Audio is BGM or SFX, just look it up in AudioManager Prefab at ListAudioData.
    /// </summary>
    /// <param name="name">The name of the audio</param>
    public void PlayAudio(string name)
    {
        AudioData audioDat = ListAudioData.Find(x => x.Name == name);
        if (audioDat == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Can't find AudioData with name {name}");
#endif
            return;
        }

        PlayAudio(audioDat);
    }
    public void PlayAudio(AudioClip clip, SoundType soundTypeEnum = SoundType.SFX, float vol = 1)
    {
        AudioData audioDat = new AudioData(clip, soundTypeEnum, vol);
        PlayAudio(audioDat);
    }
    public void PlayAudio(AudioData audioData)
    {
        if (audioData.SoundTypeEnum == SoundType.BGM)
            PlayBGM(audioData);
        else if (audioData.SoundTypeEnum == SoundType.SFX)
            PlaySFX(audioData);
    }

    private void PlaySFX(AudioData audioDat)
    {
        if(audioDat.SoundClip == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Audio clip with name {audioDat.Name} is null!");
#endif
            return;
        }

        AudioSourceSFX.PlayOneShot(audioDat.SoundClip, audioDat.Volume);
    }
    private void PlayBGM(AudioData audioDat)
    {
        if(audioDat.SoundClip == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Audio clip with name {audioDat.Name} is null!");
#endif
            return;
        }

        if(AudioSourceBGM.isPlaying)
        {
            if(AudioSourceBGM.clip != audioDat.SoundClip)
            {
                AudioMixer.GetFloat(BGM_VOLUME, out float vol);
                _ltBGMCache = LeanTween.value(gameObject, (float val) =>
                {
                    AudioMixer.SetFloat(BGM_VOLUME, val);
                }, vol, -80, BGM_TRANSITION_DELAY).setOnComplete(() => 
                {
                    if (!IsBGMOn)
                        vol = -80;

                    AudioMixer.SetFloat(BGM_VOLUME, -80f); //-80 disini itu volume = 0 di Audio Mixer

                    AudioSourceBGM.Stop();
                    AudioSourceBGM.volume = audioDat.Volume;
                    AudioSourceBGM.clip = audioDat.SoundClip;
                    AudioSourceBGM.Play();

                    _ltBGMCache = LeanTween.value(gameObject, (float val) => {
                        AudioMixer.SetFloat(BGM_VOLUME, val);
                    }, -80f, vol, BGM_TRANSITION_DELAY).setOnComplete(()=> {
                        AudioMixer.SetFloat(BGM_VOLUME, vol);
                    });
                });
            }
        }
        else
        {
            AudioSourceBGM.volume = audioDat.Volume;
            AudioSourceBGM.clip = audioDat.SoundClip;
            AudioSourceBGM.Play();
        }
    }
    public void PauseBGM()
    {
        AudioSourceBGM.Pause();
    }
    public void ResumeBGM()
    {
        AudioSourceBGM.UnPause();
    }

    /// <summary>
    /// Changes the Audio Volume in the Audio Mixer.
    /// </summary>
    /// <param name="audioVolType">The type of AudioSource that the volume wanted to changes</param>
    /// <param name="volume">Between 0 - 1</param>
    public void SetVolume(AudioVolumeType audioVolType, float volume)
    {
        string audioType = null;
        float maxVolume = 80f;
        switch (audioVolType)
        {
            case AudioVolumeType.Master:
                audioType = MASTER_VOLUME;
                maxVolume = MAX_MASTER_VOLUME;
                break;
            case AudioVolumeType.BGM:
                audioType = BGM_VOLUME;
                maxVolume = MAX_BGM_VOLUME;
                break;
            case AudioVolumeType.SFX:
                audioType = SFX_VOLUME;
                maxVolume = MAX_SFX_VOLUME;
                break;
        }

        float volAdjusted = Mathf.Clamp01(volume);
        volAdjusted = ((volAdjusted * maxVolume) - 80); //karena min di audio mixer -80 audionya mati, 0 audionya normal, dan 20 audionya boosted (itungan desibles)

        if (audioType != null && AudioMixer != null)
            AudioMixer.SetFloat(audioType, volAdjusted);
    }
    /// <summary>
    /// Mutes or Unmutes the sound, depending on bool Is__On. Don't have to call this, just set the Is__On variable.
    /// </summary>
    /// <param name="audioType">The type of AudioSource that the volume wanted to changes</param>
    public void MuteUnMuteSound(AudioVolumeType audioType)
    {
        if (audioType == AudioVolumeType.SFX)
        {
            SetVolume(AudioVolumeType.SFX, IsSFXOn == true ? 1 : 0);
        }
        else if (audioType == AudioVolumeType.BGM)
        {
            SetVolume(AudioVolumeType.BGM, IsBGMOn == true ? 1 : 0);
        }
        else if (audioType == AudioVolumeType.Master)
        {
            SetVolume(AudioVolumeType.Master, IsMasterOn == true ? 1 : 0);
        }
    }

}