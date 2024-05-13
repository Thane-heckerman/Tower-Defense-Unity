using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class EnemyWaveManager : MonoBehaviour
{

    public static EnemyWaveManager Instance { get; private set; }
    public event EventHandler OnWaveNumberChanged;
    private enum State {
        WaitingForSpawnNextWave,
        SpawningWave,
    }
    private State state;
    [SerializeField] private List<Transform> enemySpawnList;
    [SerializeField] private Transform nextEnemySpawnPos;
    private float waveSpawnTimer;
    private int enemyRemaining;
    private int waveNumber = 0;

    private float nextEnemySpawner = .2f;
    private Vector3 spawnPos = new Vector3(100, 0);

    // test function trước khi mở rộng
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        state = State.WaitingForSpawnNextWave;
        waveSpawnTimer = 3f;
        spawnPos = enemySpawnList[UnityEngine.Random.Range(0, enemySpawnList.Count)].position;
        nextEnemySpawnPos.position = spawnPos;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingForSpawnNextWave:
                waveSpawnTimer -= Time.deltaTime;
                if (waveSpawnTimer < 0f)
                {
                    SpawnWave();
                }
                break;

            case State.SpawningWave:

                if (enemyRemaining > 0f)
                {
                    nextEnemySpawner -= Time.deltaTime;
                    if (nextEnemySpawner <0f)
                    {
                        nextEnemySpawner = UnityEngine.Random.Range(0, .2f);
                        Enemy.CreateEnemy(spawnPos + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0, 10f));
                        enemyRemaining--;
                        if (enemyRemaining <= 0)
                        {
                            state = State.WaitingForSpawnNextWave;
                            spawnPos = enemySpawnList[UnityEngine.Random.Range(0, enemySpawnList.Count)].position;
                            nextEnemySpawnPos.position = spawnPos;
                            waveSpawnTimer = 10f;

                        }
                    }
                }
                break;

        }
        
    }
    private void SpawnWave()
    {
        enemyRemaining = 5 + 3*waveNumber;
        state = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetTimeToSpawnNextWave()
    {
        return waveSpawnTimer;
    }

    public Transform GetSpawnPosition()
    {
        return nextEnemySpawnPos;
    }
}
