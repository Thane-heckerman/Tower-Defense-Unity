using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUIManager : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI waveNumber;
    private TextMeshProUGUI waveSecond;
    private RectTransform NextWaveSpawnPosIndicator;

    private Camera mainCamera;
    private void Awake()
    {
        waveNumber = transform.Find("waveNumber").GetComponent<TextMeshProUGUI>();

        waveSecond = transform.Find("waveSecond").GetComponent<TextMeshProUGUI>();

        NextWaveSpawnPosIndicator = transform.Find("NextWaveSpawnPosIndicator").GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumber("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        float waveTimer = enemyWaveManager.GetTimeToSpawnNextWave();
        if (waveTimer <= 0f)
        {
            waveSecond.SetText("");
        }
        else
        {
            waveSecond.SetText(waveTimer.ToString("F1") + " second(s) remaining to next wave");
        }

        if (enemyWaveManager.GetSpawnPosition() != null)
        {
            Vector3 dirToNextEnemySpawnPos = (enemyWaveManager.GetSpawnPosition().transform.position - mainCamera.transform.position).normalized;
            NextWaveSpawnPosIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetRotation(dirToNextEnemySpawnPos));

        }
        else
        {
            NextWaveSpawnPosIndicator.gameObject.SetActive(false);

        }
    }

    private void SetWaveNumber(string message)
    {
        waveNumber.SetText(message);
    }
}
