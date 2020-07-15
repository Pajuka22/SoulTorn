using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [NonSerialized]
    public Camera cam;
    public ZoomData defaultData = new ZoomData();
    [NonSerialized]
    public ZoomData currentData = new ZoomData();
    private ZoomData startData = new ZoomData();
    private ZoomData endData = new ZoomData();
    private float time;
    private float lerpTime;
    Vector2 startPosition;
    public MovementScript player;
    public float zDistanceFromPlayer;
    [System.NonSerialized]
    public float angle;

    public Vector3 startPos;
    private void Start()
    {
        cam = GetComponent<Camera>();
        defaultData.followPlayer = true;
        ResetDefaults();
        currentData.Set(defaultData);
        startData.Set(defaultData);
        endData.Set(defaultData);
        zDistanceFromPlayer = transform.position.z - player.transform.position.z;
        angle = Mathf.Atan(cam.rect.height / zDistanceFromPlayer);
        startPos = transform.position;

    }
    private void LateUpdate()
    {
        if (time > lerpTime)
        {
            if (currentData.followPlayer)
            {
                //transform.position = player.transform.position + (Vector3)(Local2Global(currentData.center)) + zDistanceFromPlayer * currentData.camSize / defaultData.camSize * Vector3.forward;
                //Debug.Log(currentData.center);\
                transform.position = Local2Global(currentData.center);
                transform.position = new Vector3(transform.position.x, transform.position.y, zDistanceFromPlayer * currentData.camSize / defaultData.camSize);
            }
        }
        else
        {
            time += Time.fixedDeltaTime;
            
            DoLerping();
        }
        
    }
    public void DoLerping()
    {
        float time = this.time;
        Mathf.Clamp(time, 0, lerpTime);
        cam.orthographicSize = Mathf.Lerp(startData.camSize, endData.camSize, time / lerpTime);
        if (endData.followPlayer != startData.followPlayer)
        {
            transform.position = Vector2.Lerp(startPosition, endData.followPlayer ? Local2Global(endData.center) : endData.center, time / lerpTime);
        }
        else if (startData.followPlayer)
        {
            transform.position = ((Vector2)player.transform.position + Vector2.Lerp(startData.center, endData.center, time / lerpTime));
        }
        else
        {
            transform.position = Vector2.Lerp(startPosition, endData.center, time / lerpTime);
        }
        
        currentData.center = currentData.followPlayer ? Global2Local(transform.position - player.transform.position) : (Vector2)transform.position;
        currentData.camSize = cam.orthographicSize;
        if(time >= lerpTime)
        {
            currentData.Set(endData);
        }
        float z = zDistanceFromPlayer * currentData.camSize / defaultData.camSize;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
    public void DoZoomStuff(ZoomData data, float lerpTime = 0)
    {
        if (!endData.EqualValues(data))
        {
            startPosition = transform.position;
            startData.Set(currentData);
            endData.Set(data);
            this.lerpTime = lerpTime;
            time = 0;
            currentData.followPlayer = data.followPlayer;
        }
    }
    public void ResetDefaults(float lerpTime = 0)
    {
        DoZoomStuff(defaultData, lerpTime);
    }
    Vector2 Local2Global(Vector2 v)
    {
        return v.x * player.transform.right + v.y * player.transform.up + player.transform.position;
    }
    Vector2 Global2Local(Vector2 v)
    {
        float x = Vector2.Dot(v, player.transform.right) / player.transform.right.magnitude;
        float y = Vector2.Dot(v, player.transform.up) / player.transform.up.magnitude;
        return new Vector2(x, y);

    }
    float GetZDist()
    {
        return Mathf.Abs(transform.position.z - player.transform.position.z);
    }
}
