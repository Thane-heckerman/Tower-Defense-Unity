using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] BuildingTypeHolder hqBuildingType;

    private BuildingTypeListSO buildingTypeList;

    public static BuildingManager Instance { get; private set; }

    public BuildingTypeSO activeBuildingType;

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }


    // Start is called before the first frame update
    private void Awake()
    {
        BuildingManager.Instance = this;
    }

    void Start()
    {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        hqBuildingType.GetComponent<HealthSystem>().OnDied += HQ_OnDied;
    }

    private void HQ_OnDied(object sender, EventArgs e)
    {
        GameOverUIManager.Instance.Show();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null)
                if(CanSpawnBuilding(activeBuildingType,UtilsClass.GetMouseWorldPosition(),out string errorMessage))
            {
                if (ResourceManager.Instance.CanAfford(activeBuildingType.buildingCostAmount))
                {
                    ResourceManager.Instance.SpendResource(activeBuildingType.buildingCostAmount);

                    BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(),activeBuildingType);

                }
                else {
                    TooltipUI.Instance.Show("not enough resource" + activeBuildingType.GetResourceAmountStr(), new TooltipUI.TooltipTimer { timer = 2f });
                        }
            }
            else
            {
                TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2f});
            }
        }
    }
    
    public void SetActiveBuildingType(BuildingTypeSO buildingType) {
        activeBuildingType = buildingType;

        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });   
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage)
    {
        BoxCollider2D boxCollider2D =  buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position+ (Vector3)boxCollider2D.offset, boxCollider2D.size ,0);
        // check xem building có nằm trên một building khác không 
        bool isAreaClear = collider2DArray.Length == 0;

        if (!isAreaClear)
        {
            errorMessage = "Area is not clear";
            return false;
        }

        // check xem có building cùng loại trong vòng bán kính minConstructionSpawnRadius không
        // nếu có return false

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionSpawnRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                // has a buildingtypeholder aka is building around
                if(buildingTypeHolder.buildingType == buildingType)
                {
                    errorMessage = "Too close to a same building type";
                    return false;
                }
            }
        }
        // không cho spawn ở quá xa các building khác

        float maxConstructionSpawnRadius = 25f;

        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionSpawnRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                errorMessage = "";
                
                return true;
            }
        }

        errorMessage = "Too far from another building";
        return false;

    }

    public BuildingTypeHolder GetHQBuildingType()
    {
        return hqBuildingType;
    }
}
