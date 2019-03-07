using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject[] buttons;
    public bool canRedirect = false;
    public string url;

    private UniWebView webView;
    private GameObject confirmButton;
    private GameObject closeButton;

    private void Awake()
    {
        if(instance)
        {
            Destroy(instance);
        }

        instance = this;

        TrackingDefiner.OnRedirect += OnRedirect;
        TrackingDefiner.OnTracked += OnTracked;
        TrackingDefiner.OnLost += OnLost;

        buttons = Resources.LoadAll<GameObject>("Prefabs/Buttons/");
        webView = FindObjectOfType<UniWebView>().GetComponent<UniWebView>();

    }

    private void OnDestroy()
    {
        TrackingDefiner.OnRedirect -= OnRedirect;
        TrackingDefiner.OnTracked -= OnTracked;
        TrackingDefiner.OnLost -= OnLost;
    }

    private void OnTracked()
    {
        if(confirmButton == null)
        {
            confirmButton = Instantiate(buttons[0], this.transform);
            canRedirect = true;
            confirmButton.GetComponent<Button>().onClick.AddListener(OnRedirect);
        }
    }

    private void OnLost()
    {
        Debug.Log("lost");
        Destroy(confirmButton);
        canRedirect = false;
    }

    private void OnRedirect()
    {
        if(canRedirect)
        {
            webView.Show();
            webView.Load(url);
            closeButton = Instantiate(buttons[1], this.transform);
        }
    }

    private void OnClose()
    {

    }

}
