﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //we're gonna make a bunch of activation overloads for all the situations we need.
    protected virtual void Activate()
    {
        //my debug messages tend to be vulgar, and i may or may not try to change that some time soon
        Debug.Log("Bitch");
        //but not today.
    }
    //who knows when i'd need to use this
    protected virtual void Activate(Enemy e)
    {

    }
    //for two enemies colliding with flame touch
    protected virtual void Activate(Enemy e1, Enemy e2)
    {

    }
}