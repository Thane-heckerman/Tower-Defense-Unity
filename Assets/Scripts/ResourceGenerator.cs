using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private float Timer;

    private GeneratorData generatorData;

    private float timerMax;

    // Start is called before the first frame update
    private void Awake()
    {
        generatorData = GetComponent<BuildingTypeHolder>().buildingType.generatorData;

        timerMax = generatorData.timerMax;

    }

    private void Start()
    {
        int nearbyResourceAmountNode = 0;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, generatorData.nearbyResourceNodeDetectRadius); ;
        foreach (Collider2D collider in collider2DArray)
        {
            ResourceTypeHolder resourceTypeHolder = collider.GetComponent<ResourceTypeHolder>();
            if (resourceTypeHolder != null)
            {
                if(resourceTypeHolder.resourceType == generatorData.resourceType)
                {
                    nearbyResourceAmountNode++;

                }
            }
        }
        nearbyResourceAmountNode = Mathf.Clamp(nearbyResourceAmountNode, 0, generatorData.maxResourceAmount);
        Debug.Log("resourceAmountNode" + nearbyResourceAmountNode);
        
        if (nearbyResourceAmountNode == 0)
        {
            enabled = false;
        }
        else
        {
            // nếu maxAmount thì sẽ generate resource nhanh hơn 2 lần so với min dưới là phép tính

            timerMax = (generatorData.timerMax / 2f) + generatorData.timerMax *
                (1 - (float)nearbyResourceAmountNode / generatorData.maxResourceAmount);
        }// timerMax càng nhỏ thì generate resource càng nhanh


    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            Timer += timerMax;
            ResourceManager.Instance.AddResource(generatorData.resourceType, 1);
        }
    }

    public float GetTimeNormalized ()
    {
        return Timer / timerMax;
    }

    public GeneratorData GetGeneratorData()
    {
        return generatorData;
    }

    public float GetNumberOfResourceGeneratedPerSecond()
    {
        return 1 / timerMax;
    }
}
