using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] BuildingConstruction buildingConstruction;

    private Image image;

    private void Awake()
    {
        image = transform.Find("mask").Find("Image").GetComponent<Image>();
    }

    private void Update()
    {
        image.fillAmount = buildingConstruction.GetBuildingConstructionProcessNormalized();
    }
}
