using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{

    //define event
    public event EventHandler OnResourceAmountChanged;

    public static ResourceManager Instance { get; private set; }

    private Dictionary<ResourceTypeSO, int> resourceTypeAmount;

    [SerializeField] private List<ResourceAmount> startResourceAmount;

    private void Awake()
    {
        Instance = this; 

        resourceTypeAmount = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceTypeAmount[resourceType] = 0;
        }

        foreach ( ResourceAmount resourceAmount in startResourceAmount)
        {
            AddResource(resourceAmount.resourceType, resourceAmount.amount);
        }
    }

    

    public void AddResource(ResourceTypeSO resourceType , int amount)
    {
        resourceTypeAmount[resourceType] += amount;

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceTypeAmount[resourceType]; 
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {

            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {

            }
                // can afford

            else return false;

        }
        return true;
    }

    public void SpendResource(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            resourceTypeAmount[resourceAmount.resourceType] -= resourceAmount.amount;
            OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
