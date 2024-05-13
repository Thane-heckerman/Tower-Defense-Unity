using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSceneUI : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("PlayBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameScenceManager.Load(GameScenceManager.Scene.GameScene);
        });
        transform.Find("QuitBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

}
