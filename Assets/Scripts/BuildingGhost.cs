using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteObject;

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        spriteObject = transform.Find("image").gameObject;
        Hide();
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (e.activeBuildingType == null) Hide();
        else
        {
            Sprite ghostSprite = e.activeBuildingType.sprite;
            
            Show(ghostSprite);
        }
    }

    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }

    private void Show(Sprite ghostSprite)
    {
        spriteObject.SetActive(true);
        spriteObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;

    }

    private void Hide()
    {
        spriteObject.SetActive(false);
    }
}
