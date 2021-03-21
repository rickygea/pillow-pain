using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum SoundType
{
    SFX,
    BGM,
}

[System.Serializable]
public class AudioData
{
    public string Name = "";
    public AudioClip SoundClip = null;
    public SoundType SoundTypeEnum = SoundType.SFX;
    [Range(0, 1)]
    public float Volume = 1;

    public AudioData()
    { }

    public AudioData(string name = "", AudioClip clip = null, SoundType soundTypeEnum = SoundType.SFX, float vol = 1)
    {
        Name = name;
        SoundClip = clip;
        SoundTypeEnum = soundTypeEnum;
        Volume = vol;
    }
    public AudioData(AudioClip clip = null, SoundType soundTypeEnum = SoundType.SFX, float vol = 1)
    {
        SoundClip = clip;
        SoundTypeEnum = soundTypeEnum;
        Volume = vol;
    }

#if UNITY_EDITOR
    public void OnGui(int index = 0, string text = "", SerializedProperty serialProp = null)
    {
        Name = EditorGUILayout.TextField("Name", Name);
        SoundClip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip", SoundClip, typeof(AudioClip), false);
        SoundTypeEnum = (SoundType)EditorGUILayout.EnumPopup("Sound Type", SoundTypeEnum);
        Volume = EditorGUILayout.Slider("Volume", Volume, 0, 1);
    }
#endif
}
