using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class TrackingDefiner : MonoBehaviour, ITrackableEventHandler
{

    public delegate void MarkerDetectedEvent();
    public static event MarkerDetectedEvent OnMarkerDetected;

    public delegate void RedirectEvent();
    public static event RedirectEvent OnRedirect;
    public static event RedirectEvent OnTracked;
    public static event RedirectEvent OnLost;

    private ImageTargetBehaviour targetBehaviour;
    private TrackableBehaviour trackableBehaviour;

    private ObjectManager objectManager;
    private UIManager uiManager;

    private string url;

    private void Start()
    {
        targetBehaviour = GetComponent<ImageTargetBehaviour>();
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (trackableBehaviour)
        {
            trackableBehaviour.RegisterTrackableEventHandler(this);
        }

        objectManager = ObjectManager.instance;
        uiManager = UIManager.instance;
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
                url = objectManager.arObjects[i].url;
                uiManager.url = url;
            }
        }
        OnTracked?.Invoke();
    }

    private void onTrackingLost()
    {
        if (transform.childCount > 0)
        {
            SetChildrenActive(false);
        }
        OnLost?.Invoke();
    }

    private void SetChildrenActive(bool activeState)
    {
        for (int i = 0; i <= transform.childCount; i++)
        {
            transform.GetChild(i++).gameObject.SetActive(activeState);
        }
    }
}
