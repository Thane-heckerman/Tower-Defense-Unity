using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour  
{
    public static Enemy CreateEnemy(Vector3 position) {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private float lookForTargetTimerMax = .2f;

    private float lookForTargetTimer;

    private Transform targetTransform;

    private float moveSpeed = 10f;

    private Rigidbody2D rb2d;

    private HealthSystem healthSystem;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();

    }

    private void Start()
    {
        if (BuildingManager.Instance.GetHQBuildingType() != null)
        {
            targetTransform = BuildingManager.Instance.GetHQBuildingType().gameObject.transform;
        }
        lookForTargetTimer = Random.Range(.1f ,lookForTargetTimerMax);
        healthSystem.OnDied += HealthSystem_OnDied;

    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Instantiate(Resources.Load<Transform>("pfEnemyDieParticles"), transform.position, Quaternion.identity);
        ChromaticAberrationEffect.Instance.SetEffect(.5f);
        CameraShake.Instance.ShakeCamera(8f, .5f);
        Destroy(gameObject);
    }

    public void Update()
    {
        if (targetTransform != null)
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;
            rb2d.velocity = moveDir * moveSpeed;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTarget();
        }
    }

    // gây dmg khi mà chạm 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check xem có phải building không nếu va vào enemy thì cũng k đc trừ máu
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null)
        {
            //là building
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damaged(10);
            Destroy(gameObject);
        }
    }

    private void LookForTarget()
    {
        float lookingRadius = 10f;
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, lookingRadius);
        //check xem có building không
        foreach(Collider2D collider in colliderArray)
        {
            Building building = collider.GetComponent<Building>();
            if (building != null)
            {
                if (targetTransform == null)
                {
                    targetTransform = building.transform;
                }
                else
                {
                    if (Vector2.Distance(transform.position, building.transform.position) <
                        Vector2.Distance(transform.position, targetTransform.position))
                    {
                        targetTransform = building.transform;
                    }
                }
            }
        }
        if (targetTransform == null)
        {
            if (BuildingManager.Instance.GetHQBuildingType() != null)
            {
                targetTransform = BuildingManager.Instance.GetHQBuildingType().gameObject.transform;
            }
        }
    }

}
