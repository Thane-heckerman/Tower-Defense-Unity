using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour
{
    public static Projectiles Create(Vector3 shootPosition, Enemy targetEnemy)
    {

        Transform pfArrow = Resources.Load<Transform>("pfArrow");
        Transform arrow = Instantiate(pfArrow, shootPosition, Quaternion.identity);
        Projectiles arrowTransform = arrow.GetComponent<Projectiles>();
        arrowTransform.setTargetEnemy(targetEnemy);
        return arrowTransform;
    }
    private float timeToDie = 5f;

    private Vector3 lastMoveDir;

    private Enemy targetEnemy;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void setTargetEnemy(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    public void Update()
    {
        Vector3 dir;
        if (targetEnemy != null)
        {
            dir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = dir;
        }
        else
        {
            dir = lastMoveDir ;
        }
        float moveSpeed = 20f;
        float degree = UtilsClass.GetRotation(dir);
        transform.position += moveSpeed * dir * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, degree);

        timeToDie -= Time.deltaTime;
        if (timeToDie < 0f)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)// vì sử dụng kinematic body type rigidbody
    {
        Enemy enemy = collision.transform.GetComponent<Enemy>();
        
        if ( enemy != null)
        {
            HealthSystem healthSystem = enemy.GetComponent<HealthSystem>();
            healthSystem.Damaged(10);
            Destroy(gameObject);

        }

    }
}


