using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystem1;

    private void Play()
    {
        foreach (ParticleSystem particleSystem in particleSystem1)
        {
            particleSystem.Play();
        }
        
    }

    private void Stop()
    {
        foreach (ParticleSystem particleSystem in particleSystem1)
        {
            particleSystem.Stop();
        }
    }

}
