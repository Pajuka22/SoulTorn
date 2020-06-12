using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
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
    public float[] playerLocation;


    public int blueSouls, redSouls; //total num of blue & red souls

    //Abilities    
    public bool canDodge;
    public bool canDodgeRoll;
    public bool canAccelerate;
    public bool[] abilities;


    //System Variables
    public int fileNum;



    public SaveFile(Player player)
    {
        speed = GlobalControl.Instance.speed;
        jumpProcs = GlobalControl.Instance.jumpProcs;
        atkDamage = GlobalControl.Instance.atkDamage;
        atkLag = GlobalControl.Instance.atkLag;
        maxHealth = GlobalControl.Instance.maxHealth;
        health = GlobalControl.Instance.health;
        maxSpeed = GlobalControl.Instance.maxSpeed;
        dodgeLength = GlobalControl.Instance.dodgeLength;
        accelerationRate = GlobalControl.Instance.accelerationRate;
        decelerationRate = GlobalControl.Instance.decelerationRate;
        InvincibleFrames = GlobalControl.Instance.InvincibleFrames;


        canDodge = GlobalControl.Instance.canDodge;
        canDodgeRoll = GlobalControl.Instance.canDodgeRoll;
        canAccelerate = GlobalControl.Instance.canAccelerate;
        blueSouls = GlobalControl.Instance.blueSouls;
        redSouls = GlobalControl.Instance.redSouls;
        level = GlobalControl.Instance.level;
        difficulty = GlobalControl.Instance.difficulty;


        //Now you might be  wondering, "What about the scene the player is in?"
        levelAt = player.levelAt;
        playerLocation = new float[2];
        playerLocation[0] = player.transform.position.x;
        playerLocation[1] = player.transform.position.y;

        //NEW ability stuff
        abilities = AbilityManager.instance.GetAllAbilitiesUnlockStatus();


    }

    public SaveFile(int fileNum1)
    {
        //This is for whenever the file is new
        fileNum = fileNum1;
        speed = 4f;
        jumpProcs = 1;
        atkDamage = 10;
        atkLag = 20;
        maxHealth = 100;
        health = 100;
        GravityScale = 3f;
        maxSpeed = 4f;
        dodgeLength = 0f;
        accelerationRate = 0f;
        decelerationRate = 0f;
        InvincibleFrames = 0.5f;

        canDodge = false;
        canDodgeRoll = false;
        canAccelerate = false;

        redSouls = 0;
        blueSouls = 0;

        difficulty = 1;
        levelAt = 12;
        level = 0;

        playerLocation = new float[2];
        playerLocation[0] = 0;
        playerLocation[1] = 8;
        Debug.Log(AbilityManager.instance.GetAllAbilitiesUnlockStatus());
        abilities = AbilityManager.instance.GetAllAbilitiesUnlockStatus();

    }

    // Start is called before the first frame update

}
