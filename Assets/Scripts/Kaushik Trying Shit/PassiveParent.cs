using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PassiveParent : AbilityParent
{

    /*
     * these things are going to be activated by an events system. I don't feel like manually calling the activate funciton, so instead ima do this.
     * basically you choose an activation type, and then there's a few more specification options, and then i'll set it to an event in the global control
     * this is honestly the easiest way to do this. My idea is that we just smack all the abilities on the player prefab and enable the ones that we want.
    */
    protected enum ActivationType { Hit, GetHit, ActivateAbility, EnemiesCollide, None}//these are public for the custom editor.
    protected enum HitTypes { ActivatedAbility, Sword, Both, None}
    [SerializeField]
    protected ActivationType trigger;
    [SerializeField]
    [Tooltip("Won't do anything unless trigger == Hit")]
    protected HitTypes hitType = HitTypes.None;
    
    // Start is called before the first frame update
    void Start()
    {
        //subscribe activate to events based on what triggers it. Using AbilityManager for this
        switch (trigger)
        {
            case ActivationType.Hit:
                switch (hitType)
                {
                    case HitTypes.ActivatedAbility:
                        break;
                    case HitTypes.Sword:
                        break;
                    case HitTypes.Both:
                        break;
                    case HitTypes.None:
                        break;
                }
                break;
            case ActivationType.GetHit:
                
                break;
            case ActivationType.ActivateAbility:
                break;
            case ActivationType.EnemiesCollide:
                break;
            case ActivationType.None:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}