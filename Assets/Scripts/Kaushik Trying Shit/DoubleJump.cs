using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : PassiveParent
{
    MovementScript movement;
    [SerializeField]
    private float height;
    
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<MovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Enable()
    {
        on = true;
        movement.jumpHeight.Add(height);
    }
    public override void Disable()
    {
        on = false;
        float jump = movement.jumpHeight[0];
        movement.jumpHeight.Clear();
        movement.jumpHeight.TrimExcess();
        movement.jumpHeight.Add(jump);
    }
}
