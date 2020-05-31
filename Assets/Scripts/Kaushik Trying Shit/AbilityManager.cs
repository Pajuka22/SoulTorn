using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;
    public ActivatedParent[] activatedAbilities = new ActivatedParent[3];
    public PassiveParent[] passiveAbilities = new PassiveParent[5];

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
    public event Action<Enemy> hitAny;
    public void HitAny(Enemy e)
    {
        hitAny?.Invoke(e);
    }
    public event Action<Enemy> hitSword;
    public void HitSword(Enemy e)
    {
        hitSword?.Invoke(e);
    }
    public event Action<Enemy> hitAbility;
    public void HitAbility(Enemy e)
    {
        hitAbility?.Invoke(e);
    }
    void RemoveAllAbilities()
    {
        foreach(AbilityParent a in activatedAbilities)
        {
            a.Disable();
        }
        foreach (AbilityParent a in passiveAbilities)
        {
            a.Disable();
        }
    }
    public event Action<Enemy> getHit;
    public void GetHit(Enemy e)
    {
        getHit?.Invoke(e);
    }
    public event Action<AbilityParent> activateAbility;
    public void ActivateAbility(AbilityParent a)
    {
        activateAbility?.Invoke(a);
    }
    public event Action<Enemy, Enemy> enemiesCollide;
    public void EnemiesCollide(Enemy e1, Enemy e2)
    {
        enemiesCollide?.Invoke(e1, e2);
    }
}
