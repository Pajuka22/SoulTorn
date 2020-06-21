using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityLibrary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int sign(float input)
    {
        if (input == 0)
        {
            return 0;
        }
        else if (input > 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
    public static bool Has<T>(GameObject obj)
    {
        return obj.GetComponent<T>() != null;
    }

    public static Vector2 LocalToWorld2DRotOnly(Vector2 vector, Transform space)
    {
        return vector.x * space.right + vector.y * space.up;
    }

}
