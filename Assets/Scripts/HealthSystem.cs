using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem Instance { get; private set; }

    [SerializeField] private int maxHealth;
    private int currentHealth;

    public event EventHandler OnDamaged;

    public event EventHandler OnDied;

    public event EventHandler OnHealed;

    private BuildingTypeHolder buildingTypeHolder;

    private void Awake()
    {
        HealthSystem.Instance = this;
        if (buildingTypeHolder != null)
        {
            buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        }
        
    }

    private void Start()
    {
        if (buildingTypeHolder != null)
        {
            maxHealth = buildingTypeHolder.buildingType.maxHealthAmount;

        }

        currentHealth = maxHealth;

    }
    public void Damaged(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
             Damaged(20);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetNormalizedHealthAmount()
    {
        return (float) currentHealth / maxHealth;
    }

    private bool IsDead()
    {
        return currentHealth == 0;
    }

    public bool IsFullHealth()
    {
        return currentHealth == maxHealth;
    }

    public void SetMaxHealthAmount(int maxHealthBuilding, bool updateCurrentHealthAmount)
    {
        this.maxHealth = maxHealthBuilding;

        if (updateCurrentHealthAmount)
        {
            currentHealth = maxHealthBuilding;
        }
    }

    public void HealFull()
    {
        OnHealed?.Invoke(this, EventArgs.Empty);
        currentHealth = maxHealth;
    }
}
