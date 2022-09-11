using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamaraController : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin noise;
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void shake(float duration = 0.1f, float amplitude = 1.5f, float frecuency = 20)
    {
        StopAllCoroutines();
        StartCoroutine(shakeEffect(duration, amplitude, frecuency));
    }

    IEnumerator shakeEffect(float duration, float amplitude, float frecuency)
    {
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frecuency;
        yield return new WaitForSeconds(duration); //wait
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }
}