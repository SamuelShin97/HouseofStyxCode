using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class ScreenShake : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera;
    private CinemachineBasicMultiChannelPerlin playerCameraNoise;

    public float duration = 0.3f; // duration of noise shake on camera
    public float amplitude = 1.2f;
    public float frequency = 2.0f;
    private float noiseOverTime = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator _ProcessShake(float shakeIntensity = 5f, float shakeTiming = 0.5f)
    {
        Noise(1, shakeIntensity);
        yield return new WaitForSeconds(shakeTiming);
        Noise(0, 0);
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        /*playerCamera.topRig.Noise.m_AmplitudeGain = amplitudeGain;
        playerCamera.middleRig.Noise.m_AmplitudeGain = amplitudeGain;
        playerCamera.bottomRig.Noise.m_AmplitudeGain = amplitudeGain;

        playerCamera.topRig.Noise.m_FrequencyGain = frequencyGain;
        playerCamera.middleRig.Noise.m_FrequencyGain = frequencyGain;
        playerCamera.bottomRig.Noise.m_FrequencyGain = frequencyGain;*/
    }
}
