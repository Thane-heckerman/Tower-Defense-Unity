using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float positionOffsetY;

    [SerializeField] private bool runOnce;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        float precisionRange = 5f;
        spriteRenderer.sortingOrder = (int)( - (transform.position.y+ positionOffsetY) * precisionRange);
        if (runOnce == true)
        {
            Destroy(this); 
        }
    }
}
