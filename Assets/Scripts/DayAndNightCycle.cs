using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class DayAndNightCycle : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    private Light2D light2D;
    private float dayTime;
    private float secondPerDay = 10f;
    private float dayTimeSpeed;


    // Start is called before the first frame update
    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        dayTimeSpeed = 1 / secondPerDay;
    }

    // Update is called once per frame
    void Update()
    {
        dayTime += Time.deltaTime * dayTimeSpeed;
        light2D.color = gradient.Evaluate(dayTime % 1);
    }
}
