using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    /* tức là sẽ find gameobject có tên là resourcesTemplate
     * load resourceTypeList;
     * instantiate gameobject và move anchor position của mỗi object đc spawn ra bởi 1 offset amount 
     * trên trục Ox
     * sau đó sẽ access vào trong resourceListSO để lấy là image và resourceAmount trong ResourceGenerator
     * qua singleton
     * sử dụng namespace unityEngine.UI để set text và image lấy từ resourceType
    */
    private ResourceTypeListSO resourceTypeList;

    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;

    private void Awake()
    {
        
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
    
        Transform resourceTemplate = transform.Find("resourceTemplate");

        resourceTemplate.gameObject.SetActive(false);

        int index = 0;

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);

            resourceTransform.gameObject.SetActive(true);

            resourceTypeTransformDictionary[resourceType] = resourceTransform;

            // set offsetAmount

            float offsetAmount = -160f;

            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

  
            resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType.prefab.GetComponent<SpriteRenderer>().sprite;


            index++;
        }

    }

    private void Start()
    {
        // call event từ resourceManager

        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        // store resourceType vào 1 dictionary để update amount của từng resourceType
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTransfrom = resourceTypeTransformDictionary[resourceType];

            string amount = ResourceManager.Instance.GetResourceAmount(resourceType).ToString();

            resourceTransfrom.Find("Text").GetComponent<TextMeshProUGUI>().SetText(amount);

        }

    }

}
