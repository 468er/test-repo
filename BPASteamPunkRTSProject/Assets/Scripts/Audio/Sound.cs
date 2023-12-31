using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip Clip;

    public float clip_length;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 30f)]

    public float pitch;

    public bool loop;
    [HideInInspector]
    public AudioSource source;

    [Range(1f, 3f)]
    public int IFSFXis1_2ifOOGA;
    public int position;

}
