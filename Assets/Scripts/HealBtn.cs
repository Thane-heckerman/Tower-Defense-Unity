using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealBtn : MonoBehaviour
{
    [SerializeField] private ResourceTypeSO goldResourceType;
    [SerializeField] private HealthSystem healthSystem;
    private void Awake()
    {
        
        transform.Find("Btn").GetComponent<Button>().onClick.AddListener(() =>
        {
            int missingHealthAmount = healthSystem.GetMaxHealth() - healthSystem.GetCurrentHealth();
            int healCost = missingHealthAmount / 2;
            ResourceAmount[] resourceCost = new ResourceAmount[] { new ResourceAmount { resourceType = goldResourceType, amount = healCost } };
            if (ResourceManager.Instance.CanAfford(resourceCost)) {
                // can afford
                ResourceManager.Instance.SpendResource(resourceCost);
                healthSystem.HealFull();
            }
            else {
                // cannot afford
                TooltipUI.Instance.Show("Cannot Afford Repair Cost", new TooltipUI.TooltipTimer { timer = 2f });
                }
        });
    }


}
