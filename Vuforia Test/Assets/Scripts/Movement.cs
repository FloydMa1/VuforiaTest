using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody player;

    private Pose touchPosition;

    void Start()
    {
        player = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.current.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                MovePlayer();
            }
        }
    }

    private void MovePlayer()
    {
        player.MovePosition(Input.GetTouch(0).position);
    }
}
