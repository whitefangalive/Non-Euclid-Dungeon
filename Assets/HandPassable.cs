using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPassable : MonoBehaviour
{
    private float timerNow = 0.0f;
    private new Collider collider;
    public LayerMask passThroughHandMask;
    private LayerMask defaultLayerMask;
    public float passableInterval = 5;

    private void Start()
    {
        collider = GetComponent<Collider>();
        defaultLayerMask = collider.excludeLayers;
    }
    public void PassThrowHand() 
    {
        collider.excludeLayers = passThroughHandMask;
        timerNow = Time.timeSinceLevelLoad;
    }

    private void FixedUpdate()
    {
        if (collider.excludeLayers == passThroughHandMask && Time.timeSinceLevelLoad - timerNow > passableInterval) 
        {
            collider.excludeLayers = defaultLayerMask;
        }
    }
}
