using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ZoomZone : MonoBehaviour
{
    new private BoxCollider2D collider;
    public bool returnToDefault;
    public ZoomData zoomData;
    public float lerpTime;
    //public MovementScript player;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class ZoomData
{
    /*[Range(0, 1)]
    public float width;
    [Range(0, 1)]
    public float height;
    public float x;
    public float y;*/
    public float camSize;
    public bool followPlayer;
    [Tooltip("if followPlayer, this is relative to the player. otherwise it's absolute.")]
    public Vector2 center;
    public void resetToCamera(Camera cam)
    {
        /*width = cam.rect.width;
        height = cam.rect.height;
        x = cam.rect.x;
        y = cam.rect.y;*/
        camSize = cam.orthographicSize;
    }
    public void Set(ZoomData data)
    {
        camSize = data.camSize;
        followPlayer = data.followPlayer;
        center = data.center;
    }
    public bool EqualValues(ZoomData data)
    {
        Debug.Log("Cameras: " + (camSize == data.camSize));
        Debug.Log("FollowPlayer" + (followPlayer == data.followPlayer));
        Debug.Log("Centers" + (center == data.center));
        return camSize == data.camSize && followPlayer == data.followPlayer && center == data.center;
    }
}