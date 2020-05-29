using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skill", order = 1)]

public class Skill: ScriptableObject
{
    // The Skill class consists of five things, two strings  (its name and item description), an image (its cover so to speak), a button to click, and an int (its cost)
    // These variables will call to activate different things when put in the player's slot
    // For one, the string will be called to see which ability is being activated for the player
    // For the second, the int will be called in the shop method
    //This will have two subclasses, activeSkill and passiveSkill. Each of these will have the attack method in them.
    public Sprite cover;
    public string nameOfSkill; //must be unique (plz)
    public string descriptionOfSkill;
    public int cost;
    public List<Skill> branches;

    private void Awake()
    {

    }
}


