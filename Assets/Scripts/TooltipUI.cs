using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }
    [SerializeField] private RectTransform canvasRectransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    private TooltipTimer tooltipTimer;
    private void Awake()
    {
        TooltipUI.Instance = this;
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        Hide();

    }

    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition/canvasRectransform.localScale.x;
        // nếu vị trí của tooltip vượt quá canvas size
        // tức là vị trí x của chuột + giá trị width cùa background tức điểm cuối của chữ chính là tính điểm cuối của cái tooltip
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectransform.rect.width)
        {
            anchoredPosition.x = canvasRectransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectransform.rect.height)
        {
            anchoredPosition.y = canvasRectransform.rect.height - backgroundRectTransform.rect.height;
        }
        //validate anchoredPosition
        rectTransform.anchoredPosition = anchoredPosition;
        if (tooltipTimer != null)
        {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer < 0f)
            {
                Hide();
            }
        }
    }

    private void SetText (string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);

        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void Show(string text, TooltipTimer tooltipTimer = null)
    {
        this.tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(text);
    }

    public void Hide()
    {
        gameObject.SetActive(false);

    }

    public class TooltipTimer {
        public float timer;
    }
}
