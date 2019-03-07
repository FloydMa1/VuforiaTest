using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;
    
    public TestObject[] arObjects;

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }

        instance = this;

        TrackingDefiner.OnMarkerDetected += OnMarkerDetected;

        arObjects = Resources.LoadAll<TestObject>("Prefabs/ScriptableObjects/");
    }

    private void OnDestroy()
    {
        TrackingDefiner.OnMarkerDetected -= OnMarkerDetected;
    }

    private void OnMarkerDetected()
    {
            
    }
}
