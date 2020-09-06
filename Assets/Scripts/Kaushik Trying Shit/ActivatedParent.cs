using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedParent : AbilityParent
{
    // Start is called before the first frame update
    [SerializeField]
    protected bool toPassive;
    [SerializeField]
    [Tooltip("doesn't do anything unless toPassive")]
    protected float timeLimit;
    [SerializeField]
    protected GameObject effect;
    [SerializeField]
    [Tooltip("doesn't do anything unless toPassive")]
    protected PassiveParent.Types trigger;
    [SerializeField]
    [Tooltip("doesn't do anything unless passiveType == hit")]
    protected PassiveParent.HitTypes hitType;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void EnablePassive()
    {
        switch (trigger)
        {
            case PassiveParent.Types.Hit:
                switch (hitType)
                {
                    case PassiveParent.HitTypes.ActivatedAbility:
                        AbilityManager.instance.hitAbility += Activate;
                        break;
                    case PassiveParent.HitTypes.Sword:
                        AbilityManager.instance.hitSword += Activate;
                        break;
                    case PassiveParent.HitTypes.Both:
                        AbilityManager.instance.hitAny += Activate;
                        break;
                    case PassiveParent.HitTypes.None:
                        Debug.Log("u a lil bitch");
                        break;
                }
                break;
            case PassiveParent.Types.GetHit:
                AbilityManager.instance.getHit += Activate;
                break;
            case PassiveParent.Types.ActivateAbility:
                AbilityManager.instance.activateAbility += Activate;
                break;
            case PassiveParent.Types.EnemiesCollide:
                AbilityManager.instance.enemiesCollide += Activate;
                break;
            case PassiveParent.Types.None:
                Debug.Log("may or may not need to do something here.");
                break;
        }
    }
    protected virtual void DisablePassive()
    {
        switch (trigger)
        {
            case PassiveParent.Types.Hit:
                switch (hitType)
                {
                    case PassiveParent.HitTypes.ActivatedAbility:
                        AbilityManager.instance.hitAbility -= Activate;
                        break;
                    case PassiveParent.HitTypes.Sword:
                        AbilityManager.instance.hitSword -= Activate;
                        break;
                    case PassiveParent.HitTypes.Both:
                        AbilityManager.instance.hitAny -= Activate;
                        break;
                    case PassiveParent.HitTypes.None:
                        Debug.Log("u a lil bitch");
                        break;
                }
                break;
            case PassiveParent.Types.GetHit:
                AbilityManager.instance.getHit -= Activate;
                break;
            case PassiveParent.Types.ActivateAbility:
                AbilityManager.instance.activateAbility -= Activate;
                break;
            case PassiveParent.Types.EnemiesCollide:
                AbilityManager.instance.enemiesCollide -= Activate;
                break;
            case PassiveParent.Types.None:
                Debug.Log("may or may not need to do something here.");
                break;
        }
    }
}
