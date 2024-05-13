using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillHealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();

    }

    private void Start()
    {
        UpdateBar();
        SetHeathBarVisible();
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;

    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        UpdateBar();
        SetHeathBarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
        SetHeathBarVisible();
    }


    private void UpdateBar()
    {
        float healthAmount = healthSystem.GetNormalizedHealthAmount();
        
        slider.value = healthAmount;
    }


    private void SetHeathBarVisible()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
