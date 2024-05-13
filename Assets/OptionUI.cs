using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MusicManager musicManager;
    private TextMeshProUGUI soundText;
    private TextMeshProUGUI musicText;

    private void Awake()
    {
        soundText = transform.Find("SoundText").GetComponent<TextMeshProUGUI>();
        musicText = transform.Find("MusicText").GetComponent<TextMeshProUGUI>();
        transform.Find("SoundIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        { soundManager.SoundIncrease();
            UpdateSoundText();
        });
        transform.Find("SoundDecreaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            soundManager.SoundDecrease();
            UpdateSoundText();
        });
        transform.Find("MusicIncreaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            musicManager.SoundIncrease();
            UpdateMusicText();
        });
        transform.Find("MusicIncreaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            musicManager.SoundDecrease();
            UpdateMusicText();
        });

        transform.Find("MainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameScenceManager.Load(GameScenceManager.Scene.MainMenuScene);
        });
    }
    private void Start()
    {
        UpdateMusicText();
        UpdateSoundText();
        gameObject.SetActive(false);
    }

    private void UpdateSoundText()
    {
        soundText.SetText(Mathf.RoundToInt(soundManager.GetVolume()).ToString());
    }

    private void UpdateMusicText()
    {
        musicText.SetText(Mathf.RoundToInt(musicManager.GetVolume()).ToString());
    }

    public void ToggleVisible()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
