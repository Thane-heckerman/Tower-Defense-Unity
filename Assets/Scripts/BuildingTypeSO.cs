using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeSO")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;

    public Transform prefab;

    public GeneratorData generatorData;

    public Sprite sprite;

    public float minConstructionSpawnRadius;

    public ResourceAmount[] buildingCostAmount;

    public int maxHealthAmount;

    public float constructionTimerMax;

    public string GetResourceAmountStr()
    {
        string str = "";
        foreach (ResourceAmount resourceAmount in buildingCostAmount)
        {
            str += resourceAmount.resourceType.shortName +" : " + resourceAmount.amount +" ";
        }
        return str;
    }
}
