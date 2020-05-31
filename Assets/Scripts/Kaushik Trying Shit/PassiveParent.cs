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
    public enum Types { Hit, GetHit, ActivateAbility, EnemiesCollide, None}//these are public for the custom editor.
    public enum HitTypes { ActivatedAbility, Sword, Both, None}
    [SerializeField]
    readonly protected Types trigger = Types.None;
    [SerializeField]
    [Tooltip("Won't do anything unless trigger == Hit")]
    readonly protected HitTypes hitType = HitTypes.None;
    
    // Start is called before the first frame update
    void Start()
    {
        AttachToEvents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Enable()
    {
        on = true;
        AttachToEvents();
    }
    public override void Disable()
    {
        on = false;
        RemoveFromEvents();
    }
    public virtual void AttachToEvents()
    {
        //subscribe activate to events based on what triggers it. Using AbilityManager for this
        switch (trigger)
        {
            case Types.Hit:
                switch (hitType)
                {
                    case HitTypes.ActivatedAbility:
                        AbilityManager.instance.hitAbility += Activate;
                        break;
                    case HitTypes.Sword:
                        AbilityManager.instance.hitSword += Activate;
                        break;
                    case HitTypes.Both:
                        AbilityManager.instance.hitAny += Activate;
                        break;
                    case HitTypes.None:
                        Debug.Log("u a lil bitch");
                        break;
                }
                break;
            case Types.GetHit:
                AbilityManager.instance.getHit += Activate;
                break;
            case Types.ActivateAbility:
                AbilityManager.instance.activateAbility += Activate;
                break;
            case Types.EnemiesCollide:
                AbilityManager.instance.enemiesCollide += Activate;
                break;
            case Types.None:
                Debug.Log("may or may not need to do something here.");
                break;
        }
    }
    public virtual void RemoveFromEvents()
    {
        switch (trigger)
        {
            case Types.Hit:
                switch (hitType)
                {
                    case HitTypes.ActivatedAbility:
                        AbilityManager.instance.hitAbility -= Activate;
                        break;
                    case HitTypes.Sword:
                        AbilityManager.instance.hitSword -= Activate;
                        break;
                    case HitTypes.Both:
                        AbilityManager.instance.hitAny -= Activate;
                        break;
                    case HitTypes.None:
                        Debug.Log("u a lil bitch");
                        break;
                }
                break;
            case Types.GetHit:
                AbilityManager.instance.getHit -= Activate;
                break;
            case Types.ActivateAbility:
                AbilityManager.instance.activateAbility -= Activate;
                break;
            case Types.EnemiesCollide:
                AbilityManager.instance.enemiesCollide -= Activate;
                break;
            case Types.None:
                Debug.Log("may or may not need to do something here.");
                break;
        }
    }
}