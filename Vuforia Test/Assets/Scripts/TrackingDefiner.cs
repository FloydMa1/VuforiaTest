using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingDefiner : MonoBehaviour, ITrackableEventHandler
{

    public delegate void MarkerDetectedEvent();
    public static event MarkerDetectedEvent OnMarkerDetected;

    private ImageTargetBehaviour targetBehaviour;
    private TrackableBehaviour trackableBehaviour;

    private ObjectManager objectManager;


    private void Awake()
    {
        objectManager = ObjectManager.instance.GetComponent<ObjectManager>();
    }

    private void Start()
    {
        targetBehaviour = GetComponent<ImageTargetBehaviour>();
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (trackableBehaviour)
        {
            trackableBehaviour.RegisterTrackableEventHandler(this);
        }

    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();

        }
        else
        {
            onTrackingLost();
        }
    }

    private void OnTrackingFound()
    {
        for (int i = 0; i < objectManager.arObjects.Length; i++)
        {
            if (objectManager.arObjects[i].name == this.targetBehaviour.ImageTarget.Name)
            {
                //OnMarkerDetected?.Invoke();
                Instantiate(objectManager.arObjects[i].gameObject, this.transform);
            }
        }
    }

    private void onTrackingLost()
    {
        if (transform.childCount > 0)
        {
            SetChildrenActive(false);
        }
    }

    private void SetChildrenActive(bool activeState)
    {
        for (int i = 0; i <= transform.childCount; i++)
        {
            transform.GetChild(i++).gameObject.SetActive(activeState);
        }
    }
}
