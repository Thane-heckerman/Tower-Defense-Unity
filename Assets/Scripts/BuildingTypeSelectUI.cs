using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingTypeSelectUI : MonoBehaviour
{
    // spawn button và grab sprite in building type (trong building manager)

    private Dictionary<BuildingTypeSO,Transform> btnTransformDictionary;

    private Transform arrowBtn;

    [SerializeField] private Sprite arrowSprite;

    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypes;

    private void Awake()
    {
        btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        Transform btnTemplate = transform.Find("btnTemplate");

        btnTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        int index = 0;

        float offsetPosition = 130f;

        arrowBtn = Instantiate(btnTemplate, transform);

        arrowBtn.gameObject.SetActive(true);

        arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetPosition * index, 0);

        arrowBtn.Find("image").GetComponent<Image>().sprite = arrowSprite;

        arrowBtn.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetActiveBuildingType(null); });

        MouseEnterExitEvent mouseEnterExitEvent = arrowBtn.GetComponent<MouseEnterExitEvent>();

        mouseEnterExitEvent.OnMouseEnter += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Show("Arrow", new TooltipUI.TooltipTimer { timer = 2f });
        };

        mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) =>
        {
            TooltipUI.Instance.Hide();
        };

        index++;

        //add function trong suốt runtime là khi click sẽ gọi SetActiveBuildingType trong manager nhận buildingtype mà click vào

        foreach (BuildingTypeSO buildingType in buildingTypeList.list )
        {

            if (ignoreBuildingTypes.Contains(buildingType)) continue;

            offsetPosition = 130f;

            Transform btnTransform = Instantiate(btnTemplate, transform);

            btnTransform.gameObject.SetActive(true);

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetPosition * index, 0);

            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            //add function trong suốt runtime là khi click sẽ gọi SetActiveBuildingType trong manager nhận buildingtype mà click vào
            btnTransform.GetComponent<Button>().onClick.AddListener(() => { BuildingManager.Instance.SetActiveBuildingType(buildingType); });

            mouseEnterExitEvent = btnTransform.GetComponent<MouseEnterExitEvent>();

            mouseEnterExitEvent.OnMouseEnter += (object sender , EventArgs e) =>
            {
                TooltipUI.Instance.Show(buildingType.nameString + "\n" + buildingType.GetResourceAmountStr(), new TooltipUI.TooltipTimer { timer = 2f });
            };
            mouseEnterExitEvent.OnMouseExit += (object sender, EventArgs e) =>
            {
                TooltipUI.Instance.Hide();
            };
            // bỏ raycast target trong background để đỡ bị show hide liên tục và trong extra settings của text

            btnTransformDictionary[buildingType] = btnTransform;

            index++;
        }
    }

   

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        UpdateActiveBuildingType();
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        UpdateActiveBuildingType();
    }

    private void UpdateActiveBuildingType()
    {
        arrowBtn.Find("selected").gameObject.SetActive(false);
        //set all selected image to false
        foreach (BuildingTypeSO buildingType in btnTransformDictionary.Keys)
        {
            Transform btnTransform = btnTransformDictionary[buildingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType != null)
        {
            btnTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);

        }
        else
        {
            arrowBtn.Find("selected").gameObject.SetActive(true);
        }
    }
}
