using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    private BuildingTypeSO buildingType;

    private HealthSystem healthSystem;

    private Transform buildingDemonlishBtn;

    private Transform healBtn;

    private void Awake()
    {

        
        buildingDemonlishBtn = transform.Find("BuildingDemonlishBtn");
        healBtn = transform.Find("HealBtn");
        HideBtnHeal();
        HideBtnDemonlish();
    }

    private void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetMaxHealthAmount(buildingType.maxHealthAmount, true);
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        if (healthSystem.IsFullHealth())
        {
            HideBtnHeal();
        }
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        CameraShake.Instance.ShakeCamera(10f, 1f);
        ChromaticAberrationEffect.Instance.SetEffect(1);
        ShowBtnHeal();
    }

 
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        CameraShake.Instance.ShakeCamera(15f, 1.8f);
        Destroy(this.gameObject);
        Instantiate(Resources.Load<Transform>("pfBuildingDestroyedParticles"), transform.position, Quaternion.identity);
    }

    private void OnMouseEnter()
    {
        ShowBtnDemonlish();
    }

    private void OnMouseExit()
    {
        HideBtnDemonlish();
    }

    private void ShowBtnDemonlish()
    {
        if (buildingDemonlishBtn != null)
        {
            buildingDemonlishBtn.gameObject.SetActive(true);
        }
    }

    private void HideBtnDemonlish()
    {
        if (buildingDemonlishBtn != null)
        {
            buildingDemonlishBtn.gameObject.SetActive(false);
        }
    }

    private void ShowBtnHeal()
    {
        if (healBtn != null)
        {
            healBtn.gameObject.SetActive(true);
        }
    }

    private void HideBtnHeal()
    {
        if (healBtn != null)
        {
            healBtn.gameObject.SetActive(false);
        }
    }
}
