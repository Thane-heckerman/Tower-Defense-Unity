using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private float volume = .5f;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }
    public void SoundIncrease()
    {
        volume += .1f;
        Mathf.Clamp(volume, 0, 1);
        audioSource.volume = volume;
        Debug.Log("muisc" + volume);
    }

    public void SoundDecrease()
    {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }
}
