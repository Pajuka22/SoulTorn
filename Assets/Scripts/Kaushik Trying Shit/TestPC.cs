using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DoubleJump))]
[RequireComponent(typeof(MovementScript))]
[RequireComponent(typeof(PlayerStates))]
public class TestPC : Player
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!GetComponent<DoubleJump>().on)
            {
                GetComponent<DoubleJump>().Enable();
            }
            else
            {
                GetComponent<DoubleJump>().Disable();
            }
        }
    }
}
