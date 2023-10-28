using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public CinemachineImpulseSource cinemachineImpluseSource;
    public static CameraControl Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public void OnCameraShakeEvent()
    {
        cinemachineImpluseSource.GenerateImpulse();
    }
}
