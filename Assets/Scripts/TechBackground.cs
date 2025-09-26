using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechBackground : MonoBehaviour
{
    public List<BackgroundLightColor> backgroundLightColors;
    

    // Update is called once per frame
    public void CurrentPointReachedDestination(int index)
    {
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
