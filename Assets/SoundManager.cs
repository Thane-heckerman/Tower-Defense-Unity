using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource audioSource;
    private float volume = .5f;
    public enum Sound
    {
        EnemyHit,
        EnemyDie,
        BuildingDamaged,
        BuildingDestroyed,
        BuildingPlaced,
        EnemyWaveStarting,
        GameOver,
    }
    private Sound sound;
    private Dictionary<Sound, AudioClip> soundDictionary;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        soundDictionary = new Dictionary<Sound, AudioClip>();
        foreach(Sound sound in System.Enum.GetValues(typeof(Sound))){
            soundDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound, float volume)
    {
        audioSource.PlayOneShot(soundDictionary[sound],volume);
    }

    public void SoundIncrease()
    {
        volume += .1f;
        Mathf.Clamp(volume, 0, 1);
    }

    public void SoundDecrease()
    {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
    }

    public float GetVolume()
    {
        return volume;
    }
}