using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    private Transform playerTransform;
    private Vector3 lastPlayerPosition;
    private Vector3 deltaPlayerPosition;

    private void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();

        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;

        // Find the player's transform (replace "Player" with your actual player object name or tag)
        playerTransform = GameObject.FindWithTag("Player").transform;
        lastPlayerPosition = playerTransform.position;
    }

    private void Move(float delta)
    {
        // Calculate the movement of the player since the last frame
        deltaPlayerPosition = playerTransform.position - lastPlayerPosition;

        foreach (ParallaxLayer layer in transform.GetComponentsInChildren<ParallaxLayer>())
        {
            layer.Move(delta, deltaPlayerPosition);
        }

        lastPlayerPosition = playerTransform.position;
    }



    //public ParallaxCamera parallaxCamera;
    //List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    //void Start()
    //{
    //    if (parallaxCamera == null)
    //        parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
    //    if (parallaxCamera != null)
    //        parallaxCamera.onCameraTranslate += Move;
    //    SetLayers();
    //}

    //void SetLayers()
    //{
    //    parallaxLayers.Clear();
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

    //        if (layer != null)
    //        {
    //            layer.name = "Layer-" + i;
    //            parallaxLayers.Add(layer);
    //        }
    //    }
    //}
    //void Move(float delta)
    //{
    //    foreach (ParallaxLayer layer in parallaxLayers)
    //    {
    //        layer.Move(delta);
    //    }
    //}
}