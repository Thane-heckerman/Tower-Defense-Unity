using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceOverlayManager : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;

    private float resourceGeneratedPerSecond;

    private TextMeshProUGUI resourceText;

    private SpriteRenderer spriteRenderer;

    private Slider slider;


    private void Awake()
    {
        resourceText = transform.Find("resourceGeneratePerSecond").GetComponent<TextMeshProUGUI>();
        spriteRenderer = transform.Find("Image").GetComponent<SpriteRenderer>();
        slider = transform.Find("Slider").GetComponent<Slider>();

    }

    private void Start()
    {
        resourceText.SetText(resourceGenerator.GetNumberOfResourceGeneratedPerSecond().ToString("F1"));
        spriteRenderer.sprite = resourceGenerator.GetGeneratorData().resourceType.prefab.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        slider.value = resourceGenerator.GetTimeNormalized();
    }


}
