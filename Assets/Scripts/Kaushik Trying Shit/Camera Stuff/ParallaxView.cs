using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxView : MonoBehaviour
{
    public Vector3 relativeStartPos;
    private float zDepth;
    [SerializeField]
    public CameraController cam;
    private float startScale;

    // Start is called before the first frame update
    void Start()
    {
        relativeStartPos = transform.position - cam.transform.position;
        Debug.Log(transform.position.z);
        zDepth = default;
        startScale = transform.localScale.y;

        if (zDepth == 0 && transform.position.z != 0)
            zDepth = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        

        Vector3 camMoved = cam.transform.position - cam.startPos;
        ChangePos(camMoved);
        ChangeScale(camMoved);
        transform.position = new Vector3(transform.position.x, transform.position.y, zDepth);
        
    }

    public void ChangeScale(Vector3 camMoved)
    {
        float newScale = startScale * (relativeStartPos.z) / (transform.position.z - cam.transform.position.z) * cam.currentData.camSize / cam.defaultData.camSize;
        Debug.Log(name + "1: " + cam.currentData.camSize / cam.defaultData.camSize);
        Debug.Log(name + "2: " + relativeStartPos.z);
        Debug.Log(name + "3: " + (transform.position.z - cam.transform.position.z));

        transform.localScale = new Vector3(newScale, newScale, newScale);

        //float desiredAngularScale = startScale * (relativeStartPos.z) / (transform.position.z - cam.transform.position.z);

    }

    public void ChangePos(Vector3 camMoved)
    {
        //camMoved = cam.transform.position - cam.startPos;
        transform.position = (Vector2)(cam.transform.position + relativeStartPos - camMoved / (zDepth - cam.transform.position.z * Mathf.Sin(cam.angle)));
        transform.position += new Vector3(0, 0, zDepth);
    }
}
