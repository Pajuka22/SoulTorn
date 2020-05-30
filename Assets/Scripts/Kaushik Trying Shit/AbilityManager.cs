using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;
    bool dunnit;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //creating a bunch of events to trigger when enemies get hit, when the player gets hit, etc. mainly for passive abilities.
    public event Action hitAny;
    public void HitAny()
    {
        hitAny?.Invoke();
    }
    public event Action hitSword;
    public void HitSword()
    {
        hitSword?.Invoke();
    }
    public event Action hitAbility;
    public void HitAbility()
    {

    }
}
