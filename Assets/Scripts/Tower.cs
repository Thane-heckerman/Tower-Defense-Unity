using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private Vector3 shootPosition;

    private float lookForTargetTimer;

    private float lookForTargetTimerMax = .2f;

    private float shootTimer;

    [SerializeField] private float shootTimerMax = .5f;

    private Enemy enemyTarget;

    private void Awake()
    {
        lookForTargetTimer = lookForTargetTimerMax;
        shootPosition = transform.Find("shootPosition").position;
    }

    private void Update()
    {
        HandleEnemyFinder();

        HandleShooting();
    }

    private void HandleEnemyFinder()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            EnemyFinder();
        }
    }
    private void EnemyFinder()
    {
        float findRadius = 50f;
        Collider2D[] collider2dArray = Physics2D.OverlapCircleAll(transform.position, findRadius);
        foreach ( Collider2D collider in collider2dArray)
        {
            Enemy enemy = collider.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (enemyTarget == null)
                {
                    enemyTarget = enemy;

                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, enemyTarget.transform.position))
                    {
                        enemyTarget = enemy;
                    }
                }
            }
        }
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0f)
        {
            shootTimer += shootTimerMax;
            if (enemyTarget != null)
            {
                Projectiles.Create(shootPosition, enemyTarget);
            }
        }
    }
}
