using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    public static GameOverUIManager Instance { get; private set; }

    private void Awake()
    {
        transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameScenceManager.Load(GameScenceManager.Scene.GameScene);
        });

        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameScenceManager.Load(GameScenceManager.Scene.MainMenuScene);
        });
        Instance = this;
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        transform.Find("youSurvivedText").GetComponent<TextMeshProUGUI>().
            SetText("You Survived " + EnemyWaveManager.Instance.GetWaveNumber() + "waves");
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
