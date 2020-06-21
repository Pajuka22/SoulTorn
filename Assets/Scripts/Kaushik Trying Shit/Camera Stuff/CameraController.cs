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
    private Camera cam;
    [SerializeField]
    private ZoomData defaultData = new ZoomData();
    [SerializeField]
    private ZoomData currentData = new ZoomData();
    private ZoomData startData = new ZoomData();
    private ZoomData endData = new ZoomData();
    private float time;
    private float lerpTime;
    Vector2 startPosition;
    public MovementScript player;
    private void Start()
    {
        cam = GetComponent<Camera>();
        defaultData.followPlayer = true;
        currentData.Set(defaultData);
        startData.Set(defaultData);
        endData.Set(defaultData);
    }
    private void Update()
    {
        if (time > lerpTime)
        {
            if (currentData.followPlayer)
            {
                transform.position = (Vector3)(Local2Global(currentData.center)) - 10 * Vector3.forward;
                currentData.Set(endData);
                Debug.Log(currentData.center);
            }
        }
        else
        {
            time += Time.deltaTime;
            
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
            transform.position = (Vector3)Vector2.Lerp(startPosition, endData.followPlayer ? Local2Global(endData.center) : endData.center, time / lerpTime) - Vector3.forward * 10;
        }
        else if (startData.followPlayer)
        {
            transform.position = (Vector3)((Vector2)player.transform.position + Vector2.Lerp(startData.center, endData.center, time / lerpTime)) - 10 * Vector3.forward;
        }
        else
        {
            transform.position = (Vector3)Vector2.Lerp(startPosition, endData.center, time / lerpTime) - Vector3.forward * 10;
        }
        currentData.center = currentData.followPlayer ? Global2Local(transform.position - player.transform.position) : (Vector2)transform.position;
        currentData.camSize = cam.orthographicSize;
    }
    public void DoZoomStuff(ZoomData data, float lerpTime)
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
    public void ResetDefaults(float lerpTime)
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
}
