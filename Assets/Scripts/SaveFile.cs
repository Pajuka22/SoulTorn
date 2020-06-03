using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    //Player Variables
    public float speed;
    public int jumpProcs;
    public int atkDamage;
    public int atkLag;
    public int maxHealth;
    public int health;
    public float maxSpeed;
    public float dodgeLength;
    public float accelerationRate;
    public float decelerationRate;
    public float InvincibleFrames;
    public GameObject[] passives;
    public GameObject[] actives;
    public static float GravityScale;


    //Level variables
    public int levelAt; //Represents what level the player is at
    public int difficulty; //0: easy, 1: medium, 2: difficult
    public int level; //how many levels the player has completed so far


    public int blueSouls, redSouls = 0; //total num of blue & red souls

    //Abilities    
    public bool canDodge;
    public bool canDodgeRoll;
    public bool canAccelerate;

    //Probably important
    public List<Skill> blueSkills, redSkills; //this might become a list or dictionary at some point
    public string[] skillCover;
    public string[] skillDescription;
    public int[] skillCost;


    public SaveFile(GlobalControl controller)
    {
        speed = controller.speed;
        jumpProcs = controller.jumpProcs;
        atkDamage = controller.atkDamage;
        atkLag = controller.atkLag;
        maxHealth = controller.maxHealth;
        health = controller.health;
        maxSpeed = controller.maxSpeed;
        dodgeLength = controller.dodgeLength;
        accelerationRate = controller.accelerationRate;
        decelerationRate = controller.decelerationRate;
        InvincibleFrames = controller.InvincibleFrames;
        for (int x = 0; x < controller.passives.Length; x++)
        {
            passives[x] = controller.passives[x];
        }
        for (int y = 0; y < controller.actives.Length; y++)
        {
            actives[y] = controller.actives[y];
        }

        canDodge = controller.canDodge;
        canDodgeRoll = controller.canDodgeRoll;
        canAccelerate = controller.canAccelerate;
        blueSouls = controller.blueSouls;
        redSouls = controller.redSouls;
        level = controller.level;
        difficulty = controller.difficulty;

        for (int x = 0; x < controller.blueSkills.Capacity; x++)
        {
            blueSkills[x] = controller.blueSkills[x];

        }
        for (int x = 0; x < controller.redSkills.Capacity; x++)
        {
            redSkills[x] = controller.redSkills[x];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
