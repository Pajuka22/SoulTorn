using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public enum AnimStates
    {
        idle, jump, stun, run, atk, runatk, hurt, death //turn?
    }
    public AnimStates current = AnimStates.idle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }


    public void SetStateToStun()
    {
        current = AnimStates.stun;
    }
    public void SetStateToIdle()
    {
        current = AnimStates.idle;
    }
}
