using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamShakeManager : MonoBehaviour
{
    public static CamShakeManager Instance;
    public float globalShakeMagnitude = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void CameraShake(CinemachineImpulseSource impulseSource, float magnitude = 1f)
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse(magnitude * globalShakeMagnitude);
        }
    }
}
