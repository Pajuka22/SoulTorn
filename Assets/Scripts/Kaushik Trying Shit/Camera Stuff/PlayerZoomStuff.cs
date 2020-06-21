using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoomStuff : MonoBehaviour
{
    public static PlayerZoomStuff instance;
    public CameraController cam;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if(cam == null)
        {
            cam = Camera.main.GetComponent<CameraController>();
        }
    }
    private void EnterZoomZone(ZoomData zoomData, float lerpTime)
    {
        cam.DoZoomStuff(zoomData, lerpTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (UtilityLibrary.Has<ZoomZone>(other.gameObject))
        {
            ZoomZone zoom = other.GetComponent<ZoomZone>();
            if (zoom.returnToDefault)
            {
                cam.ResetDefaults(zoom.lerpTime);
            }
            else
            {
                EnterZoomZone(zoom.zoomData, zoom.lerpTime);
            }
        }
    }
}
