using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MovementScript))]
public class AbilityParent : MonoBehaviour
{
    [System.NonSerialized]
    public bool unlocked;
    [System.NonSerialized]
    public bool on;//optional
    Player player;
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
    protected virtual void Activate(AbilityParent a)
    {

    }
    protected virtual void Activate(Enemy e, float damage)
    {

    }
    public virtual void Deactivate()
    {

    }
    public virtual void Enable()
    {
        on = true;
    }
    public virtual void Disable()
    {
        Deactivate();
        on = false;
    }
}
