using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechBackground : MonoBehaviour
{
    public List<BackgroundLightColor> backgroundLightColors;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void CurrentPointReachedDestination(int index)
    {
        Debug.Log("length: " + backgroundLightColors.Count );
        if (index < backgroundLightColors.Count)
        {
            backgroundLightColors[index].PointReachedDestination();
        }
        else
        {
            Debug.LogWarning("Index out of range for background light colors.");
        }
        
    }
}
