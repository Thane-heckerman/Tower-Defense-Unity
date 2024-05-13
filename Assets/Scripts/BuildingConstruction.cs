using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
        Transform buildingConstructionTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);
        
        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();

        buildingConstruction.Setup(buildingType);//set building type ở đây vì sẽ gọi hàm này trong buiding manager
        return buildingConstruction;
    }

    private float buildingCountDownTimer;
    private float buildingCountDownTimerMax;

    private BuildingTypeSO buildingType;

    private BoxCollider2D boxCollider2D;
    private Transform buildingSpawnParticles;
    private SpriteRenderer sprite;
    private BuildingTypeHolder buildingTypeHolder;
    private void Awake()
    {
        buildingSpawnParticles = Resources.Load<Transform>("pfBuildingSpawn");
        boxCollider2D = GetComponent<BoxCollider2D>();
        sprite = transform.Find("sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
    }

    private void Update()
    {
        buildingCountDownTimer -= Time.deltaTime;
        if (buildingCountDownTimer < 0f)
        {
            Instantiate(buildingSpawnParticles, transform.position, Quaternion.identity);

            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);

            // get offset and size of the prefabs before create
            Destroy(gameObject);
        }
    }

    private void Setup(BuildingTypeSO buildingType)
    {
        this.buildingCountDownTimerMax = buildingType.constructionTimerMax;
        this.buildingType = buildingType;
        buildingCountDownTimer = buildingCountDownTimerMax;
        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
        sprite.sprite = buildingType.sprite;
        this.buildingTypeHolder.buildingType = buildingType;
    }

    public float GetBuildingConstructionProcessNormalized()
    {
        return 1 - buildingCountDownTimer / buildingCountDownTimerMax;
    }
}
