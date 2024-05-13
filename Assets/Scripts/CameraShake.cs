using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    private CinemachineVirtualCamera cinenmachineCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float shakeTimer;
    private float timerMax;
    private float startIntensity;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cinenmachineCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinenmachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if( shakeTimer < timerMax)
        {
            shakeTimer += Time.deltaTime;
            float amplitude = Mathf.Lerp(startIntensity, 0f, shakeTimer / timerMax);
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        }
    }

    public void ShakeCamera (float intensity, float timerMax)
    {
        this.timerMax = timerMax;
        shakeTimer = 0f;
        startIntensity = intensity;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }
}
