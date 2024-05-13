using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChromaticAberrationEffect : MonoBehaviour
{
    public static ChromaticAberrationEffect Instance { get; private set; }
    private float startWeight;
    private Volume volume;

    private void Awake()
    {
        Instance = this;
        volume = GetComponent<Volume>();

    }

    private void Update()
    {
        if(volume.weight > 0)
        {
            float decreasingSpeed = 1f;
            volume.weight -= Time.deltaTime * decreasingSpeed;
        }
    }

    public void SetEffect( float weight)
    {
        volume.weight = weight;
    }

}
