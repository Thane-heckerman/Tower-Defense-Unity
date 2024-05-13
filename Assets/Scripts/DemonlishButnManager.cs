using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonlishButnManager : MonoBehaviour
{
    private Button demonlishBtn;
    [SerializeField] private Building building;

    private void Awake()
    {
        BuildingTypeHolder buildingTypeHolder = building.GetComponent<BuildingTypeHolder>();
        transform.Find("Btn").GetComponent<Button>().onClick.AddListener(() =>
        {
            foreach(ResourceAmount resourceAmount in buildingTypeHolder.buildingType.buildingCostAmount)
            {
                ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt( resourceAmount.amount * .6f));
            }
            Destroy(building.gameObject);
        });
    }

   
}
